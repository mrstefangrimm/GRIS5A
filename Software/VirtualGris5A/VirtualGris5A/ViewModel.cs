/* ViewModel.cs - Virtual GRIS5A (C) motion phantom application.
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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace VirtualGris5A {

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

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class ViewModel : INotifyPropertyChanged {

    private ICommand _cmdManualMovement;
    private ICommand _cmdPreSet;
    private PreSet _model;

    public ViewModel() {
      _model = new PreSet(this);
      _cmdManualMovement = new RelayCommand<object>(param => {
        _model.StopPreset();
      });
      ICommand preSetRelayCmd = new RelayCommand<string>(param => {
        int n;
        if (int.TryParse(param, out n)) {
          _model.StartPreset(n);
        }
      });
      _cmdPreSet = preSetRelayCmd;
    }

    public ICommand GoManual {
      get { return _cmdManualMovement; }
      set {
        _cmdManualMovement = value;
        _OnPropertyChanged("GoManual");
      }
    }

    public ICommand GoPreSet {
      get { return _cmdPreSet; }
      set {
        _cmdPreSet = value;
        _OnPropertyChanged("GoPreSet");
      }
    }  

    public Cylinder LU { get; private set; } = new Cylinder();
    public Cylinder RU { get; private set; } = new Cylinder();
    public Cylinder LL { get; private set; } = new Cylinder();
    public Cylinder RL { get; private set; } = new Cylinder();
    public Cylinder GA { get; private set; } = new Cylinder();

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void _OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

}