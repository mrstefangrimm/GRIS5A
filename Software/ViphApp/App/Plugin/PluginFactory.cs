/* PluginFactory.cs - ViphApp (C) motion phantom application.
 * Copyright (C) 2020 by Stefan Grimm
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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using ViphApp.Common;

namespace ViphApp.App.Plugin {

  class PluginFactory {

    public IPluginCreator CreatePluginCreator(string pluginAsmName) {
      var asm = Assembly.LoadFile(pluginAsmName);
      var allTypes = asm.GetTypes();
      foreach (Type clType in asm.GetTypes()) {
        var implIf = clType.GetInterfaces().Any(i => i == typeof(IPluginCreator));
        if (implIf) {
          return Activator.CreateInstance(clType) as IPluginCreator;
        }
      }
      return null;
    }

    public DataTemplate CreateTemplate(Type viewModelType, Type viewType) {

      // https://www.ikriv.com/dev/wpf/DataTemplateCreation/
      // https://www.ikriv.com/dev/wpf/DataTemplateCreation/DataTemplateManager.cs
      //var manager = new DataTemplateManager();
      //manager.RegisterDataTemplate<ViewModelA, ViewA>();
      //manager.RegisterDataTemplate<ViewModelB, ViewB>();

      const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><v:{1} /></DataTemplate>";
      var xaml = string.Format(xamlTemplate, viewModelType.Name, viewType.Name, viewModelType.Namespace, viewType.Namespace);

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
