using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon; // eventdataを使用するためのやつ

public class PlayerController : MonoBehaviourPunCallbacks
{
    Rigidbody rb;
    float speed = 7.0f;
    public GameObject playerCamera;  // プレイヤーのカメラ
    public static bool goalflag = false;
    private const byte GoalEventCode = 3; // カスタムイベントコード
    int buttonflag=PhotonManager.getButtonflag();
 
    void OnTriggerEnter(Collider other)
    {
        // ゴールに当たったら、goalflagをtrueにする
        if (other.CompareTag("Goal"))
        {
            goalflag = true;
 
            // goalflag を他のプレイヤーに通知するイベントを送信
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All // 全プレイヤーにイベントを送信
            };
            PhotonNetwork.RaiseEvent(GoalEventCode, goalflag, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    // オブジェクトが削除されたらコールバックを削除したままに
    void OnDestroy()
    {
        // イベントコールバックを解除
        PhotonNetwork.RemoveCallbackTarget(this);
    }
 
    // Photonのカスタムイベントを受信するメソッド
    // 受信したイベントコードに対応する処理を行う
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == GoalEventCode)
        {
            // goalflag を更新
            goalflag = (bool)photonEvent.CustomData;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(buttonflag==1){
            // イベントコールバックを登録
            PhotonNetwork.AddCallbackTarget(this);
            // 自分のものならカメラをアクティブに
            if (photonView.IsMine)
            {
                playerCamera.SetActive(true);
            }
            else
            {
                playerCamera.SetActive(false);
            }
        }
    }

    // playerの操作
    void Update()
    {
        if(buttonflag==1){
            if (photonView.IsMine)
            {
                playermove();
                // if (Input.GetKey(KeyCode.UpArrow))
                // {
                //     rb.velocity = transform.forward * speed;
                // }
                // if (Input.GetKey(KeyCode.DownArrow))
                // {
                //     rb.velocity = -transform.forward * speed;
                // }
                // if (Input.GetKey(KeyCode.RightArrow))
                // {
                //     rb.velocity = transform.right * speed;
                // }
                // if (Input.GetKey(KeyCode.LeftArrow))
                // {
                //     rb.velocity = -transform.right * speed;
                // }
            }
        }else{
             playermove();
        }
    }

    void playermove(){
        if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = transform.forward * speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = -transform.forward * speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = transform.right * speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = -transform.right * speed;
            }
    }
}
