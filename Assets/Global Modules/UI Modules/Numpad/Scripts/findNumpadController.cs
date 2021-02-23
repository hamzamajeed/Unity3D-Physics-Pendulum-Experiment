using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace P9_Dynamics_3_1
{
    public class findNumpadController : MonoBehaviour,IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            OpenNumpadAndAssignToInput();
        }

        void OpenNumpadAndAssignToInput()
        {
            numpadController Object;
            if (FindObjectOfType<numpadController>())
            {
                Object = FindObjectOfType<numpadController>();
                Object.OpenNumPad();
                Object.AssignInputField(GetComponent<InputField>());
                Object.AddListnerInputField();
            }
        }
    }
}
