using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    /// <summary>
    /// Represents one persons personal preference for different moves and positions
    /// </summary>
    public class DancePersonStyleRuntime
    {
        public class DanceMoveWeights
        {
            public float Weight
            {
                get;
                set;
            }
            public DanceMoveRuntime TargetMove
            {
                get;
                set;
            }
        }

        public class DancePositionWeights
        {
            public float Weight
            {
                get;
                set;
            }
            public DancePositionRuntime TargetPosition
            {
                get;
                set;
            }
        }

        public string PersonName
        {
            get;
            set;
        }

        public List<DanceMoveWeights> myMoveWeights = new List<DanceMoveWeights>();
        public List<DancePositionWeights> myPositionWeights = new List<DancePositionWeights>();

    }
}