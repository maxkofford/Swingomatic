using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    public class AllMovesRuntime
    {
        public List<DancePositionRuntime> Positions;
        public List<DanceMoveRuntime> Moves;

        public AllMovesRuntime(List<DancePositionRuntime> positions, List<DanceMoveRuntime> moves)
        {
            this.Positions = positions;
            this.Moves = moves;
        }
    }
}
