using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour {

    private GameObject m_StartPanel;   //开始面板
    private GameObject m_SettingPanel; //设置面板
    private GameObject m_InfoPanel;    //简介面板

    private GameObject m_SettingButton; //设置按钮
    private GameObject m_CloseButton;   //关闭设置面板按钮
    private GameObject m_StartButton;   //开始游戏按钮
    private GameObject m_BackStart;      //返回开始界面
    private GameObject m_GoInfo;         //去往简介面板

    private AudioSource m_Audio;

    private GameObject m_OpenAudio;     //开启声音
    private GameObject m_CloseAudio;    //关闭声音


	void Start ()
    {
       m_StartPanel = GameObject.Find("StartPanel");
       m_SettingPanel = GameObject.Find("SettingPanel");
       m_InfoPanel = GameObject.Find("InfoPanal");

       m_SettingButton = GameObject.Find("Setting");
       m_CloseButton = GameObject.Find("Close");
       m_StartButton = GameObject.Find("Start");
       m_BackStart = GameObject.Find("BackStart");
       m_GoInfo = GameObject.Find("GoInfo");

       m_OpenAudio = GameObject.Find("OpenAudio");
       m_CloseAudio = GameObject.Find("CloseAudio");

       m_Audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

       UIEventListener.Get(m_SettingButton).onClick = SettingonClick;
       UIEventListener.Get(m_CloseButton).onClick = CloseonClick;
       UIEventListener.Get(m_StartButton).onClick = StartGame;       
       UIEventListener.Get(m_BackStart).onClick = BackStartClick;
       UIEventListener.Get(m_GoInfo).onClick = GoInfoClick;

       UIEventListener.Get(m_OpenAudio).onClick = OpenAudioClick;
       UIEventListener.Get(m_CloseAudio).onClick = CloseAudioClick;
       m_CloseAudio.SetActive(false);

       m_SettingPanel.SetActive(false);  //游戏开始时隐藏设置面板
       m_InfoPanel.SetActive(false);     //开始隐藏简介面板
		
	}

    /// <summary>
    /// 点击设置按钮，显示设置面板
    /// </summary>
    private void SettingonClick(GameObject go)
    {
        //当设置面板没有显示时
        if (m_SettingPanel.activeSelf == false)
        {
            Debug.Log("设置面板");
            m_SettingPanel.SetActive(true);  //显示设置面板
        }       
    }

    /// <summary>
    /// 关闭设置面板
    /// </summary>
    private void CloseonClick(GameObject go)
    {
        m_SettingPanel.SetActive(false);
    }

    /// <summary>
    /// 跳转到Game场景
    /// </summary>
    /// <param name="go"></param>
    public void StartGame(GameObject go)
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 设置开始按钮
    /// </summary>
    /// <param name="state"></param>
    public void SetStartButton(int state)
    {
        if (state == 1)
        {
            m_StartButton.SetActive(true);
        }
        else
        {
            m_StartButton.SetActive(false);
        }
    }

    /// <summary>
    /// 音效控制按钮
    /// </summary>
    /// <param name="go"></param>
    private void OpenAudioClick(GameObject go)
    {
        m_Audio.Pause();
        m_OpenAudio.SetActive(false);
        m_CloseAudio.SetActive(true);
    }
    private void CloseAudioClick(GameObject go)
    {
        m_Audio.Play();
        m_OpenAudio.SetActive(true);
        m_CloseAudio.SetActive(false);
    }

    private void BackStartClick(GameObject go)
    {
        m_InfoPanel.SetActive(false);
        m_SettingPanel.SetActive(false);
        m_StartPanel.SetActive(true);
    }

    private void GoInfoClick(GameObject go)
    {
        m_InfoPanel.SetActive(true);
        m_SettingPanel.SetActive(false);
        m_StartPanel.SetActive(false);
    }
}
