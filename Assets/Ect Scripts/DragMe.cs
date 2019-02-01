using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace DanceFlow
{

    [RequireComponent(typeof(Image))]
    public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler, IPointerClickHandler
    {
        public bool dragOnSurfaces = true;


        private RectTransform m_DraggingPlane;

        public delegate void DragEvent();

        public event DragEvent DragStartHandler;
        public event DragEvent DragEndHandler;
        private Vector3 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = this.GetComponent<Image>().canvas;
            if (canvas == null)
                return;

            if (dragOnSurfaces)
                m_DraggingPlane = transform as RectTransform;
            else
                m_DraggingPlane = canvas.transform as RectTransform;

           // SetOffset(eventData);
            SetDraggedPosition(eventData);

            if(DragStartHandler!= null)
            DragStartHandler();
        }

        public void OnDrag(PointerEventData data)
        {
            if (gameObject != null)
                SetDraggedPosition(data);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            offset = Vector3.zero;
            if (DragEndHandler != null)
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
                rt.position = globalMousePos-offset;

            }
        }

        private void SetOffset(PointerEventData data)
        {
            if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = gameObject.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                offset = globalMousePos - rt.position;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetOffset(eventData);
        }
    }
}