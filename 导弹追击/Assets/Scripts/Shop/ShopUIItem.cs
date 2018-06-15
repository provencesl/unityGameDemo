using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商品UI控制器
/// </summary>
public class ShopUIItem : MonoBehaviour {

    private Transform m_Transform;

    private UILabel ui_Speed;
    private UILabel ui_Rotate;
    private UILabel ui_Price;

    private GameObject shipParent; //飞机模型父物体


    private GameObject buyButton;   //购买按钮
    public GameObject buyState;    //商品状态(1为已购买，0为没有购买)

    public int itemPrice;           //商品价格
    public int itemID;              //商品的ID编号

	void Awake () {

        m_Transform = gameObject.GetComponent<Transform>();

        ui_Speed = m_Transform.Find("Speed/Speed_Num").GetComponent<UILabel>();
        ui_Rotate = m_Transform.Find("Rotate/Rotate_Num").GetComponent<UILabel>();
        ui_Price = m_Transform.Find("Buy/Price").GetComponent<UILabel>();

        shipParent = m_Transform.Find("Model").gameObject;

        buyButton = m_Transform.Find("Buy/Bg").gameObject;
        buyState = m_Transform.Find("Buy").gameObject;

        UIEventListener.Get(buyButton).onClick = BuyButtonClick;             
	}
      

    /// <summary>
    /// 创建商城数据元素
    /// </summary>
    public void CreateShopData(string id, string speed, string rotate, string price,GameObject model,int state)
    {
        //给商城UI赋值
        ui_Speed.text = speed;
        ui_Rotate.text = rotate;
        ui_Price.text = price;

        itemPrice = int.Parse(price);
        itemID = int.Parse(id);

        if (state == 1)
        {
            buyState.SetActive(false);
        }

        //实例化飞机模型
        GameObject ship = NGUITools.AddChild(shipParent, model);
        //给父物体指定模型层
        ship.layer = 8;
        //子物体指定模型层
        ship.GetComponent<Transform>().Find("Mesh").gameObject.layer = 8;
        //设置模型的位置和缩放
        Transform ship_Transform = ship.GetComponent<Transform>();

        if (model.name == "Ship_4")  //调整个别飞机模型的大小
        {
            ship_Transform.localScale = new Vector3(3, 3, 3);        //大小
        }
        else
        {
            ship_Transform.localScale = new Vector3(8, 8, 8);        //大小
        }
        
        ship_Transform.localPosition = new Vector3(0, -73, 112); //位置
        Vector3 rot = new Vector3(-90,0,0);
        ship_Transform.localRotation = Quaternion.Euler(rot);    //旋转

    }

    /// <summary>
    /// 购买点击事件
    /// </summary>
    /// <param name="go"></param>
    private void BuyButtonClick(GameObject go)
    {
        Debug.Log("购买");
        SendMessageUpwards("CalcItemPrice", this);  //向父物体发送消息
    }
	
}
