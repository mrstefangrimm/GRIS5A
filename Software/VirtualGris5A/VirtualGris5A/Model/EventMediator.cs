﻿/* EventMediator.cs - Virtual GRIS5A (C) motion phantom application.
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

using MessagingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using VirtualGris5A.MessagingUtil;
using VirtualGris5A.Model.Ev;
using VirtualGris5A.Model.Ev.MotionSystem;
using VirtualGris5A.Model.Ev.UI;
using ZeroMQ;

namespace VirtualGris5A.Model {

  public class EventMediator : IDisposable {

    private Task _taskIncoming;
    private ZSocket _mqNotifyViewModel;
    private Dictionary<Type, Action<object>> _eventHandlers = new Dictionary<Type, Action<object>>();

    private MotionSystem _motionSystem;
    private PreSetMotionGenerator _preSetMotionGenerator;
    private bool _isMotionSystemConnected;

    public EventMediator() {
      _motionSystem = new MotionSystem(OnMotionSystemLogMessage);
      _preSetMotionGenerator = new PreSetMotionGenerator(OnEvCylinderPositionsPreSet);
      RegisterCommands();
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
        _mqNotifyViewModel.Dispose();
        _taskIncoming.Dispose();
      }
    }

    public Task StartBindIncomingAsync(CancellationToken cancellationToken, ZContext ctx, string commandQueueName, AutoResetEvent startupWaitHandle) {
      _taskIncoming = Task.Run(() => {
        Thread.CurrentThread.Name = "EventMediator/Incoming";
        using (ZSocket mqIncoming = new ZSocket(ctx, ZSocketType.PULL)) {
          // Also notify sockets have to be created on the thread they are used
          _mqNotifyViewModel = new ZSocket(ctx, ZSocketType.PAIR);
          mqIncoming.Bind(commandQueueName);
          startupWaitHandle.Set();
          while (true) {
            cancellationToken.ThrowIfCancellationRequested();
            try {
              using (ZFrame mqmsg = mqIncoming.ReceiveFrame()) {
                IMessage msg = WireMessage.Deserialize(mqmsg.ReadString());
                _eventHandlers[msg.GetType()].Invoke(msg);
              }
            }
            catch (ZException ze) {
              if (ze.Error == ZError.ETERM) {
                // ZError.ETERM is thrown if the context is closed
                break;
              }
              if (ze.Error != ZError.EAGAIN) {
                // ZError.EAGAIN is thrown if the receive timeout exceeded (mqIncoming.SetOption(ZSocketOption.RCVTIMEO, 200);)
                throw;
              }
            }
          }
        }
      });
      return _taskIncoming;
    }
        
    public void ConnectViewModel(ZContext ctx, string notifyQueueName) {
      _mqNotifyViewModel.Connect(notifyQueueName);
    }

    private static ISerialOutData CylinderToMotionSystem(Ev.UI.EvCylinderMotion ev) {
      MotorOutCommand cmd = new MotorOutCommand();
      switch (ev.Cy) {
      default:
        break;
      case Cylinder.LeftUpper:
        cmd.Add(ServoNumber.LULNG, ev.Lng, 5);
        cmd.Add(ServoNumber.LURTN, ev.Rtn, 5);
        break;
      case Cylinder.LeftLower:
        cmd.Add(ServoNumber.LLLNG, ev.Lng, 5);
        cmd.Add(ServoNumber.LLRTN, ev.Rtn, 5);
        break;
      case Cylinder.RightUpper:
        cmd.Add(ServoNumber.RULNG, ev.Lng, 5);
        cmd.Add(ServoNumber.RURTN, ev.Rtn, 5);
        break;
      case Cylinder.RightLower:
        cmd.Add(ServoNumber.RLLNG, ev.Lng, 5);
        cmd.Add(ServoNumber.RLRTN, ev.Rtn, 5);
        break;
      case Cylinder.Platform:
        cmd.Add(ServoNumber.MPLNG, ev.Lng, 5);
        cmd.Add(ServoNumber.MPRTN, ev.Rtn, 5);
        break;
      }
      return cmd;
    }

    private static ISerialOutData CylinderToMotionSystem(Ev.PreSet.EvCylinderPositions ev) {
      MotorOutCommand cmd = new MotorOutCommand();
      foreach(var pos in ev.Positions) {
        switch (pos.Cy) {
        default:
          break;
        case Cylinder.LeftUpper:
          cmd.Add(ServoNumber.LULNG, pos.Lng, pos.StepSize);
          cmd.Add(ServoNumber.LURTN, pos.Rtn, pos.StepSize);
          break;
        case Cylinder.LeftLower:
          cmd.Add(ServoNumber.LLLNG, pos.Lng, pos.StepSize);
          cmd.Add(ServoNumber.LLRTN, pos.Rtn, pos.StepSize);
          break;
        case Cylinder.RightUpper:
          cmd.Add(ServoNumber.RULNG, pos.Lng, pos.StepSize);
          cmd.Add(ServoNumber.RURTN, pos.Rtn, pos.StepSize);
          break;
        case Cylinder.RightLower:
          cmd.Add(ServoNumber.RLLNG, pos.Lng, pos.StepSize);
          cmd.Add(ServoNumber.RLRTN, pos.Rtn, pos.StepSize);
          break;
        case Cylinder.Platform:
          cmd.Add(ServoNumber.MPLNG, pos.Lng, pos.StepSize);
          cmd.Add(ServoNumber.MPRTN, pos.Rtn, pos.StepSize);
          break;
        }
      }      
      return cmd;
    }

    private void OnEvCylinderMotionUI(Ev.UI.EvCylinderMotion ev) {
      if (_isMotionSystemConnected) {
        ISerialOutData cmd = CylinderToMotionSystem(ev);
        _motionSystem.Send(cmd);
      }
    }

    private void OnEvCylinderPositionsPreSet(Ev.PreSet.EvCylinderPositions ev) {
      // not over the event queue (yet)
      if (_isMotionSystemConnected) {
        ISerialOutData cmd = CylinderToMotionSystem(ev);
        _motionSystem.Send(cmd);
      }
      var mqmsg = WireMessage.Serialize(ev);
      using (var frame = new ZFrame(mqmsg)) { _mqNotifyViewModel.Send(frame); }
    }

    private void OnEvLogMessage(EvLogMessage ev) {
      string mqmsg = WireMessage.Serialize(ev);
      using (var frame = new ZFrame(mqmsg)) { _mqNotifyViewModel.Send(frame); }
    }

    private void OnEvManualMotionClick(EvManualMotionClick ev) {
      _preSetMotionGenerator.ManualMotionClick(ev);
    }

    private void OnEvPresetMotionClick(EvPresetMotionClick ev) {
      _preSetMotionGenerator.PresetMotionClick(ev);
    }

    private void OnEvShutdown(EvShutdown ev) {
      var mqmsg = WireMessage.Serialize(ev);
      using (var frame = new ZFrame(mqmsg)) { _mqNotifyViewModel.Send(frame); }

      _motionSystem.Shutdown(ev);
      _preSetMotionGenerator.Shutdown(ev);

      throw new OperationCanceledException();
    }

    private void OnEvConnect(EvConnect ev) {
      _isMotionSystemConnected = true;
      _motionSystem.Connect(ev);
    }

    private void OnEvDisconnect(EvDisconnect ev) {
      _isMotionSystemConnected = false;
      _motionSystem.Disconnect(ev);
    }

    private void OnMotionSystemLogMessage(string logMsg) {
      // not over the event queue (yet)
      EvLogMessage msg = new EvLogMessage();
      msg.Message = logMsg;
      string mqmsg = WireMessage.Serialize(msg);
      using (var frame = new ZFrame(mqmsg)) { _mqNotifyViewModel.Send(frame); }
    }

    private void RegisterCommands() {
      _eventHandlers[typeof(Ev.UI.EvCylinderMotion)] = ev => OnEvCylinderMotionUI((Ev.UI.EvCylinderMotion)ev);
      _eventHandlers[typeof(Ev.PreSet.EvCylinderPositions)] = ev => OnEvCylinderPositionsPreSet((Ev.PreSet.EvCylinderPositions)ev);
      _eventHandlers[typeof(EvLogMessage)] = ev => OnEvLogMessage((EvLogMessage)ev);
      _eventHandlers[typeof(EvManualMotionClick)] = ev => OnEvManualMotionClick((EvManualMotionClick)ev);
      _eventHandlers[typeof(EvPresetMotionClick)] = ev => OnEvPresetMotionClick((EvPresetMotionClick)ev);
      _eventHandlers[typeof(EvConnect)] = ev => OnEvConnect((EvConnect)ev);
      _eventHandlers[typeof(EvDisconnect)] = ev => OnEvDisconnect((EvDisconnect)ev);
      _eventHandlers[typeof(EvShutdown)] = ev => OnEvShutdown((EvShutdown)ev);
    }
 
  }

}
