using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 水果脚本
 */
public class Fruit : MonoBehaviour {
    
    
    //特效区
    public AudioClip bombClip;     //生成炸弹的音效
    public AudioClip clip;
    public GameObject SplashFlat;//平面飞溅
    public GameObject Splash;//飞溅
    public GameObject FireWork;//炸弹特效
    public GameObject halfFruit;//一半的水果

    private GameObject[] HeartLife;  //生命值心的数量

    //分数
    public float score = 0;//默认分数

	void Start () 
    {

        Destroy(gameObject, 4);
        //判断水果或炸弹，
        if (gameObject.tag=="Bomb")
        {
            AudioSource.PlayClipAtPoint(bombClip, Camera.main.transform.position);      //播放声音
        }
        
	}

    //被刀切
    public void Cut()
    {

        ScoreScript.instance.UpdateScore(score);
        //.1播放被切的声音和特效
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        if (gameObject.tag=="Bomb")
        {
          //生成炸弹特效
            Instantiate(FireWork, transform.position, Quaternion.identity);
            
        }
        else
        {
            GameObject sf=Instantiate(SplashFlat,transform.position,Quaternion.identity);
            Destroy(sf, 2);
           GameObject s= Instantiate(Splash,transform.position,Quaternion.identity);//transform.position:当前位置
           Destroy(s, 2);
            //生成两个半水果
            for (int i = 0; i < 2; i++)
            {
                GameObject go=   Instantiate(halfFruit, transform.position, Random.rotation);//切完后，随机旋转
                //使半颗水果沿着不同的方向飞出去
                go.GetComponent<Rigidbody>().velocity = Random.onUnitSphere*Random.Range(5,10);//在单位圆上的随机方向
                //延迟销毁半颗水果
                Destroy(go, 2);
            }
        }
        //销毁
        Destroy(gameObject);
    }

}
