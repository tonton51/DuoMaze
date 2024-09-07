using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerGenerator : MonoBehaviourPunCallbacks
{
    public int buttonflag=PhotonManager.getButtonflag();
    // Start is called before the first frame update
    private void Start()
    {   
        Debug.Log(buttonflag);
            Cursor.visible=true;
        if(buttonflag==1){
            if(PhotonNetwork.LocalPlayer.ActorNumber==1){
                var adposition=new Vector3(0,180,0);
                    PhotonNetwork.Instantiate("Adviser",adposition,Quaternion.identity);
                
            }else{
                var position=new Vector3(-45,5,-45);
                    PhotonNetwork.Instantiate("Player",position,Quaternion.identity);
            }
        }
        if(buttonflag==0){
             // PlayerプレハブをGameObject型で取得
             GameObject Player = (GameObject)Resources.Load ("FPSPlayer");
             // Playerプレハブを元に、インスタンスを生成
             var position=new Vector3(-45,5,-45);
             Instantiate (Player, position, Quaternion.identity);
        }

    }

    
}
