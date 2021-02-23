using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P9_Dynamics_3_1
{
    public class numpadController : MonoBehaviour
    {
        public InputField NumPadInput;
        public GameObject NumPad;
        public float Limit = 10f;

        private void Start()
        {
            if (NumPadInput != null)
            {
                NumPadInput.onValueChanged.AddListener(LimitReached);
            }
        }

        public void NumPadClick(string digit)
        {
            try
            {
                if (digit == "<")
                {
                    if (!string.IsNullOrEmpty(NumPadInput.text.ToString()))
                    {
                        NumPadInput.text = NumPadInput.text.Remove(NumPadInput.text.Length - 1, 1);
                    }
                }
                else
                {
                    NumPadInput.text = NumPadInput.text + digit;
                    LimitReached(NumPadInput.text);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        void LimitReached(string temp = null)
        {
            if (!string.IsNullOrEmpty(NumPadInput.text.ToString()))
            {

                if (Mathf.Abs(float.Parse(NumPadInput.text.ToString())) > Limit)
                {
                    NumPadInput.text = NumPadInput.text.Remove(NumPadInput.text.Length - 1, 1);
                }
            }
        }

        public void AddListnerInputField()
        {
            NumPadInput.onValueChanged.AddListener(LimitReached);
        }

        public void AssignInputField(InputField Object)
        {
            NumPadInput = Object;
        }

        public void OpenNumPad()
        {
            NumPad.SetActive(true);
        }
        public void CloseNumPad()
        {
            NumPad.SetActive(false);
        }
    }
}
