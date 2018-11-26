using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DanceFlow
{


    [CreateAssetMenu(fileName = "AllMovesV1", menuName = "AllMovesV1", order = 1)]
    public class AllMovesScriptable : ScriptableObject
    {
        [SerializeField]
        private List<DancePositionScriptable> positions;

        [SerializeField]
        private List<DanceMoveScriptable> moves;

        private AllMovesRuntime runtimeAllMoves;

        public AllMovesRuntime GetAllMovesRuntime()
        {
            if (runtimeAllMoves == null)
            {
                List<DancePositionRuntime> runtimePositions = new List<DancePositionRuntime>();
                List<DanceMoveRuntime> runtimeMoves = new List<DanceMoveRuntime>();

                foreach (DancePositionScriptable position in positions)
                {
                    runtimePositions.Add(position.GetRuntimePosition());
                }

                foreach (DanceMoveScriptable move in moves)
                {
                    var currentRuntime = move.GetRunTimeMove();
                    runtimeMoves.Add(currentRuntime);
                    currentRuntime.LeftPosition.moves.Add(currentRuntime);
                    currentRuntime.RightPosition.moves.Add(currentRuntime);
                }


                runtimeAllMoves = new AllMovesRuntime(runtimePositions, runtimeMoves);
            }

            return runtimeAllMoves;
        }
    }

    
}