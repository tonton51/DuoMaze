using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerGenerator : MonoBehaviourPunCallbacks
{
    
    // Start is called before the first frame update
    private void Start()
    {     
        Cursor.visible=true;
        
        if(PhotonNetwork.LocalPlayer.ActorNumber==1){
            var position=new Vector3(0,180,0);
                PhotonNetwork.Instantiate("Adviser",position,Quaternion.identity);
            
        }else{
            var position=new Vector3(-45,5,-45);
                PhotonNetwork.Instantiate("Player",position,Quaternion.identity);
        }

    }

    
}
