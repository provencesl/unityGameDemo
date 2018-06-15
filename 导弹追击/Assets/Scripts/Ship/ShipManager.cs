using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞机管理器
/// </summary>
public class ShipManager : MonoBehaviour {
  
    private GameObject model;
    private GameObject player;

	void Awake () {

        string ship = PlayerPrefs.GetString("ModelName"); //接收传递过来的飞机信息
        int speed = PlayerPrefs.GetInt("PlayerSpeed");    //飞机速度
        int rotate = PlayerPrefs.GetInt("PlayerRotate");  //飞机旋转灵敏度

        model = Resources.Load<GameObject>(ship);          //获取飞机模型
        player = GameObject.Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;  //实例化飞机模型
        Ship myShip = player.AddComponent<Ship>();   //给实例化出来的角色添加控制脚本
        myShip.Speed = speed;
        myShip.Ratate = rotate;

        player.tag = "Player";         //设置标签
        if (player.name == "Ship_4(Clone)")
        {
            player.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (player.name == "Ship_1(Clone)")
        {
            player.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            player.GetComponent<Transform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        		
	}
	
	
}
