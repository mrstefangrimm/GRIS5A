using ViphApp.Common.UI;

namespace ViphApp.No2.UI {

  public class No2PhantomViewModel : No2ViewModel, IPlugInPhantomViewModel {

    public No2PhantomViewModel() {
      L.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      R.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
      GA.PropertyChanged += (sender, arg) => OnPropertyChanged(arg.PropertyName);
    }

  }
}
