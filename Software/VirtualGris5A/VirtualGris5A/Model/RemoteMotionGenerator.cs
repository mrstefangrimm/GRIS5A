/* RemoteMotionGenerator.cs - Virtual GRIS5A (C) motion phantom application.
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

using System;
using System.Threading;
using VirtualGris5A.Model.Ev;
using VirtualGris5A.Model.Ev.PreSet;
using VirtualGris5A.Model.Ev.UI;

namespace VirtualGris5A.Model {

  public delegate void PreSetMotion(EvCylinderPositions pos);

  public class RemoteMotionGenerator : IDisposable {

    private const int PRESETTIMERINCR = 40;

    private Timer _timer;
    private int _preSetTimer;
    private int _currentPreset;

    public RemoteMotionGenerator(PreSetMotion handler) {
      TimerCallback timerDelegate =
      new TimerCallback(delegate (object state) {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        switch (_currentPreset) {
          default: StopPreset(); break;

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

    public void PresetMotionClick(EvPresetMotionClick ev) {
      StopPreset();
      _currentPreset = ev.Num;
      _preSetTimer = 0;
      if (_timer != null) {
        _timer.Change(0, PRESETTIMERINCR);
      }
    }

    public void ManualMotionClick(EvManualMotionClick ev) {
      StopPreset();
    }

    public void Shutdown(EvShutdown ev) {
      StopPreset();
    }

    private void StopPreset() {
      if (_timer != null) {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
      }
    }

    // Marker Position 1
    private void _prog1(PreSetMotion handler) {

      const ushort STEPSZ = 2;

      if (_preSetTimer == 0) {
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 0, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer == 3000) {
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[4];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer <= 3000) {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    // Marker Position 2
    private void _prog2(PreSetMotion handler) {

      const ushort STEPSZ = 2;

      if (_preSetTimer == 0) {
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 100, Rtn = 100, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 70, Rtn = 70, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 0, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer == 3000) {
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[4];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 167, Rtn = 167, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 117, Rtn = 117, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 87, Rtn = 137, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 137, Rtn = 87, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer <= 3000) {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    // Marker Position 1 <-> 2
    private void _prog3(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 147, Rtn = 147, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 122, Rtn = 122, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 107, Rtn = 132, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 132, Rtn = 107, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 0, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 10;
        double targetDeltaSmall = 10 * Math.Sin((_preSetTimer - 3000) / 3000d * Math.PI);
        double targetDeltaLarge = 40 * Math.Sin((_preSetTimer - 3000) / 3000d * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[4];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)(147 - targetDeltaLarge), Rtn = (ushort)(147 + targetDeltaLarge), StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)(122 - targetDeltaSmall), Rtn = (ushort)(122 - targetDeltaSmall), StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)(107 + targetDeltaLarge), Rtn = (ushort)(132 + targetDeltaSmall), StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)(132 + targetDeltaSmall), Rtn = (ushort)(107 - targetDeltaLarge), StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer == 8960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    // Free-breath Gating
    private void _prog4(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 2960) {
        const ushort STEPSZ = 10;
        double target = 127 + 80 * Math.Sin((_preSetTimer - 2960) / 2480.0 * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer == 7840) {
        _preSetTimer = 2960;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }
    }

    // Breath-hold Gating
    private void _prog5(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 60, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000 && _preSetTimer < 28000) {
        const ushort STEPSZ = 10;
        double target = 60 + 50 * Math.Sin((_preSetTimer - 3000) / 2500.0 * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer == 28000) {
        const ushort STEPSZ = 4;

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 250, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 250, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 250, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 250, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 250, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer > 28000 && _preSetTimer < 38000) {
        const ushort STEPSZ = 10;
        double target = 200 + 50 * Math.Cos((_preSetTimer - 28000) / 40000.0 * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer == 38000) {
        const ushort STEPSZ = 4;

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 10, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer == 40000) {
        _preSetTimer = 6720;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }

    }

    // Free-breath Gating, Marker Position 1 <-> 2
    private void _prog6(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 147, Rtn = 147, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 122, Rtn = 122, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 107, Rtn = 132, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 132, Rtn = 107, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {

        const ushort STEPSZ = 10;
        double targetDeltaSmall = 10 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetDeltaLarge = 40 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);
        double targetGating = 127 + 80 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)(147 - targetDeltaLarge), Rtn = (ushort)(147 + targetDeltaLarge), StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)(122 - targetDeltaSmall), Rtn = (ushort)(122 - targetDeltaSmall), StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)(107 + targetDeltaLarge), Rtn = (ushort)(132 + targetDeltaSmall), StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)(132 + targetDeltaSmall), Rtn = (ushort)(107 - targetDeltaLarge), StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)targetGating, Rtn = 127, StepSize = STEPSZ };
        handler(pos);

      }

      if (_preSetTimer == 8960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }

    }

    // Free-breath Gating loosing signal
    private void _prog7(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;
        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 127, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 2960) {
        const ushort STEPSZ = 10;
        double target = 127 + 80 * Math.Sin((_preSetTimer - 2960) / 2480.0 * Math.PI);

        ushort rtnMP = 127;
        if (_preSetTimer >= 25000 && _preSetTimer < 35000) { rtnMP = 255; }

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)(target), Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)(target), Rtn = rtnMP, StepSize = STEPSZ };
        handler(pos);
      }

      if (_preSetTimer == 37960) {
        _preSetTimer = 3000;
      }
      else {
        _preSetTimer += PRESETTIMERINCR;
      }

    }

    // Free-breath Gating base line shift
    private void _prog8(PreSetMotion handler) {

      if (_preSetTimer == 0) {
        const ushort STEPSZ = 2;

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = 130, Rtn = 127, StepSize = STEPSZ };
        handler(pos);
      }
      else if (_preSetTimer >= 3000) {
        const ushort STEPSZ = 10;
        double baseline = 130 + 30 * Math.Sin((_preSetTimer - 3000) / 30000.0 * Math.PI);
        double target = baseline + 50 * Math.Sin((_preSetTimer - 3000) / 3000.0 * Math.PI);

        EvCylinderPositions pos = new EvCylinderPositions();
        pos.Positions = new EvCylinderPosition[5];
        pos.Positions[0] = new EvCylinderPosition() { Cy = Cylinder.LeftUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[2] = new EvCylinderPosition() { Cy = Cylinder.RightUpper, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[1] = new EvCylinderPosition() { Cy = Cylinder.LeftLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[3] = new EvCylinderPosition() { Cy = Cylinder.RightLower, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
        pos.Positions[4] = new EvCylinderPosition() { Cy = Cylinder.Platform, Lng = (ushort)target, Rtn = 127, StepSize = STEPSZ };
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
