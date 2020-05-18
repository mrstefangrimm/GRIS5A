﻿/* RemoteMotionGenerator.cs - Virtual No3 (C) motion phantom application.
 * Copyright (C) 2019-2020 by Stefan Grimm
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
 * along with the VirtualNo3 software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Threading;

namespace ViphApp.No3 {

  enum Cylinder { Upper, Lower, Platform }

  class CylinderPosition {
    public Cylinder Cy;
    public ushort Lng;
    public ushort Rtn;
    public ushort StepSize;
  }

  class MotionPatternGenerator : IDisposable {

    private const int PRESETTIMERINCR = 40;

    private Timer _timer;
    private int _preSetTimer;
    private int _currentProgramId;

    public MotionPatternGenerator(Action<IEnumerable<CylinderPosition>> handler) {
      TimerCallback timerDelegate =
      new TimerCallback(delegate (object state) {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        switch (_currentProgramId) {
          default: Stop(); break;

          case 1:_prog1(handler); break;
          case 2:_prog2(handler); break;
          case 3:_prog3(handler); break;
          case 4:_prog4(handler); break;
          case 5:_prog5(handler); break;
          case 6:_prog6(handler); break;
          case 7:_prog7(handler); break;
          case 8:_prog8(handler); break;
        }
        _timer.Change(PRESETTIMERINCR, PRESETTIMERINCR);
      });

      _timer = new Timer(timerDelegate);
    }

    public void Dispose() {
      if (_timer != null) {
        _timer.Dispose();
        _timer = null;
      }
    }

    public void Start(int programId) {
      Stop();
      _currentProgramId = programId;
      _preSetTimer = 0;
      if (_timer != null) {
        _timer.Change(0, PRESETTIMERINCR);
      }
    }

    public void Stop() {
      if (_timer != null) {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
      }
    }

    private void _prog1(Action<IEnumerable<CylinderPosition>> handler) {
      //  Position 1
      const ushort STEPSZ = 2;
      CylinderPosition[] pos = new CylinderPosition[3];
      pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 128, Rtn = 127, StepSize = STEPSZ };
      pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 128, Rtn = 127, StepSize = STEPSZ };
      pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 128, Rtn = 127, StepSize = STEPSZ };
      handler(pos);
    }

    private void _prog2(Action<IEnumerable<CylinderPosition>> handler) {
      // Position 2
      const ushort STEPSZ = 2;
      CylinderPosition[] pos = new CylinderPosition[3];
      pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 0, Rtn = 255, StepSize = STEPSZ };
      pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 40, Rtn = 79, StepSize = STEPSZ };
      pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 0, Rtn = 135, StepSize = STEPSZ };
      handler(pos);
    }

    private void _prog3(Action<IEnumerable<CylinderPosition>> handler) {
      // Position 1 <-> 2

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 64, Rtn = 191, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 84, Rtn = 103, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 8;
        double targetLlng = 64 - 64 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetRlng = 84 - 44 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetLrtn = 191 + 64 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetRrtn = 103 - 24 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[2];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)targetLlng, Rtn = (ushort)targetLrtn, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)targetRlng, Rtn = (ushort)targetRrtn, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer == 8960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    private void _prog4(Action<IEnumerable<CylinderPosition>> handler) {
      // Free-breath Gating   

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 8;
        double target = 127 + 80 * Math.Sin((_preSetTimer - 3000) / 2500.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      if (_preSetTimer == 7960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    private void _prog5(Action<IEnumerable<CylinderPosition>> handler) {
      // Breath-hold Gating    
    
      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000 && _preSetTimer < 28000) {
        const ushort STEPSZ = 8;
        double target = 60 + 50 * Math.Sin((_preSetTimer - 3000) / 2500.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }     
      else if (_preSetTimer > 28000 && _preSetTimer < 38000) {
        const ushort STEPSZ = 4;
        double target = 200 + 50 * Math.Cos((_preSetTimer - 28000) / 40000.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer == 38000) {
        const ushort STEPSZ = 4;

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      if (_preSetTimer == 40000) {
        _preSetTimer = 6720;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    private void _prog6(Action<IEnumerable<CylinderPosition>> handler) {
      // Free-breath Gating, Position 1 <-> 2

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 64, Rtn = 191, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 84, Rtn = 103, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 64, Rtn = 131, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 8;
        double targetLGlng = 64 - 64 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetRlng = 84 - 44 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetLrtn = 191 + 64 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetRrtn = 103 - 24 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetGrtn = 131 + 4 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)targetLGlng, Rtn = (ushort)targetLrtn, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)targetRlng, Rtn = (ushort)targetRrtn, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)targetLGlng, Rtn = (ushort)targetGrtn, StepSize = STEPSZ };
        handler(pos);
      }            
      if (_preSetTimer == 8960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    private void _prog7(Action<IEnumerable<CylinderPosition>> handler) {
      // Free-breath Gating loosing signal

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 8;
        double target = 127 + 80 * Math.Sin((_preSetTimer - 3000) / 2500.0 * Math.PI);

        ushort rtnGP = 127;
        if (_preSetTimer >= 25000 && _preSetTimer < 35000) { rtnGP = 255; }

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = rtnGP, StepSize = STEPSZ };
        handler(pos);
      }      
      if (_preSetTimer == 37960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    private void _prog8(Action<IEnumerable<CylinderPosition>> handler) {
      // Free-breath Gating base line shift
      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 8;
        double baseline = 130 + 30 * Math.Sin((_preSetTimer - 3000) / 30000.0 * Math.PI);
        double target = baseline + 50 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);

        CylinderPosition[] pos = new CylinderPosition[3];
        pos[0] = new CylinderPosition() { Cy = Cylinder.Upper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[1] = new CylinderPosition() { Cy = Cylinder.Lower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos[2] = new CylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }          
      if (_preSetTimer == 62960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }
   
  }
}
