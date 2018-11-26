using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DanceFlow
{

    public class LineConnector : MonoBehaviour
    {
        [SerializeField]
        private Transform TargetStartPoint;

        [SerializeField]
        private Transform TargetEndPoint;
        
        [SerializeField]
        private bool ShouldUpdate = false;

        public float lineWidth = 10;

        [SerializeField]
        private List<Image> lineImages;

        private RectTransform imageRectTransform;

        private Vector2 pivot = new Vector2(0, 0.5f);

        void Start()
        {
            imageRectTransform = this.transform as RectTransform;
        }

        // Update is called once per frame
        void Update()
        {
            if (ShouldUpdate)
            {
                RefreshLine();             
            }
        }


        public void SetColor(Color newColor)
        {
            foreach (Image i in lineImages)
            {
                i.color = newColor;
            }
        }

        public void RefreshLine()
        {       
            if(imageRectTransform == null)
                imageRectTransform = this.transform as RectTransform;

            Vector3 differenceVector = TargetStartPoint.position - TargetEndPoint.position;

            
            imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude/imageRectTransform.lossyScale.x, lineWidth);
            
            imageRectTransform.pivot = pivot;
            imageRectTransform.position = TargetEndPoint.position;
            float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            imageRectTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetShouldUpdate(bool shouldUpdate)
        {
            ShouldUpdate = shouldUpdate;
        }

        public void SetStartPoint(Transform start)
        {
            TargetStartPoint = start;
        }

        public void SetEndPoint(Transform end)
        {
            TargetEndPoint = end;
        }
    }
}
