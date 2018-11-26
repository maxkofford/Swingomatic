using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceFlow
{

    public class LoadMoves : MonoBehaviour
    {
        private static int randomStartingState = 234567;

        [SerializeField]
        private Canvas positionsCanvas;

        [SerializeField]
        private Canvas movesCanvas;

        [SerializeField]
        private AllMovesScriptable movesScriptableHolder;

        [SerializeField]
        private PositionHolder positionHolder;

        [SerializeField]
        private MoveHolder moveHolder;

        [SerializeField]
        private Settings settings;

        public static AllMovesRuntime theMoves;

        enum LoadType
        {
            ScriptableObject,
            File,
            Database
                
        }

        [SerializeField]
        private LoadType loader;

        void Start()
        {
            Random.InitState(randomStartingState);
            AllMovesRuntime allMoves;

            switch (loader)
            {
                case LoadType.ScriptableObject:
                    allMoves = movesScriptableHolder.GetAllMovesRuntime();
                    break;
                case LoadType.File:
                    allMoves = AllMovesFile.GetAllMovesRuntime();
                    break;
                default:
                    allMoves = AllMovesFile.GetAllMovesRuntime();
                    break;
            }

            
            List<DancePositionRuntime> positions = new List<DancePositionRuntime>(allMoves.Positions);
            List<DanceMoveRuntime> moves = new List<DanceMoveRuntime>(allMoves.Moves);

            foreach (DancePositionRuntime position in positions)
            {
                //create a move icon and its connections
                PositionHolder currentPosition = Instantiate(positionHolder,positionsCanvas.transform);
                currentPosition.Initialize(position, settings);

            }

            foreach (DanceMoveRuntime move in moves)
            {
                MoveHolder currentMove = Instantiate(moveHolder, movesCanvas.transform);
                currentMove.Initialize(move, settings);
            }

            theMoves = allMoves;
        }


    }
   
}
