using ViphApp.Common.UI;

namespace ViphApp.App
 {

  public class PluginPhantom : IPluginPhantom {
    
    public PluginPhantom(string name, IPlugInPhantomViewModel phantom, IPlugInControlViewModel control) {
      Name = name;
      Phantom = phantom;
      Control = control;
    }

    public string Name { get; private set; }

    public IPlugInPhantomViewModel Phantom { get; private set; }

    public IPlugInControlViewModel Control { get; private set; }
  }
}
