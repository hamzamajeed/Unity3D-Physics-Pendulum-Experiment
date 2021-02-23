using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tajurbah_Gah
{
    public class StopwatchController : MonoBehaviour
    {
        public static StopwatchController Instance;

        public Action PlayButtonClickedCallBack;
        public Action AutomaticTimerCallBack;

        [Header("Hours? Minutes? Seconds? MiliSeconds")]
        [SerializeField] bool hours = false;
        [SerializeField] bool minutes = false;
        [SerializeField] bool seconds = false;
        [SerializeField] bool milliSeconds = false;

        [Header("Stopwatch Display Texts")]
        [SerializeField] Text stopwatchTimeText;
        [SerializeField] Text stopwatchAutomaticTimeText;
        [SerializeField] Text stopwatchXSpeedsText;

        [Header("Stopwatch Buttons")]
        [SerializeField] Button playButton;
        [SerializeField] Button stopButton;
        [SerializeField] Button resetButton;
        [SerializeField] Button slowTime;
        [SerializeField] Button fastTime;

        [Header("Time Speeds")]
        [SerializeField] int[] timeSpeeds = {1,2,4,8};
        List<float> timeSpeedsList = new List<float>();
        int timeSpeedsIndex = 0;

        [Header("Sounds")]
        [SerializeField] AudioClip stopwatchSound;


        [SerializeField] GameObject DialogueBox;

        Fraction fractionValue = new Fraction();

        bool startStopwatch = false;
        bool startAutomaticStopwatch = true;
        float stopwatchTime = 0;

        float t1Value = 0;
        float t2Value = 0;
        int CheckTurn = 0;//Check the value of t1 and t2
        private void Awake()
        {
            
            Instance = this;
            DialogueBox.gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {
            AutomaticTimerCallBack += () => { startAutomaticStopwatch = false; };

            for(int i=timeSpeeds.Length-1;i>=0;i--)
            {
                timeSpeedsList.Add(1f/timeSpeeds[i]);
            }
            for(int i=1;i<timeSpeeds.Length;i++)
            {
                timeSpeedsList.Add(timeSpeeds[i]);
            }
            timeSpeedsIndex = timeSpeeds.Length - 1;

            Init();

            playButton.onClick.AddListener(StartStopWatch);
            stopButton.onClick.AddListener(PauseStopWatch);
            resetButton.onClick.AddListener(ResetStopwatch);

            fastTime.onClick.AddListener(IncreaseTimeSpeed);
            slowTime.onClick.AddListener(DecreaseTimeSpeed);
        }

        void Init()
        {
            stopwatchAutomaticTimeText.gameObject.SetActive(false);

            playButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
            resetButton.gameObject.SetActive(true);

            timeSpeedsIndex = timeSpeeds.Length - 1;
            AdjustStopwatchSpeed(timeSpeedsList, null);

        }

        void StartStopWatch()
        {
            startStopwatch = true;
            playButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);

            if(PlayButtonClickedCallBack!=null)
            {
                PlayButtonClickedCallBack.Invoke();
            }
        }
        public void PauseStopWatch()
        {
            if(pendulum_script.DraggedNo <=0)
            {
                DialogueBox.gameObject.SetActive(true);
                DialogueBox.gameObject.GetComponentInChildren<Text>().text = "Swing the pendulum before you press stop on the stopwatch";
            }
            if(pendulum_script.DraggedNo >=1)
            {
                if(CheckTurn == 0)
                {
                    DialogueBox.gameObject.SetActive(true);
                    DialogueBox.gameObject.GetComponentInChildren<Text>().text = "You got the value in " + "<color=red>" + stopwatchTime + "</color>" + " minutes. \n Note down your t1 and perofrm experiment again to generate t2 value \n"
                        + " <color=red>Click Reset Button to Reset Bob to Origin again</color>";
                    t1Value = stopwatchTime;
                    startStopwatch = false;
                    playButton.gameObject.SetActive(true);
                    stopButton.gameObject.SetActive(false);
                    CheckTurn++;
                   
                }
                else if(CheckTurn ==1)
                {
                    t2Value = stopwatchTime;
                    float avg = (t1Value + t2Value) / 2;
                    DialogueBox.gameObject.SetActive(true);
                    DialogueBox.gameObject.GetComponentInChildren<Text>().text = "You got the value in " + "<color=red>" + stopwatchTime + "</color>" + " minutes. \n Note down your t2 value \n"
                        + " <color=green>Average value of t1 and t2 is </color>" + "<color=red>"+ avg +" </color>";
                    startStopwatch = false;
                    playButton.gameObject.SetActive(true);
                    stopButton.gameObject.SetActive(false);
                    CheckTurn = 0;

                    Debug.Log(pendulum_script.instance.Restriction5End);
                    Debug.Log(pendulum_script.instance.Restriction7End);
                    Debug.Log(pendulum_script.instance.Restriction9End);
                    if(pendulum_script.instance.Restriction5End == false)
                    {
                        pendulum_script.instance.Restriction5End = true;
                        pendulum_script.instance.Restriction7End = false;
                        pendulum_script.instance.Restriction9End = true;
                    }
                   else if (pendulum_script.instance.Restriction7End == false)
                    {
                        pendulum_script.instance.Restriction5End = true;
                        pendulum_script.instance.Restriction7End = true;
                        pendulum_script.instance.Restriction9End = false;
                    }
                   else if (pendulum_script.instance.Restriction9End == false)
                    {
                        pendulum_script.instance.Restriction5End = false;
                        pendulum_script.instance.Restriction7End = true;
                        pendulum_script.instance.Restriction9End = true;
                    }

                }

            }
            //            startStopwatch = false;
            //          playButton.gameObject.SetActive(true);
            //        stopButton.gameObject.SetActive(false);
        }
        public void OnClickDialogueOk()
        {
         
                DialogueBox.SetActive(false);
            
        }

        public void OnResetValueClicked()
        {
            pendulum_script.DraggedNo = 0;
           pendulum_script.instance.DraggedNoText.text = "Bob Daragged " + pendulum_script.DraggedNo;
            stopwatchTimeText.text = "00:00";
            stopwatchTime = 0;
            Init();
        }

        void ResetStopwatch()
        {
            PauseStopWatch();
            stopwatchTimeText.text = "00:00";
            stopwatchTime = 0;
            Init();
        }

        void IncreaseTimeSpeed()
        {
            AdjustStopwatchSpeed(timeSpeedsList, true);
        }
        void DecreaseTimeSpeed()
        {
            AdjustStopwatchSpeed(timeSpeedsList, false);
        }

        void AdjustStopwatchSpeed(List<float> StopwatchSpeedList, bool? increaseSpeed=null)
        {
            float speed = StopwatchSpeedList[timeSpeedsIndex];

            if(increaseSpeed==null)
            {
                speed= StopwatchSpeedList[timeSpeedsIndex];
                Time.timeScale = speed;
            }
            if(increaseSpeed==true &&(timeSpeedsIndex+1)<StopwatchSpeedList.Count)
            {
                timeSpeedsIndex++;
                speed = StopwatchSpeedList[timeSpeedsIndex];
                Time.timeScale = speed;
            }
            if(increaseSpeed == false && (timeSpeedsIndex - 1) >=0)
            {
                timeSpeedsIndex--;
                speed = StopwatchSpeedList[timeSpeedsIndex];
                Time.timeScale = speed;
            }

            if(speed>=1)
            {
                stopwatchXSpeedsText.text = speed.ToString() + "x";
            }
            else
            {
                fractionValue = Fraction.RealToFraction(speed,0.01f);
                stopwatchXSpeedsText.text = fractionValue.N.ToString() + "/" + fractionValue.D.ToString() + "x";
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (startStopwatch == true)
            {
                stopwatchTime += 1 * Time.deltaTime;
                DisplayTime(stopwatchTime);
            }
        }

        void DisplayTime(float TimeToDisplay)
        {
            float MinutesValue = Mathf.FloorToInt(TimeToDisplay / 60);
            float SecondsValue = Mathf.FloorToInt(TimeToDisplay % 60);
            float MilliSecondsValue = (TimeToDisplay % 1) * 1000;

            stopwatchTimeText.text = null;

            if (minutes)
            {
                stopwatchTimeText.text += string.Format("{0:00}", MinutesValue);
            }
            if (seconds)
            {
                stopwatchTimeText.text += string.Format(minutes?":{0:00}":"{0:00}", SecondsValue);
            }
            if (milliSeconds)
            {
                stopwatchTimeText.text += string.Format(seconds?":{0:00}": "{0:00}", Mathf.FloorToInt(MilliSecondsValue/10));
            }

            if(startAutomaticStopwatch)
            {
                stopwatchAutomaticTimeText.gameObject.SetActive(true);
                stopwatchAutomaticTimeText.text = stopwatchTimeText.text;
            }
        }



    }

    public struct Fraction
    {
        public Fraction(int n, int d)
        {
            N = n;
            D = d;
        }

        public int N { get; private set; }
        public int D { get; private set; }

        public static Fraction RealToFraction(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            double maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }
    }
}
