using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace DanceFlow
{


    [CreateAssetMenu(fileName = "DanceMoveV1", menuName = "DanceMoveV1", order = 1)]
    public class DanceMoveScriptable : ScriptableObject
    {

        [SerializeField]
        private DancePositionScriptable leftPosition;

        [SerializeField]
        private DancePositionScriptable rightPosition;

        [SerializeField]
        private string moveName;

        private DanceMoveRuntime runtimeMove;

        public DanceMoveRuntime GetRunTimeMove()
        {
            if(runtimeMove == null)
                runtimeMove = new DanceMoveRuntime(leftPosition, rightPosition);

            return runtimeMove;
        }

    }

   
}