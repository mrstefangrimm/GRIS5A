﻿/* IPlugInMotionPatternGenerator.cs - ViphApp (C) motion phantom application.
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

namespace ViphApp.Common.Plugin {
  
  public enum MotionPatternGeneratorState {
    Stopped,
    Running
  }

  public class MotionPatternGeneratorStateEventArgs : EventArgs {
    MotionPatternGeneratorStateEventArgs(MotionPatternGeneratorState state) {
      State = state;
    }
    public MotionPatternGeneratorState State { get; private set; }
  }

  public class MotionPatternGeneratorPositionEventArgs : EventArgs {

  }
  
  interface IPlugInMotionPatternGenerator {
    event EventHandler<MotionPatternGeneratorStateEventArgs> StateChanged;
    event EventHandler<MotionPatternGeneratorPositionEventArgs> PositionChanged;
  }
}