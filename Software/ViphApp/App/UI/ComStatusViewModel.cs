using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ViphApp.Common.Com;
using ViphApp.Common.UI;

namespace ViphApp.App.UI {

  public class ComStatusViewModel : INotifyPropertyChanged {

    private MainViewModel _parent;
    private MophAppProxy _mophApp;
    private bool _isConnected;

    public ComStatusViewModel(MainViewModel parent, MophAppProxy mophApp) {
      _parent = parent;
      _mophApp = mophApp;
      _mophApp.LogOutput += OnLogOutput;
      SSerialPorts = "None";
      SerialPorts.Add(SSerialPorts);
      foreach (var port in SerialPort.GetPortNames()) {
        SerialPorts.Add(port);
      }
    }

    public ObservableCollection<string> SerialPorts { get; } = new ObservableCollection<string>();
    public string SSerialPorts { get; set; }
    public string LogOutput { get; private set; } = string.Empty;
    public string CommandRegister { get; set; } = "0";

    public bool IsConnected {
      get { return _isConnected; }
      set {
        if (_isConnected != value) {
          _isConnected = value;
          if (_isConnected) {
            _mophApp.Connect(SSerialPorts);
          }
          else {
            _mophApp.Disconnect();
          }
        }
      }
    }

    public ICommand DoShowStatusDetails {
      get {
        return new RelayCommand<object>(param => {
          if (_parent.ComStatusViewState == ComStatusViewState.Minimized) {
            _parent.ComStatusViewState = ComStatusViewState.Details;
          }
          else {
            _parent.ComStatusViewState = ComStatusViewState.Minimized;
          }
          OnPropertyChanged("SSerialPorts");
          OnPropertyChanged("IsConnected");
        });
      }
    }

    public ICommand DoShowStatusMaximized {
      get {
        return new RelayCommand<object>(param => {
          if (_parent.MainViewState == MainViewState.StatusViewMaximized) {
            _parent.ComStatusViewState = ComStatusViewState.Details;
            _parent.MainViewState = MainViewState.Normal;
          }
          else {
            _parent.ComStatusViewState = ComStatusViewState.Maximized;
            _parent.MainViewState = MainViewState.StatusViewMaximized;
          }
          OnPropertyChanged("SSerialPorts");
          OnPropertyChanged("IsConnected");
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

    public ICommand DoSetCommandRegister {
      get {
        return new RelayCommand<object>(param => {
          int cmd;
          if (int.TryParse(CommandRegister, out cmd)) {
            if (cmd > 0 && cmd < 256) {
              _mophApp.SetCommandRegister((byte)cmd);
            }
          }
        });
      }
    }    

    internal void OnLogOutput(object sender, LogOutputEventArgs args) {
      LogOutput = LogOutput.Insert(0, args.Text + Environment.NewLine);
      OnPropertyChanged("LogOutput");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
