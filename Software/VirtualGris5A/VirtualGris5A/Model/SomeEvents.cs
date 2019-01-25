﻿/* SommEvents.cs - Virtual GRIS5A (C) motion phantom application.
 * Copyright (C) 2019 by Stefan Grimm
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

using MessagingLib;
using System.Runtime.Serialization;
using VirtualGris5A.MessagingUtil;

namespace VirtualGris5A.Model {

  namespace Ev {

    [DataContract]
    public class EvShutdown : IMessage {
      private static ushort MSGID = WireMessage.CreateMsgID();
      static EvShutdown() {
        MessageRegistry.Instance.Register(MSGID, typeof(EvShutdown));
      }
      public ushort MsgId => MSGID;
    }

    public enum Cylinder { LeftUpper, LeftLower, RightUpper, RightLower, Platform }

    namespace UI {

      [DataContract]
      public class EvCylinderMotion : IMessage {
        [DataMember] public Cylinder Cy;
        [DataMember] public ushort Lng;
        [DataMember] public ushort Rtn;

        private static ushort MSGID = WireMessage.CreateMsgID();
        static EvCylinderMotion() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvCylinderMotion));
        }
        public ushort MsgId => MSGID;
      }

      [DataContract]
      public class EvManualMotionClick : IMessage {
        private static ushort MSGID = WireMessage.CreateMsgID();
        static EvManualMotionClick() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvManualMotionClick));
        }
        public ushort MsgId => MSGID;
      }

      [DataContract]
      public class EvPresetMotionClick : IMessage {
        [DataMember] public ushort Num;

        private static ushort MSGID = WireMessage.CreateMsgID();
        static EvPresetMotionClick() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvPresetMotionClick));
        }
        public ushort MsgId => MSGID;
      }
    }

    namespace MotionSystem {

      [DataContract]
      public class EvConnect : IMessage {
        [DataMember] public string ComPort;

        private static readonly ushort MSGID = WireMessage.CreateMsgID();
        static EvConnect() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvConnect));
        }
        public ushort MsgId => MSGID;
      }

      [DataContract]
      public class EvDisconnect : IMessage {
        private static readonly ushort MSGID = WireMessage.CreateMsgID();
        static EvDisconnect() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvDisconnect));
        }
        public ushort MsgId => MSGID;
      }
      
      [DataContract]
      public class ServoAbsolutePosition {
        [DataMember] public ServoNumber Num;
        [DataMember] public ushort AbsPos;
      }

      [DataContract]
      public class EvServoAbsolutePositions : IMessage {
        [DataMember] public ServoAbsolutePosition[] Positions;

        private static ushort MSGID = WireMessage.CreateMsgID();
        static EvServoAbsolutePositions() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvServoAbsolutePositions));
        }
        public ushort MsgId => MSGID;
      }

      [DataContract]
      public class EvLogMessage : IMessage {
        [DataMember] public string Message;

        private static readonly ushort MSGID = WireMessage.CreateMsgID();
        static EvLogMessage() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvLogMessage));
        }
        public ushort MsgId => MSGID;
      }

    }

    namespace PreSet {

      [DataContract]
      public class EvCylinderPosition {
        [DataMember] public Cylinder Cy;
        [DataMember] public ushort Lng;
        [DataMember] public ushort Rtn;
        [DataMember] public ushort StepSize;
      }

      [DataContract]
      public class EvCylinderPositions : IMessage {
        [DataMember] public EvCylinderPosition[] Positions;

        private static ushort MSGID = WireMessage.CreateMsgID();
        static EvCylinderPositions() {
          MessageRegistry.Instance.Register(MSGID, typeof(EvCylinderPositions));
        }
        public ushort MsgId => MSGID;
      }

    }

  }
}
