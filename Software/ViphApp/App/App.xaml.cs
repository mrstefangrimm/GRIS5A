/* App.xaml.cs - ViphApp (C) motion phantom application.
 * Copyright (C) 2019-2020 by Stefan Grimm
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
 * along with the VirtualGris05 software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using ViphApp.App.Plugin;

namespace ViphApp.App {

  public partial class App : Application {  

    private CancellationTokenSource _cancellationTokenSource;
    private Common.Com.MophAppProxy _mophApp;

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);

      _cancellationTokenSource = new CancellationTokenSource();

      _mophApp = new Common.Com.MophAppProxy();

      var pluginFactory = new PluginFactory();

      string pluginPath = Environment.CurrentDirectory;
      
      var gris5aPluginCreator = pluginFactory.CreatePluginCreator(string.Format(@"{0}\ViphApp.Gris5a.dll", pluginPath));
      var no2PluginCreator = pluginFactory.CreatePluginCreator(string.Format(@"{0}\ViphApp.No2.dll", pluginPath));

      ObservableCollection<PluginPhantom> availablePhantoms = new ObservableCollection<PluginPhantom>() {
        new PluginPhantom("Gris5a", gris5aPluginCreator.CreatePhantomViewModel(), gris5aPluginCreator.CreateControlViewModel(_mophApp)),
        new PluginPhantom("No2", no2PluginCreator.CreatePhantomViewModel(), no2PluginCreator.CreateControlViewModel(_mophApp))
      };
      var mainViewModel = new UI.MainViewModel(_mophApp, availablePhantoms);

      var app = new UI.Views.MainWindow();
      app.DataContext = mainViewModel;

      var templ = pluginFactory.CreateTemplate(gris5aPluginCreator.GetPhantomViewModelType(), gris5aPluginCreator.GetPhantomViewType());
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = pluginFactory.CreateTemplate(gris5aPluginCreator.GetControlViewModelType(), gris5aPluginCreator.GetControlViewType());
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = pluginFactory.CreateTemplate(no2PluginCreator.GetPhantomViewModelType(), no2PluginCreator.GetPhantomViewType());
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = pluginFactory.CreateTemplate(no2PluginCreator.GetControlViewModelType(), no2PluginCreator.GetControlViewType());
      app.Resources.Add(templ.DataTemplateKey, templ);
      app.Closing += mainViewModel.OnClosing;
      app.Show();
    }

    protected override void OnExit(ExitEventArgs e) {
      base.OnExit(e);
      _cancellationTokenSource.Cancel();
      _mophApp.Dispose();
    }
  }
}
