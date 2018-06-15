using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 导弹生成管理
/// </summary>
public class MissileManager : MonoBehaviour {

    private Transform m_Transform;
    private Transform[] createPoints;  //导弹生成点数组

    private int createtime = 4;

    private GameObject prefab_Misslie;
	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();
        createPoints = GameObject.Find("CreatePoints").GetComponent<Transform>().GetComponentsInChildren<Transform>();

        prefab_Misslie = Resources.Load<GameObject>("Missile_3");

        InvokeRepeating("CreateMissile", 3, createtime);  //固定时间间隔调用导弹生成函数 
        StartCoroutine("DownCreateTime");
	}

    /// <summary>
    /// 导弹生成方法
    /// </summary>
    private void CreateMissile()
    {
        int index = Random.Range(0, createPoints.Length);
        GameObject.Instantiate(prefab_Misslie, createPoints[index].position, Quaternion.identity, m_Transform);
    }

    /// <summary>
    /// 停止生成导弹
    /// </summary>
    public void StopCreate()
    {
        CancelInvoke();
    }

    private IEnumerator DownCreateTime()
    {
        while (createtime > 0)
        {
            yield return new WaitForSeconds(20);
            createtime--;
        }
    }
}
