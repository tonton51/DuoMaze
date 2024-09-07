using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultDirector : MonoBehaviour
{
    GameObject resulttimertext;
    float resulttime;
    // Start is called before the first frame update
    void Start()
    {
            this.resulttimertext = GameObject.Find("Time");
            this.resulttime=GameDirector.getGoalTime(); 
            Debug.Log(resulttime);
            this.resulttimertext.GetComponent<TextMeshProUGUI>().text = this.resulttime.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
