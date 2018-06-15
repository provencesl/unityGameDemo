using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞机发射的导弹脚本
/// </summary>
public class ShipShoot : MonoBehaviour {

    private Transform m_Transform;

    private GameObject Explode07;  //爆炸特效

    private GameUIManager m_GameUIManager;
    private int rewardNum;   //吃到奖励物品的数量
	void Start () 
    {
        m_Transform = gameObject.GetComponent<Transform>();

        Explode07 = Resources.Load<GameObject>("Explode07");

        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();
	}


    private void OnTriggerEnter(Collider coll)
    {
        //与墙壁发生碰撞
        if (coll.tag == "Border")
        {
            GameObject.Instantiate(Explode07, m_Transform.position, Quaternion.identity);  //播放特效
            GameObject.Destroy(gameObject);       //发射的导弹销毁自身
                             
        }
        //与导弹发生碰撞
        if (coll.tag == "Missile")
        {
                       
            GameObject.Instantiate(Explode07, m_Transform.position, Quaternion.identity);  //播放特效
            GameObject.Destroy(coll.gameObject);  //销毁碰撞到的导弹
            GameObject.Destroy(gameObject);       //发射的导弹销毁自身
        }

        if (coll.tag == "Reward")
        {
            rewardNum++;
            m_GameUIManager.UpdateStarNum(rewardNum * 50); //更新奖励物品分数
            GameObject.Destroy(coll.gameObject);  //销毁奖励物品
        }
    }
	    

}
