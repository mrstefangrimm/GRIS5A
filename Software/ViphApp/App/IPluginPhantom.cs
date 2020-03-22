using ViphApp.Common.UI;

namespace ViphApp.App
{
  public interface IPluginPhantom {
    string Name { get; }
    IPlugInPhantomViewModel Phantom { get; }
    IPlugInControlViewModel Control { get; }
  }
}
