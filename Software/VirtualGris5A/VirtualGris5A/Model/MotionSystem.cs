/* MotionSustem.cs - Virtual GRIS5A (C) motion phantom application.
 * Copyright (C) 2019 by Stefan Grimm
 *
 * This is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This software is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the VirtualGris05 software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using VirtualGris5A.Model.Ev;
using VirtualGris5A.Model.Ev.MotionSystem;

namespace VirtualGris5A.Model {

  public delegate void MotionSustemLog(string logmsg);

  public interface ISerialOutData {
    byte[] Data { get; }
    void Add(ServoNumber m, UInt16 pos, UInt16 step);
    void Add(ISerialOutData moreData);
  }

  public enum ServoNumber {
    LURTN = 0,
    LULNG,
    LLRTN,
    LLLNG,
    RLLNG,
    RLRTN,
    RULNG,
    RURTN,
    MPLNG,
    MPRTN
  }

  internal class MotorOutCommand : ISerialOutData {
    const byte CMD = 2;
    private Dictionary<ServoNumber, byte[]> _servoData = new Dictionary<ServoNumber, byte[]>();

    public void Add(ServoNumber m, UInt16 pos, UInt16 step) {
      var motorData = new byte[2];
      motorData[0] = (byte)((step << 4) | (byte)m);
      motorData[1] = (byte)(pos);
      lock (_servoData) {
        if (_servoData.ContainsKey(m)) {
          _servoData[m] = motorData;
        }
        else {
          _servoData.Add(m, motorData);
        }
      }
      //_servoData.Add(m, (byte)((step << 4) | (byte)m));
      //_servoData.Add((byte)(pos));
    }
    public void Add(ISerialOutData moreData) {
      var motorData = moreData as MotorOutCommand;
      if (moreData != null) {
        lock (_servoData) {
          motorData._servoData.Keys.ToList().ForEach(k => {
            if (_servoData.ContainsKey(k)) {
              _servoData[k] = motorData._servoData[k];
            }
            else {
              _servoData.Add(k, motorData._servoData[k]);
            }
          });
        }
      }
    }

    public void Clear() {
      _servoData.Clear();
    }

    public byte[] Data {
      get {
        lock (_servoData) {
          int numBytes = _servoData.Values.Sum(x => x.Length);
          List<byte> serout = new List<byte>(1 + numBytes);
          serout.Add((byte)(CMD | (numBytes << 3)));
          _servoData.Values.ToList().ForEach(x => serout.AddRange(x));
          return serout.ToArray<byte>();
        }
        //byte[] serout = new byte[1 + _servoData.Count];
        //serout[0] = (byte)(CMD | (_servoData.Count << 3));
        //Array.Copy(_servoData.ToArray(), 0, serout, 1, _servoData.Count);
        //return serout;
      }
    }

  }

  public class MotionSystem : IDisposable {

    private const int _portBaudRate = 9600; // 9600, 38400, 115200;
    private MotorOutCommand _sendBuffer = new MotorOutCommand();
    public SerialPort _serialPort;
    private Timer _timer;
    private int _lastSentHashCode;
    private MotionSustemLog _logHandler;
    
    public MotionSystem(MotionSustemLog handler) {
      _logHandler = handler;

      TimerCallback timerDelegate =
      new TimerCallback(delegate (object state) {
       lock(_sendBuffer) {
          var data = _sendBuffer.Data;
          int hashCode = data.GetHashCode();
          if (/*hashCode != _lastSentHashCode &&*/ data.Length > 1 && _serialPort != null && _serialPort.IsOpen) {
            _serialPort.Write(data, 0, data.Length);
            _sendBuffer.Clear();
            _lastSentHashCode = hashCode;
          }
        }
      });
      _timer = new Timer(timerDelegate, null, 50, 50);
    }

    public void Dispose() {
      if (_timer != null) {
        _timer.Dispose();
        _timer = null;
      }
      if (_serialPort != null) {
        _serialPort.Dispose();
        _serialPort = null;
      }
    }

    public void Connect(EvConnect ev) {
      SerialConnect(ev.ComPort);
    }

    public void Disconnect(EvDisconnect ev) {
      SerialDisconnect();
    }

    public void Shutdown(EvShutdown ev) {
      _serialPort?.Close();
      _timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    public void Send(ISerialOutData data) {
      if (_serialPort != null && _serialPort.IsOpen) {
        lock (_sendBuffer) {
          //var seroutbytes = data.Data;
          //_serialPort.Write(seroutbytes, 0, seroutbytes.Length);

          //_timer.Change(5000, 5000);
          _sendBuffer.Add(data);
        }
      }
      else {
        _logHandler("Send failed since Serial Port is not open.");
      }
    }

    private void ServoAbsolutePositions(EvServoAbsolutePositions ev) {
    }

    private void SerialConnect(string comPort) {
      SerialDisconnect();

      _serialPort = new SerialPort(comPort, _portBaudRate);
      _serialPort.DataReceived += OnSerialPortDataReceived;
      try {
        _serialPort.Open();
      }
      catch (Exception e) {
        if (e.InnerException != null) {
          _logHandler(e.InnerException.Message);
        }
        else {
          _logHandler(e.Message);
        }
        SerialDisconnect();
      }
    }

    private void SerialDisconnect() {
      if (_serialPort != null) {
        _serialPort.DataReceived -= OnSerialPortDataReceived;
        _serialPort.Close();
        _serialPort.Dispose();
        _serialPort = null;
      }
    }

    private void OnSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e) {
      char[] serin = new char[_serialPort.BytesToRead];
      _serialPort.Read(serin, 0, serin.Length);

      _logHandler(new string(serin));

      //int beforeHash = _receivedData.GetHashCode();
      //serin.ToList().ForEach(ch => {
      //  if (!_receiving && ch != '|') {
      //    _receivedData += ch;
      //  }
      //  if (ch == '|') {
      //    _receiving = !_receiving;
      //  }
      //});
      //if (_receivedData.GetHashCode() != beforeHash) {
      //  _OnPropertyChanged("Logging");
      //}
    }

  }
}
