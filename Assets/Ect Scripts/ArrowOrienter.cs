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

        [SerializeField]
        private float angleAdjust = 0;

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

            Vector3 directionVector = TargetStartPoint.position - TargetEndPoint.position;

            RaycastHit2D[] results = new RaycastHit2D[100];
           
            int amount = Physics2D.Raycast(TargetStartPoint.position, -1 * directionVector.normalized, new ContactFilter2D(),results);

            if(amount > 0)
            foreach (RaycastHit2D result in results)
            {
                if (result.collider != null && result.collider.transform == TargetEndPoint.transform)
                {
                    imageRectTransform.position = result.point;
                }
            }

            //imageRectTransform.position = TargetStartPoint.position + ((TargetEndPoint.position - TargetStartPoint.position)* 3) / 4;
            float angle =  Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg - angleAdjust;
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
