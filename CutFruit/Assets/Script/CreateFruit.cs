using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 生成水果
 */
public class CreateFruit : MonoBehaviour {

    public GameObject[] HeartPrefab;//爱心的集合
    public GameObject[] fruitPrefabs;//水果的集合
    public GameObject BombPrefab;//炸弹

    float timer = 0;//定时器
    float z = 0;//水果z轴
    AudioSource aud;  //声源组件

    void Start()  //初始化
    {
        aud = GetComponent<AudioSource>();
    }
    void Update()
    {
        Spawn();
    }
    //生产水果 或者 炸弹
    void Spawn()     //卵生
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
           
            aud.Play();
            timer = 0;
            GameObject prefab = null;
            if (Random.value >0.8f)
            {
                prefab = BombPrefab;
                float x = Random.Range(-9f, 9f);
                Vector3 ps = new Vector3( x,transform.position.y,z);
                z--;
                if (z <= -30f)
                {
                    z = 0;
                }
               GameObject bomb = Instantiate(prefab , ps, Random.rotation) as GameObject;
               bomb.GetComponent<Rigidbody>().AddForce(new Vector3( -x*Random.Range(0.8f,1.2f),-Physics.gravity.y*Random.Range(1.0f,1.5f),0),ForceMode.Impulse);
            }
            else
            {
                //这一帧，我们要创建几个水果[3,5]
                for (int i = 0; i < Random.Range(2,4); i++)
                {
                    prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
                    float x = Random.Range(-9f, 9f);
                    Vector3 ps = new Vector3( x,transform.position.y,z);
                    z--;
                    if (z <= -30f)
                    {
                        z = 0;
                    }   
                    GameObject fruit = Instantiate(prefab , ps, Random.rotation) as GameObject;
                    fruit.GetComponent<Rigidbody>() .AddForce(new Vector3(-x * Random.Range(0.8f, 1.2f), -Physics.gravity.y * Random.Range(1.0f, 1.5f), 0), ForceMode.Impulse);
                }

                ////生成爱心
                //for (int j = 0; j < 3; j++)
                //{
                //    for (int i = 0; i < 1; i++)
                //    {
                //        prefab = HeartPrefab[j];
                //        Vector3 ht = new Vector3(j * 2.1f, (i - 1) * 1.2f, 0); //每次生成一个爱心位置发生偏移
                //        GameObject heart = Instantiate(prefab, ht, Random.rotation) as GameObject;
                //    }
                //}
                
            }
        }
    }
}
