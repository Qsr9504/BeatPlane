using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 5;//子弹移动速度
                           // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);//让子弹运动
        if (transform.position.y > 5f)//如果子弹超出屏幕就直接销毁
        {
            Destroy(this.gameObject);
        }
    }
    //碰撞触发器
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "enemy") {//判定为敌机
            if (!collision.GetComponent<Enemy>().isDeath) {//判定敌机没有死
                collision.gameObject.SendMessage("BeHit");
                GameObject.Destroy(this.gameObject);//摧毁当前子弹
            }
        }
    }
}
