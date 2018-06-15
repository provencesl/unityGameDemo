using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    private bool isLeft = false;   //左转
    private bool isRight = false;  //右转
    private bool isDeath = false;  //飞机死亡的标志

    private Transform m_Transform;

    private Vector3 m_VectorShip;  //飞机方向

    //获取脚本
    private MissileManager m_MissileManager;
    private RewardManager m_RewardManager;
    private GameUIManager m_GameUIManager;

    private GameObject Explode07;  //爆炸特效
    private GameObject Missile;//飞机发射的导弹

    private int rewardNum;   //吃到奖励物品的数量

    private int speed;
    private int rotate;

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public int Ratate
    {
        get { return rotate; }
        set { rotate = value; }
    }
    //属性:对飞机向左飞进行传值
    public bool IsLeft
    {
        get {return isLeft;}
        set { isLeft = value; }
    }

    //属性:对飞机向右飞进行传值
    public bool IsRight
    {
        get { return isRight; }
        set { isRight = value; }
    }

	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();

        
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<MissileManager>();
        m_RewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();
        m_GameUIManager = GameObject.Find("UI Root").GetComponent<GameUIManager>();

        Explode07 = Resources.Load<GameObject>("Explode07");
        Missile = Resources.Load<GameObject>("Missile_2");
	}
	

	void Update () {
        m_VectorShip = m_Transform.TransformDirection(Vector3.forward);
        if (isDeath == false)    
        {
            //飞机一直向前飞行
            m_Transform.Translate(Vector3.forward * speed);

            if (IsLeft)
            {
                m_Transform.Rotate(Vector3.up * -1 * rotate);
            }
            if (IsRight)
            {
                m_Transform.Rotate(Vector3.up * 1 * rotate);
            }

            if (Input.GetKey(KeyCode.A))
            {
                m_Transform.Rotate(Vector3.up * -1 * rotate);
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_Transform.Rotate(Vector3.up * 1 * rotate);
            }
            //飞机发射导弹
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject m_Missile = GameObject.Instantiate(Missile, m_Transform.position, m_Transform.rotation);  //生成导弹
                
                NGUITools.AddChild(gameObject, m_Missile);
                m_Missile.GetComponent<Rigidbody>().AddForce(m_VectorShip * 500,ForceMode.Impulse);
            }
        }
        
	}

    //飞机与墙壁碰撞
    private void OnTriggerEnter(Collider coll)
    {
        //与墙壁发生碰撞
        if (coll.tag == "Border")
        {
            isDeath = true;
            m_GameUIManager.GameOver(); 
            GameObject.Instantiate(Explode07, m_Transform.position, Quaternion.identity);  //播放特效
            gameObject.SetActive(false);          //飞机隐藏自身
            m_MissileManager.StopCreate();        //停止导弹生成                  
        }
        //与导弹发生碰撞
        if (coll.tag == "Missile")
        {
            isDeath = true;   //角色死亡
            m_GameUIManager.GameOver();
            
            m_MissileManager.StopCreate();        //停止导弹生成
            GameObject.Instantiate(Explode07, m_Transform.position, Quaternion.identity);  //播放特效
            GameObject.Destroy(coll.gameObject);  //销毁碰撞到的导弹
            gameObject.SetActive(false);          //飞机隐藏自身
        }

        if(coll.tag=="Reward")
        {
            rewardNum++;
            m_GameUIManager.UpdateStarNum(rewardNum*50); //更新奖励物品分数
            GameObject.Destroy(coll.gameObject);  //销毁奖励物品
        }
    }
}
