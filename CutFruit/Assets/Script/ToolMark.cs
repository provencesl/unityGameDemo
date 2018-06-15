using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolMark : MonoBehaviour {

    LineRenderer lineRender;
    bool isMouseFirstDown = false;//鼠标是否第一次按下
    bool isMouseHoldOn = false;//鼠标是否一直按住
    Vector3 currentPs;//当前鼠标的位置
    Vector3 lastPs;//上一阵鼠标的位置
    //条件：如果连续两针的鼠标的位置大于0.01个单位
    Vector3[] points;//保存符合条件的鼠标的位置
    int pointCount=0;//记录已经保存的位置的个数
    AudioSource aud;//

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        lineRender= GetComponent<LineRenderer>();
        lineRender.positionCount = 20;//设置线性渲染组件顶点的数量
        points = new Vector3[lineRender.positionCount];   //数组的长度

       
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            aud.Play();//播放挥刀的音效
            isMouseFirstDown = true;
            isMouseHoldOn = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseHoldOn=false;
        }
        DrawLine();
        isMouseFirstDown = false;     //第二次点击为False
	}

    void DrawLine()
    {
        if (isMouseFirstDown)
        {
                       //  开始划线的时候,记录鼠标的位置，并赋值给Current和Last
            currentPs = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastPs = currentPs;
            //初始化，数组中已经保存的点的个数
            pointCount = 0;
        }

        if (isMouseHoldOn)
        {
            //每一帧都保存一下鼠标的位置
            currentPs = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(currentPs,lastPs)>0.01f)
            {
                //保存满足条件的点----current
                SavePoint(currentPs);
                
                //
                Cut(currentPs);
                pointCount++;
            }
            //更换旧的LastPs
            lastPs = currentPs;
        }
        else
        {
            //清空数组-----给数组赋新的值
            points = new Vector3[lineRender.positionCount];
        }

        //LineRender通过指定的顶点来划线
        lineRender.SetPositions(points);
    }
    void SavePoint(Vector3 position)
    {
        position.z = -1;
        //已经保存的点的个数小于，lineRender所需顶点的个数
        if (pointCount < lineRender.positionCount)
        {
            for (int i = pointCount; i < lineRender.positionCount; i++)
            {
                //将没有赋值的点，全部等于当前这一帧鼠标的位置
                points[i] = position;
            }
        }
        else
        {
            for (int i = 0; i < lineRender.positionCount-1; i++)
            {
                points[i] = points[i + 1];
            }
            points[lineRender.positionCount-1] = position;
        }
    }

    //检测是否切到了水果
    void Cut(Vector3 ps)
    {
        Vector3 screenPoint=Camera.main.WorldToScreenPoint(ps);
        //获取一条射线
        Ray ray=Camera.main.ScreenPointToRay (screenPoint);
        //发射射线，RaycastHit[]这个数组保存了射线所打到的所有游戏物体
        RaycastHit []  hits = Physics.RaycastAll(ray);//hit存放被射中的物体
       //遍历被射线射中的水果，切他；
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].collider.GetComponent<Fruit>().Cut();
            if (hits[i].collider.tag=="Bomb")
            {
                ScoreScript.instance.DownLife();
            }
        }
    }


}
