/* PreSet.cs - Virtual GRIS5A (C) motion phantom application.
 * Copyright (C) 2018 by Stefan Grimm
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
 * along with the SoftDKb software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Threading;

namespace VirtualGris5A {

  public class PreSet {

    private Timer _timer;
    private int _preSetCounter;
    private int _currentPreset;

    public PreSet(ViewModel view) {
      TimerCallback timerDelegate =
      new TimerCallback(delegate (object state) {
        switch (_currentPreset) {
          default: StopPreset(); break;

          case 1:_prog1(view); break;
          case 2:_prog2(view); break;
          case 3:_prog3(view); break;
          case 4:_prog4(view); break;
          case 5:_prog5(view); break;
          case 6:_prog6(view); break;
          case 7:_prog7(view); break;
          case 8:_prog8(view); break;
        }
      });

      _timer = new Timer(timerDelegate);
    }

    public void StartPreset(int preSet) {
      StopPreset();
      _currentPreset = preSet;
      _preSetCounter = 0;
      if (_timer != null) {
        _timer.Change(0, 100);
      }
    }

    public void StopPreset() {
      if (_timer != null) {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
      }
    }

    // Marker Position 1
    private void _prog1(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 100;
        view.LU.RTNInt = 100;
        view.RU.LNGInt = 100;
        view.RU.RTNInt = 100;
        view.LL.LNGInt = 100;
        view.LL.RTNInt = 100;
        view.RL.LNGInt = 100;
        view.RL.RTNInt = 100;
        view.GA.LNGInt = 0;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter == 30) {
        view.LU.LNGInt = 127;
        view.LU.RTNInt = 127;
        view.RU.LNGInt = 127;
        view.RU.RTNInt = 127;
        view.LL.LNGInt = 127;
        view.LL.RTNInt = 127;
        view.RL.LNGInt = 127;
        view.RL.RTNInt = 127;
      }

      if (_preSetCounter < 30) {
        _preSetCounter++;
      }
    }

    // Marker Position 2
    private void _prog2(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 100;
        view.LU.RTNInt = 100;
        view.RU.LNGInt = 100;
        view.RU.RTNInt = 100;
        view.LL.LNGInt = 100;
        view.LL.RTNInt = 100;
        view.RL.LNGInt = 70;
        view.RL.RTNInt = 70;
        view.GA.LNGInt = 0;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter == 30) {
        view.LU.LNGInt = 167;
        view.LU.RTNInt = 167;
        view.RU.LNGInt = 117;
        view.RU.RTNInt = 117;
        view.LL.LNGInt = 87;
        view.LL.RTNInt = 137;
        view.RL.LNGInt = 137;
        view.RL.RTNInt = 87;
      }

      if (_preSetCounter < 30) {
        _preSetCounter++;
      }
    }

    // Marker Position 1 <-> 2
    private void _prog3(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 147;
        view.LU.RTNInt = 147;
        view.RU.LNGInt = 122;
        view.RU.RTNInt = 122;
        view.LL.LNGInt = 107;
        view.LL.RTNInt = 132;
        view.RL.LNGInt = 132;
        view.RL.RTNInt = 107;
        view.GA.LNGInt = 0;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30) {

        double targetDeltaSmall = 10 * Math.Sin((_preSetCounter - 30) / 80.0 * Math.PI);
        double targetDeltaLarge = 40 * Math.Sin((_preSetCounter - 30) / 80.0 * Math.PI);

        view.LU.LNGInt = 147 + (int)targetDeltaLarge;
        view.LU.RTNInt = 147 + (int)targetDeltaLarge;
        view.RU.LNGInt = 122 - (int)targetDeltaSmall;
        view.RU.RTNInt = 122 - (int)targetDeltaSmall;
        view.LL.LNGInt = 107 - (int)targetDeltaLarge;
        view.LL.RTNInt = 132 + (int)targetDeltaSmall;
        view.RL.LNGInt = 132 + (int)targetDeltaSmall;
        view.RL.RTNInt = 107 - (int)targetDeltaLarge;
      }

      if (_preSetCounter == 189) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }

    // Free-breath Gating
    private void _prog4(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 127;
        view.LU.RTNInt = 127;
        view.RU.LNGInt = 127;
        view.RU.RTNInt = 127;
        view.LL.LNGInt = 127;
        view.LL.RTNInt = 127;
        view.RL.LNGInt = 127;
        view.RL.RTNInt = 127;
        view.GA.LNGInt = 127;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30) {

        double target = 127 + 80 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);

        view.LU.LNGInt = (int)target;
        view.RU.LNGInt = (int)target;
        view.LL.LNGInt = (int)target;
        view.RL.LNGInt = (int)target;
        view.GA.LNGInt = (int)target;
      }

      if (_preSetCounter == 189) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }

    // Breath-hold Gating
    private void _prog5(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 60;
        view.LU.RTNInt = 127;
        view.RU.LNGInt = 60;
        view.RU.RTNInt = 127;
        view.LL.LNGInt = 60;
        view.LL.RTNInt = 127;
        view.RL.LNGInt = 60;
        view.RL.RTNInt = 127;
        view.GA.LNGInt = 60;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30 && _preSetCounter < 330) {

        double target = 60 + 50 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);

        view.LU.LNGInt = (int)target;
        view.RU.LNGInt = (int)target;
        view.LL.LNGInt = (int)target;
        view.RL.LNGInt = (int)target;
        view.GA.LNGInt = (int)target;
      }
      else if (_preSetCounter == 330) {
        view.LU.LNGInt = 250;
        view.RU.LNGInt = 250;
        view.LL.LNGInt = 250;
        view.RL.LNGInt = 250;
        view.GA.LNGInt = 250;
      }
      else if (_preSetCounter > 360 && _preSetCounter < 450) {

        double target = 200 + 50 * Math.Cos((_preSetCounter - 300) / 500.0 * Math.PI);

        view.LU.LNGInt = (int)target;
        view.RU.LNGInt = (int)target;
        view.LL.LNGInt = (int)target;
        view.RL.LNGInt = (int)target;
        view.GA.LNGInt = (int)target;
      }
      else if (_preSetCounter == 450) {
        view.LU.LNGInt = 60;
        view.RU.LNGInt = 60;
        view.LL.LNGInt = 60;
        view.RL.LNGInt = 60;
        view.GA.LNGInt = 60;
      }

      if (_preSetCounter == 469) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }

    // Free-breath Gating, Marker Position 1 <-> 2
    private void _prog6(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 147;
        view.LU.RTNInt = 147;
        view.RU.LNGInt = 122;
        view.RU.RTNInt = 122;
        view.LL.LNGInt = 107;
        view.LL.RTNInt = 132;
        view.RL.LNGInt = 132;
        view.RL.RTNInt = 107;
        view.GA.LNGInt = 127;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30) {

        double targetDeltaSmall = 10 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);
        double targetDeltaLarge = 40 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);
        double targetGating = 127 + 80 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);

        view.LU.LNGInt = 147 + (int)targetDeltaLarge;
        view.LU.RTNInt = 147 + (int)targetDeltaLarge;
        view.RU.LNGInt = 122 - (int)targetDeltaSmall;
        view.RU.RTNInt = 122 - (int)targetDeltaSmall;
        view.LL.LNGInt = 107 - (int)targetDeltaLarge;
        view.LL.RTNInt = 132 + (int)targetDeltaSmall;
        view.RL.LNGInt = 132 + (int)targetDeltaSmall;
        view.RL.RTNInt = 107 - (int)targetDeltaLarge;
        view.GA.LNGInt = (int)targetGating;
      }

      if (_preSetCounter == 89) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }

    // Free-breath Gating loosing signal
    private void _prog7(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 127;
        view.LU.RTNInt = 127;
        view.RU.LNGInt = 127;
        view.RU.RTNInt = 127;
        view.LL.LNGInt = 127;
        view.LL.RTNInt = 127;
        view.RL.LNGInt = 127;
        view.RL.RTNInt = 127;
        view.GA.LNGInt = 127;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30) {

        double target = 127 + 80 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);

        view.LU.LNGInt = (int)target;
        view.RU.LNGInt = (int)target;
        view.LL.LNGInt = (int)target;
        view.RL.LNGInt = (int)target;
        view.GA.LNGInt = (int)target;
      }

      if (_preSetCounter == 300) {
        view.LU.RTNInt = 255;
        view.RU.RTNInt = 255;
        view.LL.RTNInt = 255;
        view.RL.RTNInt = 255;
        view.GA.RTNInt = 255;
      }
      else if (_preSetCounter == 400) {
        view.LU.RTNInt = 127;
        view.RU.RTNInt = 127;
        view.LL.RTNInt = 127;
        view.RL.RTNInt = 127;
        view.GA.RTNInt = 127;
      }

      if (_preSetCounter == 449) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }

    // Free-breath Gating base line shift
    private void _prog8(ViewModel view) {
      if (_preSetCounter == 0) {
        view.LU.LNGInt = 130;
        view.LU.RTNInt = 127;
        view.RU.LNGInt = 130;
        view.RU.RTNInt = 127;
        view.LL.LNGInt = 130;
        view.LL.RTNInt = 127;
        view.RL.LNGInt = 130;
        view.RL.RTNInt = 127;
        view.GA.LNGInt = 130;
        view.GA.RTNInt = 127;
      }
      else if (_preSetCounter >= 30) {
        double baseline = 130 + 30 * Math.Sin((_preSetCounter - 30) / 320.0 * Math.PI);
        double target = baseline + 50 * Math.Sin((_preSetCounter - 30) / 30.0 * Math.PI);

        view.LU.LNGInt = (int)target;
        view.RU.LNGInt = (int)target;
        view.LL.LNGInt = (int)target;
        view.RL.LNGInt = (int)target;
        view.GA.LNGInt = (int)target;
      }

      if (_preSetCounter == 629) {
        _preSetCounter = 30;
      }
      else {
        _preSetCounter++;
      }
    }
  }
}
