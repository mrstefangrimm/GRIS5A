using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ViphApp.Common.UI;

namespace ViphApp.App.UI {

  public class AppSettingsViewModel : INotifyPropertyChanged {

    private MainViewModel _parent;
    private PluginPhantom _selectedPhantom;

    public AppSettingsViewModel(MainViewModel parent, ObservableCollection<PluginPhantom> availablePhantoms) {
      _parent = parent;
      AvailablePhantoms = availablePhantoms;
      _selectedPhantom = availablePhantoms[0];
    }

    public ObservableCollection<PluginPhantom> AvailablePhantoms { get; private set; }

    public PluginPhantom SelectedPhantom {
      get {
        return _selectedPhantom;
      }
      set {
        if (_selectedPhantom != value) {
          _selectedPhantom = value;
          _parent.Phantom = value.Phantom;
          _parent.Control = value.Control;
        }
      }
    }

    public ICommand DoShowSettingsDetails {
      get {
        return new RelayCommand<object>(param => {
          if (_parent.AppSettingsViewState == AppSettingsViewState.Minimized) {
            _parent.AppSettingsViewState = AppSettingsViewState.Details;
          }
          else {
            _parent.AppSettingsViewState = AppSettingsViewState.Minimized;
          }
        });
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
