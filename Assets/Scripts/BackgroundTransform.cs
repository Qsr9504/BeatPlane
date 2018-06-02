using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTransform : MonoBehaviour
{
    public static float moveSpeed = 2f;
    // Use this for initialization 用于初始化
    void Start()
    {

    }

    // Update is called once per frame  每一帧都会调用一次这个办法
    void Update()
    {
        //每一帧都往下移动这么多
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        Vector3 site = this.transform.position;
        if (site.y <= -8.52f)
        {
            this.transform.position = new Vector3(site.x, site.y + 8.52f * 2, site.z);
        }
    }
}
