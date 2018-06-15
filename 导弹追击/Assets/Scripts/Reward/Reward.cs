using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reward : MonoBehaviour {
    private Transform m_Transform;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
		
	}
	
	void Update () {
        m_Transform.Rotate(Vector3.left);   //奖励物品自身旋转
	}

    /// <summary>
    /// 当奖励物品自身销毁时调用
    /// </summary>
    void OnDestroy()
    {
        SendMessageUpwards("RewardCountDown");  //给父级物体发送消息
    }
}
