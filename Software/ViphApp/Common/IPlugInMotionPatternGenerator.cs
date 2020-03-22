using System;

namespace ViphApp.Common {
  
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
