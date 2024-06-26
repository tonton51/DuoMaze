using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
 
public class APController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public AudioClip assistSE;
    private AudioSource aud;
    private MeshRenderer meshRenderer;
    private float delta = 0;
    private float span = 3.0f;
    private const byte APClickEventCode = 1; // カスタムイベントコード
    private bool meshflag = false;
    private int instanceID; // オブジェクトのインスタンスID
 
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.meshRenderer = GetComponent<MeshRenderer>();
        this.instanceID = GetInstanceID(); // インスタンスIDを取得
 
        // イベントコールバックを登録
        PhotonNetwork.AddCallbackTarget(this);
 
        // 初期状態でオブジェクトを非表示にする
        if (PhotonNetwork.LocalPlayer.ActorNumber != 1)
        {
            meshRenderer.enabled = false;
        }
    }
 
    void Update()
    {
        // 時間計測用
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            meshflag = false;
        }
 
        // 自身のアクターナンバーが1（アドバイザー）でない場合、meshflagに応じてメッシュを表示・非表示する
        if (PhotonNetwork.LocalPlayer.ActorNumber != 1)
        {
            meshRenderer.enabled = meshflag;
        }
    }
 
    // cubeがなくなった時に呼ばれないようにするため、cubeがなくなったらイベントコールバックを削除
    void OnDestroy()
    {
        // イベントコールバックを解除
        PhotonNetwork.RemoveCallbackTarget(this);
    }
 
    // オブジェクトがクリックされたときの処理
    public void OnClick()
    {
        // クリックしたのがadviserなら
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            this.delta = 0;
            meshflag = true;
 
            // ローカルでSEを再生
            this.aud.PlayOneShot(this.assistSE);
 
            // クリックされたプレハブのインスタンスIDをイベントデータに含めて送信
            // オブジェクト型のevenyContentにinstanceIDを入れている
            object[] eventContent = new object[] { instanceID };
 
            // 他のプレイヤーにクリックイベントを送信
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All // 全プレイヤーにイベントを送信
            };
            // eventcontentも含めて送信している
            PhotonNetwork.RaiseEvent(APClickEventCode, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }
    }
 
    // Photonのカスタムイベントを受信するメソッド
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == APClickEventCode)
        {
            // phoyonEvent.customdata内にinstanceidことeventcontentがおそらく入っている
            object[] data = (object[])photonEvent.CustomData;
            // instanceidはintなのでint型にキャスト（イベント送信時はobject型なのでこれが必要）
            int receivedInstanceID = (int)data[0];
 
            // 受信したインスタンスIDが自分のインスタンスIDと一致する場合にのみ処理を実行
            if (receivedInstanceID == instanceID)
            {
                // SEを再生
                this.aud.PlayOneShot(this.assistSE);
                this.delta = 0;
                meshflag = true;
                meshRenderer.enabled = true; // メッシュを表示する
            }
        }
    }
}