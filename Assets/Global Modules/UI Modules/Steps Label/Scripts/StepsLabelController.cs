using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Tajurbah_Gah
{
    public class StepsLabelController : MonoBehaviour
    {
        public static StepsLabelController Instance;

        public Action callBack;

        [Header("Template to copy")]
        [SerializeField] GameObject stepTextTemplate;

        [Header("Steps Content Rect")]
        [SerializeField] GameObject stepsContentRect;

        [Header("STEPS Button")]
        [SerializeField] Button stepsButton;

        [Header("Open and Close Values")]
        RectTransform rectTransform;
        [SerializeField] float openValueX = -12.5f;
        [SerializeField] float closeValueX;
        [SerializeField] float speed = 2f;

        [Header("Should open Steps Tab in Start")]
        [SerializeField] bool openStepsTab = true;

        [Header("Normal and Highlight Color of Text")]
        [SerializeField] Color normalColor;
        [SerializeField] Color highLightColor;

        [Header("Steps Data")]
        [SerializeField] StepsLabelsMessages[] steps;

        private void Awake()
        {
            Instance = this;
            stepsButton.onClick.AddListener(OpenCloseStepsTab);
        }

        private void Start()
        {
            stepTextTemplate.SetActive(false);

            rectTransform = GetComponent<RectTransform>();

            for (int i = 0; i < steps.Length; i++)
            {
                GameObject temp = Instantiate(stepTextTemplate);
                temp.SetActive(true);
                temp.transform.SetParent(stepsContentRect.transform, false);
                temp.name = "Step " + (i + 1).ToString();
                steps[i].stepText = temp.GetComponent<TextMeshProUGUI>();
                steps[i].stepText.text = steps[i].step;
                steps[i].stepText.color = normalColor;
            }


            OpenCloseStepsTab();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.U))
            {
                UpdateStep(0, false);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                UpdateStep(0, true);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                UpdateStep(1, false);
            }
        }

        void OpenCloseStepsTab()
        {
            StopAllCoroutines();

            if (openStepsTab)
            {
                StartCoroutine(ExpandStepsTabRoutine());
            }
            else
            {
                StartCoroutine(CloseStepsTabRoutine());
            }
            openStepsTab = !openStepsTab;
        }

        IEnumerator CloseStepsTabRoutine()
        {
            while (true)
            {
                if (rectTransform.anchoredPosition.x == closeValueX)
                {
                    break;
                }
                else
                {
                    rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(closeValueX, rectTransform.anchoredPosition.y), speed * Time.unscaledDeltaTime);
                }
                yield return null;
            }
        }
        IEnumerator ExpandStepsTabRoutine()
        {
            while (true)
            {
                if (rectTransform.anchoredPosition.x == openValueX)
                {
                    break;
                }
                else
                {
                    rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(openValueX, rectTransform.anchoredPosition.y), speed * Time.unscaledDeltaTime);
                }
                yield return null;
            }
        }

        public void UpdateStep(int index, bool StepCompleted = false, Action CallBack = null)
        {
            if (!StepCompleted)
            {
                openStepsTab = true;
                OpenCloseStepsTab();
                steps[index].stepText.color = highLightColor;
            }
            if(StepCompleted)
            {
                openStepsTab = true;
                OpenCloseStepsTab();
                steps[index].stepText.color = normalColor;
                steps[index].stepText.text = "<s><i>" + steps[index].stepText.text + "</i></s>";
            }

            if (CallBack != null)
            {
                CallBack.Invoke();
            }
        }

     /*   public void ShowMessage(int index, Action CallBack = null)
        {

        }*/
    }
}

[System.Serializable]
public class StepsLabelsMessages
{
    [HideInInspector]
    public TextMeshProUGUI stepText;
    [TextArea]
    public string step;
}
