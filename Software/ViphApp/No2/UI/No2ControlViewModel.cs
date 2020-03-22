
using System.ComponentModel;
using System.Windows.Input;
using ViphApp.Common.Com;
using ViphApp.Common.UI;

namespace ViphApp.No2.UI {

  public enum ControlViewState {
    Manual,
    Automatic,
    Minimized
  }

  public class No2ControlViewModel : No2ViewModel, IPlugInControlViewModel {

    private MophAppProxy _mophApp;
    private ControlViewState _viewState;

    public No2ControlViewModel(MophAppProxy mophApp) {
      _mophApp = mophApp;
      ControlViewState = ControlViewState.Manual;

      L.PropertyChanged += L_PropertyChanged;
      R.PropertyChanged += R_PropertyChanged;
      GA.PropertyChanged += GA_PropertyChanged;

      L.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      R.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      GA.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
    }

    public ControlViewState ControlViewState {
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
          ControlViewState = ControlViewState.Manual;
        });
      }
    }

    public ICommand DoSetAutomatic {
      get {
        return new RelayCommand<object>(param => {
          ControlViewState = ControlViewState.Automatic;
        });
      }
    }

    public ICommand DoSetMinimized {
      get {
        return new RelayCommand<object>(param => {
          if (ControlViewState == ControlViewState.Minimized) {
            ControlViewState = ControlViewState.Manual;
          }
          else {
            ControlViewState = ControlViewState.Minimized;
          }
        });
      }
    }

    public ICommand DoStopAutoProgram {
      get {
        return new RelayCommand<object>(param => {
          _mophApp.FreeMem();
          //var msg = new EvManualMotionClick();
          //Mediator.OnEvManualMotionClick(msg);
          //var mqmsg = WireMessage.Serialize(msg);
          //using (var frame = new ZFrame(mqmsg)) { _mqOutgoging.Send(frame); }
        });
      }
    }

    public ICommand DoAutoProgram {
      get {
        return new RelayCommand<string>(param => {
          ushort n;
          if (ushort.TryParse(param, out n)) {
            //var msg = new EvPresetMotionClick();
            //msg.Num = n;
            //Mediator.OnEvPresetMotionClick(msg);
            //var mqmsg = WireMessage.Serialize(msg);
            //_mqOutgoging.Send(new ZFrame(mqmsg));
          }
        });
      }
    }

    INotifyPropertyChanged IPlugInControlViewModel.GA => GA;

    private void L_PropertyChanged(object sender, PropertyChangedEventArgs e) {
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

    private void R_PropertyChanged(object sender, PropertyChangedEventArgs e) {
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
  }
}
