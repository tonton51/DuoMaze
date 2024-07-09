using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviourPunCallbacks
{
    GameObject timerText;
    float time=0.0f;
    protected void Start(){
        this.timerText = GameObject.Find("Timer");
    }
    // Update is called once per frame
    void Update()
    {   
        this.time += Time.deltaTime;
        PhotonNetwork.AutomaticallySyncScene = true; // trueならマスタークライアントと同期
        bool goalflag=PlayerController.goalflag;
        // goalflagがtrueならゴールシーンに遷移
        if(goalflag){
            SceneManager.LoadSceneAsync("Result",LoadSceneMode.Single);
        }
        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
    }
}