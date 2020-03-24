/* PluginCreator.cs - ViphApp (C) motion phantom application.
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
using ViphApp.Common;
using ViphApp.Common.Com;
using ViphApp.Common.UI;
using ViphApp.Gris5a.UI;
using ViphApp.Gris5a.UI.Views;

namespace ViphApp.Gris5a {

  public class PluginCreator : IPluginCreator {

    public IPlugInPhantomViewModel CreatePhantomViewModel() {
      return new Gris5aPhantomViewModel();
    }
    public Type GetPhantomViewModelType() {
      return typeof(Gris5aPhantomViewModel);
    }
    public Type GetPhantomViewType() {
      return typeof(Gris5aPhantomView);
    }

    public IPlugInControlViewModel CreateControlViewModel(MophAppProxy mophApp) {
      return new Gris5aControlViewModel(mophApp);
    }
    public Type GetControlViewModelType() {
      return typeof(Gris5aControlViewModel);
    }
    public Type GetControlViewType() {
      return typeof(Gris5aControlView);
    }
  }
}
