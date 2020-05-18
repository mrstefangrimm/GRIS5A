﻿/* No3ControlViewModel.cs - ViphApp (C) motion phantom application.
 * Copyright (C) 2020 by Stefan Grimm
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
 * along with the ViphApp software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViphApp.Common.Com;
using ViphApp.Common.UI;

namespace ViphApp.No3.UI {

  public enum No3ControlViewState {
    Manual,
    Manual_Minimized,
    Automatic,
    Automatic_Minimized
  }

  public class No3ControlViewModel : No3ViewModel, IPlugInControlViewModel {

    private MophAppProxy _mophApp;
    private No3ControlViewState _viewState;
    private bool _isRunning;
    private string _selectedProgram;
    private MotionPatternGenerator _patternGenerator;

    static No3ControlViewModel() {
      QuickConverter.EquationTokenizer.AddNamespace(typeof(No3ControlViewState));
      QuickConverter.EquationTokenizer.AddNamespace(typeof(System.Windows.Visibility));
    }

    public No3ControlViewModel(MophAppProxy mophApp) {
      _mophApp = mophApp;
      ControlViewState = No3ControlViewState.Manual;

      Programs.Add("Program 1");
      Programs.Add("Program 2");
      Programs.Add("Program 3");
      Programs.Add("Program 4");
      Programs.Add("Program 5");
      Programs.Add("Program 6");
      Programs.Add("Program 7");
      Programs.Add("Program 8");
      SelectedProgram = "Program 1";

      UP.PropertyChanged += UP_PropertyChanged;
      LO.PropertyChanged += LO_PropertyChanged;
      GA.PropertyChanged += GA_PropertyChanged;

      UP.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      LO.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      GA.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);

      _patternGenerator = new MotionPatternGenerator(OnCylinderPositionsChanged);
    }

    public No3ControlViewState ControlViewState {
      get { return _viewState; }
      private set {
        if (_viewState != value) {
          _viewState = value;
          OnPropertyChanged();
        }
      }
    }

    public ICommand DoSetManual {
      get {
        return new RelayCommand<object>(param => {
          ControlViewState = No3ControlViewState.Manual;
        });
      }
    }

    public ICommand DoSetAutomatic {
      get {
        return new RelayCommand<object>(param => {
          ControlViewState = No3ControlViewState.Automatic;
        });
      }
    }

    public ICommand DoSetMinimized {
      get {
        return new RelayCommand<object>(param => {
          switch (ControlViewState) {
          default:
          case No3ControlViewState.Manual_Minimized:
            ControlViewState = No3ControlViewState.Manual;
            break;
          case No3ControlViewState.Automatic_Minimized:
            ControlViewState = No3ControlViewState.Automatic;
            break;
          case No3ControlViewState.Manual:
            ControlViewState = No3ControlViewState.Manual_Minimized;
            break;
          case No3ControlViewState.Automatic:
            ControlViewState = No3ControlViewState.Automatic_Minimized;
            break;
          }
        });
      }
    }

    INotifyPropertyChanged IPlugInControlViewModel.GA => GA;

    public ObservableCollection<string> Programs { get; } = new ObservableCollection<string>();
    public string SelectedProgram {
      get {
        return _selectedProgram;
      }
      set {
        if (_selectedProgram != value) {
          _selectedProgram = value;
          OnPropertyChanged();
          OnPropertyChanged("SelectedProgramDescription");
        }
      }
    }
    public string SelectedProgramDescription {
      get {
        return string.Format("This is a description what '{0}' is doing. This is just some more text.", SelectedProgram);
      }
    }

    public bool IsRunning {
      get {
        return _isRunning;
      }
      set {
        if (_isRunning != value) {
          _isRunning = value;
          if (_isRunning) {
            string[] progrIds = SelectedProgram.Split(' ');
            if (progrIds != null && progrIds.Length == 2) {
              _patternGenerator.Start(int.Parse(progrIds[1]));
            }
          }
          else {
            _patternGenerator.Stop();
          }
          OnPropertyChanged();
        }
      }
    }

    private void UP_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      var internalProp = !((CylinderPropertyChangedEventArgs)e).External;
      if (internalProp && _mophApp.State == MophAppProxy.SyncState.Synced) {
        CylinderViewModel cy = (CylinderViewModel)sender;
        var lng = (ushort)cy.LNGInt;
        var rtn = (ushort)cy.RTNInt;
        MotionSystemMotorPosition[] pos = new[] {
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.LLNG, StepSize = 5, Value = lng },
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.LRTN, StepSize = 5, Value = rtn }
        };
        _mophApp.GoTo(pos);
      }
    }

    private void LO_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      var internalProp = !((CylinderPropertyChangedEventArgs)e).External;
      if (internalProp && _mophApp.State == MophAppProxy.SyncState.Synced) {
        CylinderViewModel cy = (CylinderViewModel)sender;
        var lng = (ushort)cy.LNGInt;
        var rtn = (ushort)cy.RTNInt;
        MotionSystemMotorPosition[] pos = new[] {
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.RLNG, StepSize = 5, Value = lng },
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.RRTN, StepSize = 5, Value = rtn }
        };
        _mophApp.GoTo(pos);
      }
    }

    private void GA_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      var internalProp = !((CylinderPropertyChangedEventArgs)e).External;
      if (internalProp && _mophApp.State == MophAppProxy.SyncState.Synced) {
        CylinderViewModel cy = (CylinderViewModel)sender;
        var lng = (ushort)cy.LNGInt;
        var rtn = (ushort)cy.RTNInt;
        MotionSystemMotorPosition[] pos = new[] {
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.GALNG, StepSize = 5, Value = lng },
          new MotionSystemMotorPosition { Channel = (byte)ServoNumber.GARTN, StepSize = 5, Value = rtn }
        };
        _mophApp.GoTo(pos);
      }
    }

    private void OnCylinderPositionsChanged(IEnumerable<CylinderPosition> positions) {
      foreach (var pos in positions) {
        switch (pos.Cy) {
        default:
          break;
        case Cylinder.Upper:
          UP.LNGInt = pos.Lng;
          LO.RTNInt = pos.Rtn;
          break;
        case Cylinder.Lower:
          UP.LNGInt = pos.Lng;
          LO.RTNInt = pos.Rtn;
          break;
        case Cylinder.Platform:
          GA.LNGInt = pos.Lng;
          GA.RTNInt = pos.Rtn;
          break;
        }
      }
    }

  }
}
