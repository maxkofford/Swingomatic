using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    public class DanceMoveRuntime
    {
        public string DanceMoveName
        {
            get;
            set;
        }

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

        public List<DanceVariationRuntime> variations = new List<DanceVariationRuntime>();

        
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
