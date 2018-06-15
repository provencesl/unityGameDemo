using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奖励物品管理器
/// </summary>
public class RewardManager : MonoBehaviour {

    private Transform m_Transform;
    private GameObject prefab_reward;

    private int rewardCount = 0;    //当前存在的奖励物品数量
    private int rewardMaxCount = 4; //当前允许存在的最大奖励物品数量

	void Start () {

        m_Transform = gameObject.GetComponent<Transform>();
        prefab_reward = Resources.Load<GameObject>("reward");

        InvokeRepeating("CreateReward", 3, 4);
	}

    /// <summary>
    /// 生成奖励物品
    /// </summary>
    private void CreateReward()
    {
        if (rewardCount < rewardMaxCount)
        {
            rewardCount++;
            Vector3 pos = new Vector3(Random.Range(-400,400),0,Random.Range(-590,570));
            Instantiate(prefab_reward, pos, Quaternion.identity, m_Transform);
        }
    }

    /// <summary>
    /// 停止生成奖励物品
    /// </summary>
    public void StopCreateReward()
    {
        CancelInvoke();
    }

    /// <summary>
    /// 当前奖励物品数量减一
    /// </summary>
    public void RewardCountDown()
    {
        rewardCount--;
    }
}
