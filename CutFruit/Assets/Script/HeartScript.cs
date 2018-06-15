using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour {

    private Transform m_Transform;
    public GameObject Brick_Prefab;
    public List<GameObject[]> brickList = new List<GameObject[]>();  //建立砖块集合

    void Awake ()
	{
	    m_Transform = gameObject.GetComponent<Transform>();
	}
 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateBrickList()
    {
        for (int i = 0; i < 6; i++)
        {                                                      
            GameObject[] item = new GameObject[13]; //游戏物体数组
           
            for (int j = 0; j < 13; j++)
            {
                if (j == 0 || j == 12)  //左右边界生成墙壁
                {
                }
                     
                else
                {
                    Vector3 posbrick = new Vector3(j * 2.1f,(i-1) * 1.2f, 0); //每次生成一个砖块位置发生偏移
                        GameObject title = GameObject.Instantiate(Brick_Prefab, posbrick, Quaternion.identity) as GameObject;
                        title.GetComponent<Transform>().SetParent(m_Transform); //给砖块设置父物体
                        item[j] = title;    //生成的砖块放入数组中                                   
                }
                
            }
                       
        }
    }


}


