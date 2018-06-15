using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//需要添加的：
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour {
    public static ScoreScript instance;

    Text scoreText;//显示分数的控件
    float score = 0;
    float timer = 0;//定时器

    private Image[] LifeImage;  //生命爱心显示组件
    private int index = 3;      //生命爱心数量索引

    void Awake()
    {
        instance = this;
    }


	void Start () 
    {
        scoreText = transform.GetComponentInChildren<Text>();

        LifeImage = gameObject.GetComponentsInChildren<Image>();
    }
	

	void Update ()
    {
        scoreText.text = "分数：" + score;
	}

    /// <summary>
    /// 更新分数丝氨酸
    /// </summary>
    /// <param name="s"></param>
    public void UpdateScore(float s)
    {
        score += s;

        //timer += Time.deltaTime;
        //    if (score < 0)
        //    {
        //            SceneManager.LoadScene(2);//转到Lose界面     
        //    }
    }

    /// <summary>
    /// 生命爱心减少
    /// </summary>
    public void DownLife()
    {
        index--;
        LifeImage[index].enabled = false;
        if (index == 0)
        {
            SceneManager.LoadScene(2); //跳转到Lose界面
        }

    }


}
