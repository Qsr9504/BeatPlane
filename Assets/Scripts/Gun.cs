using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float rate = 1f;

    public GameObject bullet;

    public void fire()
    {
        //表示初始化一个Gameobject , 初始化一个bullet 在当前位置，没有任何旋转角度
        GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
    }
    public void openFire()
    {
        InvokeRepeating("fire", 1, rate);//表示一秒之后开始以rate的速率调用某个方法
    }

    public void stopFire() {
        CancelInvoke("fire");
    }
}
