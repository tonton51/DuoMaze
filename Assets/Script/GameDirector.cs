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
    public static float goaltime;
    public int buttonflag=PhotonManager.getButtonflag();
    protected void Start(){
        this.timerText = GameObject.Find("Timer");
    }
    // Update is called once per frame
    void Update()
    {   
        if(PhotonNetwork.LocalPlayer.ActorNumber!=1||buttonflag==1){
            this.time += Time.deltaTime;
            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
        }
        PhotonNetwork.AutomaticallySyncScene = true; // trueならマスタークライアントと同期
        bool goalflag=PlayerController.goalflag;
        // goalflagがtrueならゴールシーンに遷移
        if(goalflag){
            goaltime=this.time;
            SceneManager.LoadSceneAsync("Result",LoadSceneMode.Single);
        }

    }

    public static float getGoalTime() { return goaltime; }
}