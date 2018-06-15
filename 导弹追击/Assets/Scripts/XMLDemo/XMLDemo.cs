using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;   //引入XML相关的命名空间

public class XMLDemo : MonoBehaviour {

    private string Xmlpath = "Assets/Datas/web.xml"; //获取xml文件的路径
	void Start () {

        ReadbyXmlpath(Xmlpath);
	}

    /// <summary>
    /// 读取xml文件中数据的方法
    /// </summary>
    /// <param name="path"></param>
    private void ReadbyXmlpath(string path)
    {
        XmlDocument doc = new XmlDocument(); //1.实例化XML文本操作对象

        doc.Load(path);  //2.使用XML对象加载XML

        XmlNode root = doc.SelectSingleNode("Web"); //3.获取根节点

        XmlNodeList nodeList =  root.ChildNodes;  //4.获取根节点下所有子节点

        //5.遍历输出
        foreach (XmlNode node in nodeList)  
        {
            int id = int.Parse(node.Attributes["id"].Value);
            string name = node.ChildNodes[0].InnerText;
            string url = node.ChildNodes[1].InnerText;

            Debug.Log(id + "--" + name + "--" + url);
        }
    }
	
}
