using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 导弹控制
/// </summary>
public class Missile : MonoBehaviour {

    private Transform m_Transform;
    private Transform player_Transform;

    private Vector3 normalForward = Vector3.forward;

    private GameObject Explode07;  //爆炸特效

    private int speed;
   
	void Start () {
       
        speed = PlayerPrefs.GetInt("PlayerSpeed");    //飞机速度

        m_Transform = gameObject.GetComponent<Transform>();
        player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Explode07 = Resources.Load<GameObject>("Explode07");	
	}
	

	void Update () {

        m_Transform.Translate(Vector3.forward*speed);  //导弹向前飞行

        //导弹追踪角色----------------------------------------------------

        Vector3 dir = player_Transform.position - m_Transform.position;     //得到一个向量：角色位置与当前导弹位置相减

        normalForward = Vector3.Lerp(normalForward, dir, Time.deltaTime);   //向量插值计算导弹要旋转的角度

        m_Transform.rotation = Quaternion.LookRotation(normalForward);       //改变当前导弹的朝向        		
	}

    /// <summary>
    /// 导弹与导弹碰撞
    /// </summary>
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Missile")
        {
            //播放特效
            GameObject.Instantiate(Explode07, m_Transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }
}
