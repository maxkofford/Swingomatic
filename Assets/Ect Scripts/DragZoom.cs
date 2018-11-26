using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace DanceFlow
{

    [RequireComponent(typeof(Image))]
    public class DragZoom : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool dragOnSurfaces = true;


        private RectTransform m_DraggingPlane;

        public delegate void DragEvent();

        public event DragEvent DragStartHandler;
        public event DragEvent DragEndHandler;

       

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = this.GetComponent<Image>().canvas;
            if (canvas == null)
                return;

            if (dragOnSurfaces)
                m_DraggingPlane = transform as RectTransform;
            else
                m_DraggingPlane = canvas.transform as RectTransform;

            SetDraggedPosition(eventData);

            DragStartHandler();
        }

        public void OnDrag(PointerEventData data)
        {
            if (gameObject != null)
                SetDraggedPosition(data);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEndHandler();
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = gameObject.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = m_DraggingPlane.rotation;
            }
        }
        
    }
}