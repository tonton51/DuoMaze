using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAPController : MonoBehaviour
{
   private GameObject player; // プレイヤーのGameObject
    private GameObject currentClosest; // 現在の最も近いオブジェクトを保持する
 
    void Start()
    {
        // プレイヤーオブジェクトを探す。プレイヤーオブジェクトが生成される際に適切に設定する
        player = GameObject.FindWithTag("FPSPlayer");
 
        // シーン内のすべての「AssistPoint」オブジェクトを取得し、非表示にする
        GameObject[] assistPoints = GameObject.FindGameObjectsWithTag("AssistPoint");
        foreach (GameObject assistPoint in assistPoints)
        {
            assistPoint.SetActive(false);
        }
    }
 
    void Update()
    {
        
 
        if (player != null)
        {
            // シーン内のすべての「AssistPoint」オブジェクトを取得
            GameObject[] assistPoints = GameObject.FindGameObjectsWithTag("AssistPoint");
 
            // プレイヤーに最も近いオブジェクトを取得する
            GameObject closestObject = GetClosestObject(player, assistPoints);
 
            if (closestObject != null)
            {
                // 最も近いオブジェクトが変わった場合
                if (closestObject != currentClosest)
                {
                    // 前の最も近いオブジェクトを非表示にする
                    if (currentClosest != null)
                    {
                        currentClosest.SetActive(false);
                    }
 
                    // 新しい最も近いオブジェクトを表示する
                    closestObject.SetActive(true);
                    currentClosest = closestObject;
 
                    // 近いオブジェクトを確認
                    Debug.Log("Closest AssistPoint: " + closestObject.name + 
                              " at distance: " + Vector3.Distance(player.transform.position, closestObject.transform.position));
                }
            }
        }else
        {
            Debug.Log("nullpo");
            // プレイヤーオブジェクトを再度探す
            player = GameObject.FindWithTag("FPSPlayer");
        }
    }
 
    GameObject GetClosestObject(GameObject player, GameObject[] objects)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
 
        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(player.transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }
 
        return closest;
    }
}
