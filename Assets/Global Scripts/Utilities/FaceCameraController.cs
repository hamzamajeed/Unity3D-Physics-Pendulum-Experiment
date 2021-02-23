using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tajurbah_Gah
{
    public class FaceCameraController : MonoBehaviour
    {
        Transform camera;

        private void Start()
        {
            camera = Camera.main.transform;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        }
    }  
}
