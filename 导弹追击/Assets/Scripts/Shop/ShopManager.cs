using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;  //文件输出

public class ShopManager : MonoBehaviour {

    private ShopData shopData;       //商城数据

    private GameObject m_ShopItem;   //商城元素模板

    private GameObject leftButton;   //左按钮
    private GameObject rightButton;  //右按钮

    private UILabel ui_starNum;      //商城金币UI
    private UILabel ui_scoreNum;     //商城最高分UI

    private StartUIManager m_StartUIManager;

    private List<GameObject> shopUI = new List<GameObject>();  //集合管理商城元素

    //获取商城xml文件路径
    private string shopPath = "Assets/Datas/ShopData.xml";  
   
    //private string shopPath = Application.dataPath + "/Datas/ShopData.xml";  //windows路径
    //private string shopPath;  //安卓路径

    //获取保存数据xml文件路径
    private string savePath = "Assets/Datas/SaveData.xml";     

    //private string savePath = Application.dataPath + "/Datas/SaveData.xml";
    //private string savePath = Application.persistentDataPath + "/SaveData.xml"; //windows路径
    //安卓路径加载XML文档
    //private string content = "<SaveData><GoldCount>7950</GoldCount><HeightScore>0</HeightScore><ID0>1</ID0><ID1>0</ID1><ID2>0</ID2><ID3>0</ID3></SaveData>";

    private int index = 0;  //建立商城展示次序的索引

    private int Prcie;      //所拥有的金币数

	void Start () {

        //shopPath = Resources.Load("ShopData").ToString(); //获取路径

        //动态写入文件路径
        /*if (!File.Exists(savePath))
        {
            File.WriteAllText(savePath, content);
        }*/

        shopData = new ShopData(); //实例化商城数据对象

        m_ShopItem = Resources.Load<GameObject>("ShopItem"); //加载预制体

        leftButton = GameObject.Find("LeftButton");
        rightButton = GameObject.Find("RightButton");        
        
        shopData.ReadByXmlPath(shopPath);    //加载Xml
        shopData.ReadGoldAndScore(savePath); //加载保存数据xml文件       

        ui_starNum = GameObject.Find("StarNum").GetComponent<UILabel>();
        ui_scoreNum = GameObject.Find("ScoreNum").GetComponent<UILabel>();

        //更新开始面板最高分显示
        int tempHeightScore = PlayerPrefs.GetInt("HeightScore", 0);
        if (tempHeightScore > shopData.heightScore)
        {
            //更新UI
            ShowHeightScore(tempHeightScore);
            //更新XML
            shopData.ChangeXMLData(savePath, "HeightScore", tempHeightScore.ToString());
            //清空PlayerPrefs中的数据
            PlayerPrefs.SetInt("HeightScore", 0);
        }
        else
        {
            //更新UI
            ShowHeightScore(shopData.heightScore);
        }

        //更新开始UI金币数显示
        int tempgold = PlayerPrefs.GetInt("GoldNum", 0);
        if (tempgold > 0)
        {
            //金币的增加
            int allgold = tempgold + shopData.goldCount;
            //更新UI
            ShowGoldNum(allgold);
            //更新XML
            shopData.ChangeXMLData(savePath, "GoldCount", allgold.ToString());
            //清空PlayerPrefs中的数据
            PlayerPrefs.SetInt("GoldNum", 0);
        }
        else
        {
            //更新UI
            ShowGoldNum(shopData.goldCount);
        }

        m_StartUIManager = GameObject.Find("UI Root").GetComponent<StartUIManager>();

        UIEventListener.Get(leftButton).onClick = LeftButtonClick;
        UIEventListener.Get(rightButton).onClick = RightButtonClick;

        SetModelInfo(shopData.shopList[0]);
        //SetPlayerInfo(shopData.shopList[0].Player);
        CreateAllShopUI();

	}


    /// <summary>
    /// 更新最高分数
    /// </summary>
    private void ShowHeightScore(int heightscore)
    {
        ui_scoreNum.text = heightscore.ToString();
    }

    /// <summary>
    /// 更新金币数
    /// </summary>
    private void ShowGoldNum(int goldnum)
    {
        ui_starNum.text = goldnum.ToString();
    }


    /// <summary>
    /// 显示UI数据
    /// </summary>
    private void ShowUIData()
    {
        ui_starNum.text = shopData.goldCount.ToString();
        ui_scoreNum.text = shopData.heightScore.ToString();
    }

    /// <summary>
    /// 创建所有的商城UI元素
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //实例化单个商城元素
            GameObject item = NGUITools.AddChild(gameObject, m_ShopItem);
            //加载飞机模型
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            //为每个商城元素赋值
            item.GetComponent<ShopUIItem>().CreateShopData(shopData.shopList[i].ID, shopData.shopList[i].Speed, shopData.shopList[i].Rotate, shopData.shopList[i].Price, ship,shopData.shopState[i]);
            //将生成的商城元素添加进集合中
            shopUI.Add(item);
        }
        ShowShopUI(index);//先只显示第一个商城面板
    }

    /// <summary>
    /// 计算商品价格并购买
    /// </summary>
    /// <param name="item"></param>
    public void CalcItemPrice(ShopUIItem item)
    {
        if (shopData.goldCount >= item.itemPrice)   //当金币数大于商品价格时
        {
            Debug.Log("购买成功");
            item.buyState.SetActive(false);      //隐藏购买按钮
            shopData.goldCount -= item.itemPrice;  //金币数减去商品价格
            ShowUIData();     //更新UI数据
            shopData.ChangeXMLData(savePath, "GoldCount", shopData.goldCount.ToString());   //更新金币数           
            shopData.ChangeXMLData(savePath, "ID" + item.itemID, "1");      //更新商品的购买状态            
        }
        else
        {
            Debug.Log("购买失败，金币不足");
        }
    }
    /// <summary>
    /// 左按钮事件
    /// </summary>
    private void LeftButtonClick(GameObject go)
    {
        if (index > 0)
        {
            Debug.Log("left");
            index--;            
            int temp = shopData.shopState[index]; //对应商品的购买状态
            m_StartUIManager.SetStartButton(temp); //是否显示开始按钮

            SetModelInfo(shopData.shopList[index]);
            //SetPlayerInfo(shopData.shopList[index].Player);
            ShowShopUI(index);
        }       
    }

    /// <summary>
    /// 右按钮事件
    /// </summary>
    private void RightButtonClick(GameObject go)
    {
        if (index < shopUI.Count - 1)
        {
            Debug.Log("right");
            index++;          
            int temp = shopData.shopState[index]; //对应商品的购买状态
            m_StartUIManager.SetStartButton(temp); //是否显示开始按钮

            SetModelInfo(shopData.shopList[index]);
            //SetPlayerInfo(shopData.shopList[index].Player);
            ShowShopUI(index);
        }       
    }
    
    /// <summary>
    /// 展示商城界面
    /// </summary>
    private void ShowShopUI(int index)
    {
        for (int i = 0; i < shopUI.Count; i++)
        {
            shopUI[i].SetActive(false);
        }
        shopUI[index].SetActive(true);
    }

   

    /*
    private void SetPlayerInfo(string name)
    {
        PlayerPrefs.SetString("Playername", name);  //存储传递过来的角色
    }*/

    /// <summary>
    //存储飞机模型信息到PlayerPrefs中去
    /// </summary>
    /// <param name="model"></param>
    private void SetModelInfo(ShopItem item)
    {
        PlayerPrefs.SetString("ModelName", item.Model);
        PlayerPrefs.SetInt("PlayerSpeed", int.Parse(item.Speed));
        PlayerPrefs.SetInt("PlayerRotate", int.Parse(item.Rotate));
    }
}
