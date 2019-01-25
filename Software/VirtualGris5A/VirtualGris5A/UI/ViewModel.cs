/* ViewModel.cs - Virtual GRIS5A (C) motion phantom application.
 * Copyright (C) 2018-2019 by Stefan Grimm
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

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.IO.Ports;
using System;
using MessagingLib;
using ZeroMQ;
using System.Threading;
using System.Threading.Tasks;
using VirtualGris5A.MessagingUtil;
using System.Diagnostics;
using System.Collections.Generic;
using VirtualGris5A.Model.Ev.UI;
using VirtualGris5A.Model.Ev;
using VirtualGris5A.Model.Ev.MotionSystem;
using VirtualGris5A.Model.Ev.PreSet;

namespace VirtualGris5A.UI {

  public class Cylinder : INotifyPropertyChanged {
    private int _lng = 127;
    private int _rtn = 127;

    public int LNGInt {
      get { return _lng; }
      set {
        _lng = value;
        OnPropertyChanged();
        OnPropertyChanged("LNGExt");
      }
    }

    public double LNGExt {
      get { return (_lng - 127) / 255.0 * 45; }
    }

    internal void SetLngInt(int val) {
      _lng = val;
      OnPropertyChanged("LNGExt");
    }

    public int RTNInt {
      get { return _rtn; }
      set {
        _rtn = value;
        OnPropertyChanged();
        OnPropertyChanged("RTNExt");
      }
    }

    public double RTNExt {
      get { return (_rtn - 127) / 255.0 * 180; }
    }

    internal void SetRtnInt(int val) {
      _rtn = val;
      OnPropertyChanged("RTNExt");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class ViewModel : INotifyPropertyChanged, IDisposable {
    private ZSocket _mqOutgoging;
    private Task _taskIncoming;
    private Dictionary<Type, Action<object>> _eventHandlers = new Dictionary<Type, Action<object>>();

    public ViewModel() {
      SSerialPorts = "None";
      SerialPorts.Add(SSerialPorts);
      foreach (var port in SerialPort.GetPortNames()) {
        SerialPorts.Add(port);
      }

      LU.PropertyChanged += LU_PropertyChanged;
      LL.PropertyChanged += LL_PropertyChanged;
      RU.PropertyChanged += RU_PropertyChanged;
      RL.PropertyChanged += RL_PropertyChanged;
      MP.PropertyChanged += MP_PropertyChanged;
      RegisterNotifications();
    }

    public void Dispose() {
      try {
        _taskIncoming.Wait();
      }
      catch (Exception e) {
        if (e.InnerException == null || (e.InnerException != null && !(e.InnerException is OperationCanceledException))) {
          Debug.Fail(e.Message);
        }
      }
      finally {
        _mqOutgoging.Dispose();
        _taskIncoming.Dispose();
      }
    }

    public void OnClosing(object sender, EventArgs args) {
      var msg = new EvShutdown();
      var mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
    }

    public Task StartBindIncomingAsync(CancellationToken cancellationToken, ZContext ctx, string notifyQueueName, AutoResetEvent startupWaitHandle) {
      _taskIncoming = Task.Run(() => {
        Thread.CurrentThread.Name = "ViewModel/Incoming";
        using (ZSocket mqIncomming = new ZSocket(ctx, ZSocketType.PAIR)) {
          mqIncomming.Bind(notifyQueueName);
          startupWaitHandle.Set();
          while (true) {
            cancellationToken.ThrowIfCancellationRequested();
            try {
              using (ZFrame mqmsg = mqIncomming.ReceiveFrame()) {
                IMessage msg = WireMessage.Deserialize(mqmsg.ReadString());
                _eventHandlers[msg.GetType()].Invoke(msg);
              }
            }
            catch (ZException ze) {
              if (ze.Error == ZError.ETERM) {
                break;
              }
              if (ze.Error != ZError.EAGAIN) {
                throw;
              }
            }
          }
        }
      });
      return _taskIncoming;
    }

    public void ConnectOutgoing(ZContext ctx, string commandQueueName) {
      _mqOutgoging = new ZSocket(ctx, ZSocketType.PAIR);
      _mqOutgoging.Connect(commandQueueName);
    }

    private void MP_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      Cylinder cy = (Cylinder)sender;

      var msg = new EvCylinderMotion();
      msg.Cy = Model.Ev.Cylinder.Platform;
      msg.Lng = (ushort)cy.LNGInt;
      msg.Rtn = (ushort)cy.RTNInt;
      string mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
    }

    private void RL_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      Cylinder cy = (Cylinder)sender;

      EvCylinderMotion msg = new EvCylinderMotion();
      msg.Cy = Model.Ev.Cylinder.RightLower;
      msg.Lng = (ushort)cy.LNGInt;
      msg.Rtn = (ushort)cy.RTNInt;
      string mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
    }

    private void RU_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      Cylinder cy = (Cylinder)sender;

      EvCylinderMotion msg = new EvCylinderMotion();
      msg.Cy = Model.Ev.Cylinder.RightUpper;
      msg.Lng = (ushort)cy.LNGInt;
      msg.Rtn = (ushort)cy.RTNInt;
      string mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
    }

    private void LL_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      Cylinder cy = (Cylinder)sender;

      EvCylinderMotion msg = new EvCylinderMotion();
      msg.Cy = Model.Ev.Cylinder.LeftLower;
      msg.Lng = (ushort)cy.LNGInt;
      msg.Rtn = (ushort)cy.RTNInt;
      string mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
    }

    private void LU_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (e.PropertyName.Contains("Int")) {
        Cylinder cy = (Cylinder)sender;

        EvCylinderMotion msg = new EvCylinderMotion();
        msg.Cy = Model.Ev.Cylinder.LeftUpper;
        msg.Lng = (ushort)cy.LNGInt;
        msg.Rtn = (ushort)cy.RTNInt;
        string mqmsg = WireMessage.Serialize(msg);
        using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
      }
    }

    public ICommand GoManual {
      get {
        return new RelayCommand<object>(param => {
          var msg = new EvManualMotionClick();
          var mqmsg = WireMessage.Serialize(msg);
          using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
        });
      }
    }

    public ICommand GoPreSet {
      get {
        return new RelayCommand<string>(param => {
          ushort n;
          if (ushort.TryParse(param, out n)) {
            var msg = new EvPresetMotionClick();
            msg.Num = n;
            var mqmsg = WireMessage.Serialize(msg);
            _mqOutgoging.Send(new ZFrame(mqmsg));
          }
        });
      }
    }

    public ICommand DoConnectSerialPort {
      get {
        return new RelayCommand<bool>(ischecked => {
          if (ischecked) {
            var msg = new EvConnect();
            msg.ComPort = SSerialPorts;
            var mqmsg = WireMessage.Serialize(msg);
            _mqOutgoging.Send(new ZFrame(mqmsg));
          }
          else {
            var msg = new EvDisconnect();
            var mqmsg = WireMessage.Serialize(msg);
            _mqOutgoging.Send(new ZFrame(mqmsg));
          }
        });
      }
    }

    public ICommand DoRefreshSerialPorts {
      get {
        return new RelayCommand<object>(param => {
          SerialPorts.Clear();
          SSerialPorts = "None";
          SerialPorts.Add(SSerialPorts);
          foreach (var port in SerialPort.GetPortNames()) {
            SerialPorts.Add(port);
          }
          OnPropertyChanged("SerialPorts");
          OnPropertyChanged("SSerialPorts");
        });
      }
    }

    public Cylinder LU { get; private set; } = new Cylinder();
    public Cylinder RU { get; private set; } = new Cylinder();
    public Cylinder LL { get; private set; } = new Cylinder();
    public Cylinder RL { get; private set; } = new Cylinder();
    public Cylinder MP { get; private set; } = new Cylinder();

    public ObservableCollection<string> SerialPorts { get; } = new ObservableCollection<string>();
    public string SSerialPorts { get; set; }
    public string Logging { get; private set; } = string.Empty;

    private void OnEvCylinderPositions(EvCylinderPositions ev) {
      foreach (var pos in ev.Positions) {
        switch (pos.Cy) {
        default:
          break;
        case Model.Ev.Cylinder.LeftUpper:
          LU.SetLngInt(pos.Lng);
          LU.SetRtnInt(pos.Rtn);
          break;
        case Model.Ev.Cylinder.LeftLower:
          LL.SetLngInt(pos.Lng);
          LL.SetRtnInt(pos.Rtn);
          break;
        case Model.Ev.Cylinder.RightUpper:
          RU.SetLngInt(pos.Lng);
          RU.SetRtnInt(pos.Rtn);
          break;
        case Model.Ev.Cylinder.RightLower:
          RL.SetLngInt(pos.Lng);
          RL.SetRtnInt(pos.Rtn);
          break;
        case Model.Ev.Cylinder.Platform:
          MP.SetLngInt(pos.Lng);
          MP.SetRtnInt(pos.Rtn);
          break;
        }
      }
      OnPropertyChanged();
    }

    private void OnEvLogMessage(EvLogMessage ev) {
      Logging = Logging.Insert(0, ev.Message + Environment.NewLine);
      OnPropertyChanged("Logging");
    }

    private void OnEvShutdown(EvShutdown ev) {
      throw new OperationCanceledException();
    }

    private void RegisterNotifications() {      
      _eventHandlers[typeof(EvCylinderPositions)] = ev => OnEvCylinderPositions((EvCylinderPositions)ev);
      _eventHandlers[typeof(EvLogMessage)] = ev => OnEvLogMessage((EvLogMessage)ev);
      _eventHandlers[typeof(EvShutdown)] = ev => OnEvShutdown((EvShutdown)ev);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }

}