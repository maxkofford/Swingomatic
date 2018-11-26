using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace DanceFlow
{


    [CreateAssetMenu(fileName = "DancePositionV1", menuName = "DancePositionV1", order = 1)]
    public class DancePositionScriptable : ScriptableObject
    {

        [SerializeField]
        private string positionName;

        private DancePositionRuntime runtimePosition;

        public DancePositionRuntime GetRuntimePosition()
        {
            if (runtimePosition == null)
                 runtimePosition = new DancePositionRuntime(positionName);

            return runtimePosition;
        }
    }

   
}