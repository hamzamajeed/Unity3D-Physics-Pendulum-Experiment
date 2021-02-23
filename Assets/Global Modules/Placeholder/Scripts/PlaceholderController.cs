using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tajurbah_Gah
{
    public class PlaceholderController : MonoBehaviour
    {
        [Header("Next Object")]
        public GameObject nextObject;
        public bool isVisible = false;

        GameObject otherGameObject;
        ObjectProperties objectProperties;

        int flag = 0;

        private void Start()
        {
            if (!isVisible)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            otherGameObject = other.gameObject;

            while (otherGameObject.transform.parent != null)
            {
                if (otherGameObject.GetComponent<Tajurbah_Gah.ObjectProperties>())
                {
                    break;
                }
                else
                {
                    otherGameObject = otherGameObject.transform.parent.gameObject;
                }
            }
            objectProperties = otherGameObject.GetComponent<ObjectProperties>() ? otherGameObject.GetComponent<ObjectProperties>() : null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (otherGameObject.transform.tag == this.transform.tag && flag == 0 && Input.GetMouseButtonUp(0))
            {
                //SoundsManager.Instance.PlaySnapClickSound();

                flag = 1;
                PlaceholderManager.Instance.IncrementAppratusCount();
                otherGameObject.transform.position = this.transform.position;
                objectProperties.DetachObjectFromDrag();
                objectProperties.DisableDragDrop();
                objectProperties.ChangeRigidBodyToKinematic();

                this.gameObject.SetActive(false);

                if (nextObject != null)
                {
                    nextObject.SetActive(true);
                }
            }
        }
    }
}