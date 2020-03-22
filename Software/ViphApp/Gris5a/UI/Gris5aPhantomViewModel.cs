

using ViphApp.Common.UI;

namespace ViphApp.Gris5a.UI {

  public class Gris5aPhantomViewModel : Gris5aViewModel, IPlugInPhantomViewModel {

    public Gris5aPhantomViewModel() {
      LU.PropertyChanged += (sender, args) => { if (((CylinderPropertyChangedEventArgs)args).External) OnPropertyChanged(args.PropertyName); };
      LL.PropertyChanged += (sender, args) => { if (((CylinderPropertyChangedEventArgs)args).External) OnPropertyChanged(args.PropertyName); };
      RU.PropertyChanged += (sender, args) => { if (((CylinderPropertyChangedEventArgs)args).External) OnPropertyChanged(args.PropertyName); };
      RL.PropertyChanged += (sender, args) => { if (((CylinderPropertyChangedEventArgs)args).External) OnPropertyChanged(args.PropertyName); };
      GA.PropertyChanged += (sender, args) => { if (((CylinderPropertyChangedEventArgs)args).External) OnPropertyChanged(args.PropertyName); };
    }
  }
}
