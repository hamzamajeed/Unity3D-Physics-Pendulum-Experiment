using UnityEngine;

namespace Tajurbah_Gah
{
    public class DragDropController : MonoBehaviour
    {
        private Vector3 mOffset;
        private float mZCoord;
        bool isDragging = false;

        ObjectProperties objectProperties;

        private void Start()
        {
            objectProperties = GetComponent<ObjectProperties>() ? GetComponent<ObjectProperties>() : null;
        }

        public void OnMouseDown()
        {
            if (this.enabled)
            {
                //transform.position = GetMouseAsWorldPoint();
                isDragging = true;
                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
                transform.position = GetMouseAsWorldPoint() + mOffset;

                objectProperties?.ChangeRigidBodyToKinematic();
                objectProperties?.HighlightObjectOutline();
            }
        }

        private void Update()
        {
            if (isDragging)
            {
                transform.position = GetMouseAsWorldPoint() + mOffset;

                if (Input.GetMouseButtonUp(0))
                {
                    DetachFromDrag();
                }
            }
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = mZCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        public void DetachFromDrag()
        {
            isDragging = false;
            objectProperties?.ChangeRigidBodyToDynamic();
            objectProperties?.UnHighlightObjectOutline();
        }
    }
}