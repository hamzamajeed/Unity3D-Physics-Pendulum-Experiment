using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KetosGames.SceneTransition;
public class TrueFalsePuzzle : MonoBehaviour
{
    [Header("Puzzle Buttons")]
    [SerializeField] GameObject Puzzle1Panel;
    [SerializeField] GameObject Puzzle2Panel;
    [SerializeField] Button Puzzle1stTrue;
    [SerializeField] Button Puzzle1stFalse;
    [SerializeField] Button AirFriction;
    [SerializeField] Button Gravity;
    [SerializeField] Button Friction;
    [SerializeField] GameObject DialogueBox;
    [SerializeField] Button Yes;
    [SerializeField] Button No;



    private void Start()
    {
        Puzzle1Panel.gameObject.SetActive(true);
        Puzzle2Panel.gameObject.SetActive(false);
        DialogueBox.gameObject.SetActive(false);

        Puzzle1stTrue.onClick.AddListener(TrueFunc);
        Puzzle1stFalse.onClick.AddListener(FalseFunc);
       
        
        AirFriction.onClick.AddListener(AirFrictionFun);
        Gravity.onClick.AddListener(GravityFunc);
        Friction.onClick.AddListener(FrictionFunc);

        Yes.onClick.AddListener(YesClicked);
        No.onClick.AddListener(NoClicked);
    }

    private void NoClicked()
    {
        DailoguePanel();
        Puzzle1Panel.SetActive(true);
        Puzzle2Panel.SetActive(false);
        SceneLoader.LoadScene(2);
    }

    private void YesClicked()
    {
        DailoguePanel();
        SceneLoader.LoadScene(1);
    }

    private void TrueFunc()
    {
       
        Puzzle1stTrue.gameObject.GetComponent<Image>().color = Color.green;
        Puzzle1Panel.gameObject.SetActive(false);
        Puzzle2Panel.gameObject.SetActive(true);
        Debug.Log("True Clicked");
    }

    private void FalseFunc()
    {
        Puzzle1stFalse.gameObject.GetComponent<Image>().color = Color.red;
        Invoke("DailoguePanel", 2f);
       
        Debug.Log("False Clicked");
    }

    void DailoguePanel()
    {
        if (!DialogueBox.activeSelf)
        {

            DialogueBox.SetActive(true);
        }
        else
        {

            DialogueBox.SetActive(false);
        }
    }

    private void AirFrictionFun()
    {
        AirFriction.gameObject.GetComponent<Image>().color = Color.green;
        Debug.Log("False Clicked");
    }


    private void GravityFunc()
    {
        Gravity.gameObject.GetComponent<Image>().color = Color.green;
        Debug.Log("False Clicked");
    }


    private void FrictionFunc()
    {
        Friction.gameObject.GetComponent<Image>().color = Color.red;
        Invoke("DailoguePanel", 2f);
        Debug.Log("False Clicked");
    }

}
