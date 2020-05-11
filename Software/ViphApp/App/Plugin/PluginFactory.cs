﻿/* PluginFactory.cs - ViphApp (C) motion phantom application.
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
 * along with the ViphApp software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

using System;
using System.Linq;
using System.Reflection;
using ViphApp.Common.Plugin;

namespace ViphApp.App.Plugin {

  class PluginFactory {

    public IPluginBuilder CreatePluginBuilder(string pluginAsmName) {
      var asm = Assembly.LoadFile(pluginAsmName);
      var allTypes = asm.GetTypes();
      foreach (Type clType in asm.GetTypes()) {
        var implIf = clType.GetInterfaces().Any(i => i == typeof(IPluginBuilder));
        if (implIf) {
          return Activator.CreateInstance(clType) as IPluginBuilder;
        }
      }
      return null;
    }
  }
}
