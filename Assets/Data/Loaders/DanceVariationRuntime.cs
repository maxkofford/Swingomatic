using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    /// <summary>
    /// A variation for one move (aka same move but slightly different positions/speed/flirtyness) 
    /// </summary>
    public class DanceVariationRuntime
    {
        public DanceMoveRuntime BaseMove
        {
            get;
            set;
        }

        public string VariationName
        {
            get;
            set;
        }
    }
}