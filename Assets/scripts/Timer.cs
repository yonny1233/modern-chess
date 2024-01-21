using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public TMP_Text timerText;
    public boardManager boardManager;
    // Start is called before the first frame update
    void Start()
    {
        boardManager = FindObjectOfType<boardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0){
            if(gameObject.tag == blackOrWhite() ){
                timeValue -= Time.deltaTime; 
            }
        }
        else{
            timeValue = 0;
        }
        DisplayTime(timeValue);
    }
    void DisplayTime(float timeToDisplay){
        if(timeToDisplay < 0){
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    string blackOrWhite(){
        if(boardManager.whosTurn == true ){
            return "White";

        }else{
            return "Black";
        }

    }
}
