using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{

    public class SaveButton : MonoBehaviour
    {

        public void Save()
        {
            AllMovesFile.SaveAllMovesRuntime();
        }
    }
}