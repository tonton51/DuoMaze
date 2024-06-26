using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameDirector : MonoBehaviourPunCallbacks
{
    // Update is called once per frame
    void Update()
    {    PhotonNetwork.AutomaticallySyncScene = true; // trueならマスタークライアントと同期
        bool goalflag=PlayerController.goalflag;
        // goalflagがtrueならゴールシーンに遷移
        if(goalflag){
            SceneManager.LoadSceneAsync("Result",LoadSceneMode.Single);
        }   
    }
}
