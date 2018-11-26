using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{

    public class ArrowOrienter : MonoBehaviour
    {
        [SerializeField]
        private Transform TargetStartPoint;

        [SerializeField]
        private Transform TargetEndPoint;

        [SerializeField]
        private bool ShouldUpdate = false;

        private RectTransform imageRectTransform;

        void Start()
        {
            imageRectTransform = this.transform as RectTransform;
        }

        // Update is called once per frame
        void Update()
        {
            if (ShouldUpdate)
            {
                RefreshArrow();
            }
        }


        public void RefreshArrow()
        {
            if (imageRectTransform == null)
                imageRectTransform = this.transform as RectTransform;

            Vector3 differenceVector = TargetStartPoint.position - TargetEndPoint.position;


            // imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude / imageRectTransform.lossyScale.x, lineWidth);

            //imageRectTransform.pivot = pivot;
            RaycastHit2D[] results = new RaycastHit2D[100];
           // Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance = Mathf.Infinity);
            Physics2D.Raycast(TargetStartPoint.transform.position, differenceVector,new ContactFilter2D(),results);

            //LayerMask.NameToLayer("");

            imageRectTransform.position = TargetStartPoint.position + ((TargetEndPoint.position - TargetStartPoint.position)* 3) / 4;
            float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            imageRectTransform.localRotation = Quaternion.Euler(0, 0, angle+90);
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
