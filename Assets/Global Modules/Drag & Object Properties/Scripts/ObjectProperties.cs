using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tajurbah_Gah
{
    public class ObjectProperties : MonoBehaviour
    {
        [Header("Reference of All Components")]
        [SerializeField] Rigidbody objectRigidBody;
        [SerializeField] Outline[] objectHighlightOutline;

        [Header("Highlight on Start?")]
        [SerializeField] bool highlightOnStart = true;

        [Header("Disable Object?")]
        [SerializeField] bool disableOnFloor = true;
        [ShowWhen("disableOnFloor", false)]
        [SerializeField] bool destroyOnFloor = false;

        [Header("Highlight Color & Width")]
        [SerializeField] Color highlightColor;
        [SerializeField] float highlightWidth = 10;

        DragDropController dragDropController;

        private void Start()
        {
            dragDropController = GetComponent<DragDropController>() ? GetComponent<DragDropController>() : null;

            foreach(Outline outline in objectHighlightOutline)
            {
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = highlightColor;
                outline.OutlineWidth = highlightWidth;
            }

            if (!highlightOnStart)
            {
                UnHighlightObjectOutline();
            }
        }

        private void OnValidate()
        {
            if(disableOnFloor)
            {
                destroyOnFloor = false;
            }
        }

        private void OnEnable()
        {
            ChangeRigidBodyToKinematic();
        }

        public void HighlightObjectOutline()
        {
            foreach (Outline outline in objectHighlightOutline)
            {
                outline.enabled = true;
            }
        }
        public void UnHighlightObjectOutline()
        {
            foreach (Outline outline in objectHighlightOutline)
            {
                outline.enabled = false;
            }
        }

        public void ChangeRigidBodyToKinematic()
        {
            objectRigidBody.isKinematic = true;
            objectRigidBody.useGravity = false;
        }
        public void ChangeRigidBodyToDynamic()
        {
            objectRigidBody.isKinematic = false;
            objectRigidBody.useGravity = true;
        }

        public void DetachObjectFromDrag()
        {
            dragDropController?.DetachFromDrag();
        }

        public void DisableDragDrop()
        {
            dragDropController.enabled = false;
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                if(destroyOnFloor)
                {
                    Destroy(this.gameObject);
                    return;
                }
                if (!disableOnFloor)
                {
                    return;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                if (destroyOnFloor)
                {
                    Destroy(this.gameObject);
                    return;
                }
                if (!disableOnFloor)
                {
                    return;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
