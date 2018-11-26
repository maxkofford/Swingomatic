using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DanceFlow
{

    public class ChangeScale : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> thingsToScale;

        [SerializeField]
        private Slider slider;

        public void Scale(float newValue)
        {
            foreach (Transform t in thingsToScale)
            {
                t.localScale = new Vector3(newValue * 2, newValue * 2, newValue * 2);
            }
        }

        public void Update()
        {
            var d = Input.GetAxis("Mouse ScrollWheel");
            if (d > 0f)
            {
                if (slider.value < 1)
                    slider.value = slider.value + .05f;
                    
            }
            else if (d < 0f)
            {
                if (slider.value > .05)
                    slider.value = slider.value - .05f;
            }
        }
    }
}