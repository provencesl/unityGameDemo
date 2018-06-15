using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 商城数据
/// </summary>
public class ShopData {
   
    public List<ShopItem> shopList = new List<ShopItem>();//实例化集合

    public int goldCount;      //金币数
    public int heightScore;    //最高分数

    public List<int> shopState = new List<int>();      //物品购买状态
	
    /// <summary>
    /// 通过xml路径读取xml文件信息
    /// </summary>
    /// <param name="path">xml路径</param>
    public void ReadByXmlPath(string path)
    {
        XmlDocument doc = new XmlDocument();//实例化一个xml文件操作对象
        doc.Load(path);                     //使用xml对象加载xml文件
        //doc.LoadXml(path); //安卓加载路径
        XmlNode root = doc.SelectSingleNode("Shop");//获取xml根节点
        XmlNodeList nodeList = root.ChildNodes;     //通过根节点获取其所有子节点
        //遍历输出
        foreach(XmlNode node in nodeList)
        {
            string id = node.ChildNodes[0].InnerText;     //增加了商品的ID编号
            string speed = node.ChildNodes[1].InnerText;
            string rotate = node.ChildNodes[2].InnerText;
            string model = node.ChildNodes[3].InnerText;
            string price = node.ChildNodes[4].InnerText;

            //1.
            //string info = string.Format("Speed:{0},Rotate:{1},Model:{2},Price:{3}", speed, rotate, model, price);
            //Debug.Log(info);

            ShopItem item = new ShopItem(id, speed, rotate, model, price);
            shopList.Add(item);
        }
    }

    /// <summary>
    /// 读取SaveData.XML文件中的数据
    /// </summary>
    public void ReadGoldAndScore(string savepath)
    {
        XmlDocument doc = new XmlDocument();//实例化一个xml文件操作对象
        doc.Load(savepath);                 //使用xml对象加载xml文件
        XmlNode root = doc.SelectSingleNode("SaveData");  //获取xml根节点
        XmlNodeList nodeList = root.ChildNodes;           //通过根节点获取其所有子节点
        //完成XML中数据的赋值
        goldCount = int.Parse(nodeList[0].InnerText);   
        heightScore = int.Parse(nodeList[1].InnerText);

        //获取商品购买状态
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 修改XML中的数据
    /// </summary>
    public void ChangeXMLData(string path, string key, string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                doc.Save(path);          //存回原来的路径
            }
        }
    }

}
