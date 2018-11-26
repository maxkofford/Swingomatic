using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    public class DanceMoveRuntime
    {
        public DancePositionRuntime LeftPosition
        {
            get;
            set;
        }
        public DancePositionRuntime RightPosition
        {
            get;
            set;
        }


        public DanceMoveRuntime(DancePositionScriptable leftPosition, DancePositionScriptable rightPosition)
        {
            this.LeftPosition = leftPosition.GetRuntimePosition();
            this.RightPosition = rightPosition.GetRuntimePosition();
            
        }

        public DanceMoveRuntime()
        {

        }

        public MoveHolder MyHolder
        {
            get;
            set;
        }
    }
}
