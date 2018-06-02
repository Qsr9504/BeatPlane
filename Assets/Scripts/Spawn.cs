using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject enemy0Prefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    public GameObject awardTyoe0;
    public GameObject awardTyoe1;

    public float enemy0Rate = 0.5f;
    public float enemy1Rate = 5f;
    public float enemy2Rate = 8f;

    public float award0Rate = 16f;
    public float award1Rate = 32f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CreatEnemy0", 1, enemy0Rate);//0号敌机的生成
        InvokeRepeating("CreatEnemy1", 3, enemy1Rate);//1号敌机的生成
        InvokeRepeating("CreatEnemy2", 6, enemy2Rate);//2号敌机的生成
        InvokeRepeating("CreatAwardType0", 8, enemy2Rate);//0号奖励品的生成
        InvokeRepeating("CreatAwardType1", 10, enemy2Rate);//1号奖励品的生成
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatEnemy0()
    {
        //限制0号敌机生成时的x坐标范围
        float x0 = Random.Range(-2.15f,2.15f);
        GameObject.Instantiate(enemy0Prefab, new Vector3(x0,this.transform.position.y,0), Quaternion.identity);
    }

    public void CreatEnemy1()
    {
        //限制1号敌机生成时的x坐标范围
        float x1 = Random.Range(-2.06f, 2.06f);
        GameObject.Instantiate(enemy1Prefab, new Vector3(x1, this.transform.position.y, 0), Quaternion.identity);
    }

    public void CreatEnemy2()
    {
        //限制2号敌机生成时的x坐标范围
        float x2 = Random.Range(-1.58f, 1.58f);
        GameObject.Instantiate(enemy2Prefab, new Vector3(x2, this.transform.position.y, 0), Quaternion.identity);
    }
    //生成0号奖励品
    public void CreatAwardType0()
    {
        //限制0号奖励品生成时的x坐标范围
        float a_x0 = Random.Range(-2.1f, 2.1f);
        AudioSource audioSourse = this.GetComponent<AudioSource>();
        audioSourse.Play();
        GameObject.Instantiate(awardTyoe0, new Vector3(a_x0, this.transform.position.y, 0), Quaternion.identity);
    }
    //生成1号奖励品
    public void CreatAwardType1()
    {
        //限制1号奖励品生成时的x坐标范围
        float a_x1 = Random.Range(-2.1f, 2.1f);
        AudioSource audioSourse = this.GetComponent<AudioSource>();
        audioSourse.Play();
        GameObject.Instantiate(awardTyoe1, new Vector3(a_x1, this.transform.position.y, 0), Quaternion.identity);
    }
}
