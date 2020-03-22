/* App.xaml.cs - Virtual Phantom (C) motion phantom application.
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
using System.Windows.Markup;

namespace ViphApp.App {

  public partial class App : Application {  

    private CancellationTokenSource _cancellationTokenSource;
    private Common.Com.MophAppProxy _mophApp;

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);

      _cancellationTokenSource = new CancellationTokenSource();

      _mophApp = new Common.Com.MophAppProxy();

      var gris5aPhantom = new Gris5a.UI.Gris5aPhantomViewModel();
      var gris5aControls = new Gris5a.UI.Gris5aControlViewModel(_mophApp);
      var no2Phantom = new No2.UI.No2PhantomViewModel();
      var no2Controls = new No2.UI.No2ControlViewModel(_mophApp);
      ObservableCollection<PluginPhantom> availablePhantoms = new ObservableCollection<PluginPhantom>() {
        new PluginPhantom("Gris5a", gris5aPhantom, gris5aControls),
        new PluginPhantom("No2", no2Phantom, no2Controls)
      };
      var mainViewModel = new UI.MainViewModel(_mophApp, availablePhantoms);

      var app = new UI.Views.MainWindow();
      app.DataContext = mainViewModel;

      // https://www.ikriv.com/dev/wpf/DataTemplateCreation/
      // https://www.ikriv.com/dev/wpf/DataTemplateCreation/DataTemplateManager.cs
      //var manager = new DataTemplateManager();
      //manager.RegisterDataTemplate<ViewModelA, ViewA>();
      //manager.RegisterDataTemplate<ViewModelB, ViewB>();
      var templ = CreateTemplate(typeof(Gris5a.UI.Gris5aPhantomViewModel), typeof(Gris5a.UI.Views.Gris5aPhantomView));
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = CreateTemplate(typeof(Gris5a.UI.Gris5aControlViewModel), typeof(Gris5a.UI.Views.Gris5aControlView));
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = CreateTemplate(typeof(No2.UI.No2PhantomViewModel), typeof(No2.UI.Views.No2PhantomView));
      app.Resources.Add(templ.DataTemplateKey, templ);
      templ = CreateTemplate(typeof(No2.UI.No2ControlViewModel), typeof(No2.UI.Views.No2ControlView));
      app.Resources.Add(templ.DataTemplateKey, templ);
      app.Closing += mainViewModel.OnClosing;
      app.Show();
    }

    protected override void OnExit(ExitEventArgs e) {
      base.OnExit(e);
      _cancellationTokenSource.Cancel();
      _mophApp.Dispose();
    }

    DataTemplate CreateTemplate(Type viewModelType, Type viewType) {
      const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
      var xaml = String.Format(xamlTemplate, viewModelType.Name, viewType.Name, viewModelType.Namespace, viewType.Namespace);

      var context = new ParserContext();

      context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
      context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
      context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

      context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
      context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
      context.XmlnsDictionary.Add("vm", "vm");
      context.XmlnsDictionary.Add("v", "v");

      var template = (DataTemplate)XamlReader.Parse(xaml, context);
      return template;
    }
  }
}
