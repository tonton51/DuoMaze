using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviourPunCallbacks
{
    public float mouseSensitivity = 100f;  // マウスの感度
    public Transform playerBody;  // プレイヤーのトランスフォーム

    float xRotation = 0f;

    void Start()
    {
        // 自身が管理者かどうか（自分のものかどうか）
        if (!photonView.IsMine)
        {
            // そうでなければオブジェクトを無効化
            gameObject.SetActive(false);
            return;
        }

        // Cursor.lockState = CursorLockMode.Locked;  // カーソルをロック
    }

    void Update()
    {
        // 所有者ではなければupdateしない
        if (!photonView.IsMine)
        {
            return;
        }
        if(PhotonNetwork.LocalPlayer.ActorNumber!=1){
            if(Input.GetMouseButton(0)){

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 上下方向の回転角度を制限する

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            }
            
        }


    }
}

