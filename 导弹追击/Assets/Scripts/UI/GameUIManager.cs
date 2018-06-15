using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    private GameObject m_OverPanel;   //结束面板

    private GameObject m_ButtonControl; //按钮控制器

    private GameObject m_Back;   //返回开始界面按钮

    private UILabel m_StarNum;  //奖励物品分数
    private UILabel m_Time;  //时间计时

    //结束面板显示数据
    private UILabel m_HeightScore; //最高总分
    private UILabel m_TimeScore;   //时间分数
    private UILabel m_StarScore;   //奖励物品分数

    private int time; //时间数值

    

	void Start () {
        m_OverPanel = GameObject.Find("OverPanel");

        m_ButtonControl = GameObject.Find("ButtonControl");

        m_Back = GameObject.Find("Back");

        UIEventListener.Get(m_Back).onClick = ResetonClick;

        m_StarNum = GameObject.Find("StarNum").GetComponent<UILabel>();
        m_Time = GameObject.Find("Time").GetComponent<UILabel>();       

        m_HeightScore = GameObject.Find("EndScore/HeightScore").GetComponent<UILabel>();
        m_StarScore = GameObject.Find("GetStar/StarScore").GetComponent<UILabel>();
        m_TimeScore = GameObject.Find("TimeSprite/TimeScore").GetComponent<UILabel>();

        m_StarNum.text = "0";
        m_Time.text = "0:0";

        StartCoroutine("AddTime");
        m_OverPanel.SetActive(false);
        
	}

    //封装时间属性
    public int Time
    {
        get { return time; }
        set
        {
            time = value;
            UpDateTime(time);
        }
    }

    

    /// <summary>
    /// 更新奖励物品分数
    /// </summary>
    public void UpdateStarNum(int star)
    {
        m_StarNum.text = star + "";
    }

    /// <summary>
    /// 更新UI显示时间
    /// </summary>
    /// <param name="t"></param>
    private void UpDateTime(int t)
    {
        
        if (t < 60)
        {
            m_Time.text = "0:" + t;
        }
        else
        {
            m_Time.text = t / 60 + ":" + t % 60;
        }
    }

    /// <summary>
    /// 更新结束面板显示
    /// </summary>
    private void UpdateOverinfo()
    {
        int s = int.Parse(m_StarNum.text);

        m_TimeScore.text = "+" + time;

        m_StarScore.text = "+" + m_StarNum.text;

        int all = time + s;
        m_HeightScore.text = all.ToString();

        PlayerPrefs.SetInt("HeightScore", all);  //存储最高分数
        PlayerPrefs.SetInt("GoldNum", s);        //存储金币数
    }
    /// <summary>
    /// 角色死亡时
    /// </summary>
    public void GameOver()
    {
        m_OverPanel.SetActive(true);      //结束界面显示
        StopAddTime();
        UpdateOverinfo();
        m_ButtonControl.SetActive(false); //隐藏控制按钮
    }

    /// <summary>
    /// 跳转回开始场景
    /// </summary>
    private void ResetonClick(GameObject go)
    {
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    /// 协程增加时间
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddTime()
    {
        while (true)
        {
            //每隔一秒时间增加
            yield return new WaitForSeconds(1);
            Time++;
        }        
    }
    /// <summary>
    /// 停止增加时间
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }
}
