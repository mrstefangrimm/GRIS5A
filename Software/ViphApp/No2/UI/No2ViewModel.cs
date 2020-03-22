using System.ComponentModel;
using System.Runtime.CompilerServices;
using ViphApp.Common.UI;

namespace ViphApp.No2.UI {

  public class No2ViewModel : INotifyPropertyChanged {

    private static CylinderViewModel _left = new CylinderViewModel();
    private static CylinderViewModel _right = new CylinderViewModel();
    private static CylinderViewModel _gating = new CylinderViewModel();

    public string Name { get { return "No2"; } }

    public CylinderViewModel L { get { return _left; } }
    public CylinderViewModel R { get { return _right; } }
    public CylinderViewModel GA { get { return _gating; } }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
