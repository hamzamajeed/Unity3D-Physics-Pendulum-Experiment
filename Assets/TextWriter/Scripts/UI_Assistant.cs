
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mani.Utils;
using KetosGames.SceneTransition;

public class UI_Assistant : MonoBehaviour {

    private Text messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    int stringindex = 0;
    bool stringCompleteTrue = false;
    private void Awake() {
        messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
        talkingAudioSource = transform.Find("talkingSound").GetComponent<AudioSource>();

        transform.Find("message").GetComponent<Button_UI>().ClickFunc = () => {
            if (textWriterSingle != null && textWriterSingle.IsActive()) {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else {

                string[] messageArray = new string[] {
                
                    "Q) What is Time Period?",

                    "Ans) The time period is the time taken by a complete cycle of the wave to pass a point. In this practical it is the time taken for the bob to pass the origin point.",
                    
                    "Q) What is Gravity?",
                    
                    "Ans) The force that attracts a body towards the centre of the earth, or towards any other physical body having mass. In this practical it the force that attracts our bob to the ground.",
                    
                    "Q) What is Air Resistance?",
                    
                    "Ans) Air resistance is a force that is caused by air. The force acts in the opposite direction to an object moving through the air.",
                  
                };

                //    string message = messageArray[Random.Range(0, messageArray.Length)];
                if(stringindex != (messageArray.Length-1))
                {
                    stringindex++;

                }
                else
                {
                    SceneLoader.LoadScene(1);
                    Debug.Log("Array finished");
                }
                string message = messageArray[stringindex];
                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .02f, true, true, StopTalkingSound);
            }
        };
    }

    private void StartTalkingSound() {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound() {
        talkingAudioSource.Stop();
    }

    private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }

}
