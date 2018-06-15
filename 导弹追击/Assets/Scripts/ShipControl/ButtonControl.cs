using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

    private Ship m_Ship;

    private GameObject Left;
    private GameObject Right;
	
	void Start () {

        Left = GameObject.Find("Left");
        Right = GameObject.Find("Right");

        m_Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        UIEventListener.Get(Left).onPress = ButtonLeft;
        UIEventListener.Get(Right).onPress = ButtonRight;
		
	}

    //反馈按下左按钮，飞机向左飞行
    private void ButtonLeft(GameObject go, bool isPress)
    {
        if (isPress)
        {
            m_Ship.IsLeft = true;
        }
        else 
        {
            m_Ship.IsLeft = false;
        }
    }
    //反馈按下右按钮，飞机向右飞行
    private void ButtonRight(GameObject go, bool isPress)
    {
        if (isPress)
        {
            m_Ship.IsRight = true;
        }
        else
        {
            m_Ship.IsRight = false;
        }
    }
	
		
}
