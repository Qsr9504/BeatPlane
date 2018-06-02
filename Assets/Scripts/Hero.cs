using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    public bool ani = true;//是否播放飞行动画
    public int frameCountPersconds = 10;//每秒播放次数
    public float timer = 0; //时间计时器
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private bool isMouseDown = false; //检测手指是否按下

    private Vector3 lastMousePosition = Vector3.zero;

    private Transform hero;//玩家飞机
    private int gunCount = 1;//枪的数量

    public float superGunTime = 10f;//子弹奖励时长
    private float resetSuperGunTime;//重置子弹奖励时长
    public Gun gunTop;
    public Gun gunLeft;
    public Gun gunRight;

    public AudioClip[] audioClips;//撞击英雄机后的各种音效
    public AudioSource audioSource;

    public bool isDeath = false;//英雄机是否存活
    private int frameIndex = 0;
    private float explosionAnimationFrame;
    public Sprite[] explosionSprites;//英雄机爆炸动画


    // Use this for initialization
    void Start() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //通过TAG获取unity中hero的transform属性
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = this.GetComponent<AudioSource>();
        resetSuperGunTime = superGunTime;
        superGunTime = 0;
        gunTop.openFire();//单一开火
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentMousePosition;
        if (GameManager._instance.score % 10000 == 0 && GameManager._instance.score != 0) {
            //每达到一万分就播放一个成就音乐
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        //英雄机死亡时动画
        //if (isDeath) {
        //    Time.timeScale = 0;
        //    if (frameIndex >= explosionSprites.Length) {//如果播放帧数大于等于爆炸动画长度
        //        Destroy(this.gameObject);
        //    }
        //    else {
        //        //播放爆炸动画
        //        spriteRenderer.sprite = explosionSprites[frameIndex++];
        //    }
        //}
        //英雄机飞行动画
        if (ani) {
            timer += Time.deltaTime;//获取计时器
            float frameIndex = timer / (1f / frameCountPersconds);
            int frame = (int)frameIndex % 2;
            this.GetComponent<SpriteRenderer>().sprite = sprites[frame];
        }
        if (Input.GetMouseButtonDown(0)) {//检测鼠标左键按下
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))//鼠标抬起
        {
            isMouseDown = false;
            lastMousePosition = Vector3.zero;//每次按下抬起之后将上一次归零
        }
        if (isMouseDown && GameManager._instance.gameState == GameState.Running)//如果手指在屏幕上按下
        {
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print(Input.mousePosition);
            if (lastMousePosition != Vector3.zero) {
                //Input.mousePosition返回的是像素坐标，需要转换为世界坐标
                //Camera.main.ScreenToWorldPoint(Input.mousePosition)

                Vector3 offset = currentMousePosition - lastMousePosition;//上一帧与当前帧的位移差
                CheckPosition(transform.position, offset);//位置检测
                this.transform.position = transform.position + offset;//更新当前hero位置

            }
            lastMousePosition = currentMousePosition;//更新手指触摸位置
        }
        superGunTime -= Time.deltaTime;
        if (superGunTime > 0) {
            //先检查当前武器类型，如果不是超级武器模式，就进行切换
            if (gunCount == 1) {
                transformToSuperGun();
            }
        }
        else {
            if (gunCount == 2) {
                transformToNormalGun();
            }
        }
    }
    //切换为超级武器
    private void transformToSuperGun() {
        gunCount = 2;
        gunLeft.openFire();
        gunRight.openFire();
        gunTop.stopFire();
    }
    //切换为普通武器
    private void transformToNormalGun() {
        gunCount = 1;
        gunLeft.stopFire();
        gunRight.stopFire();
        gunTop.openFire();
    }

    private void CheckPosition(Vector3 position, Vector3 offset) {
        //check x -2.2f~2.2f
        //check y -3.8f~3.4f
        //获取当前x.y的值
        float x = position.x;
        float y = position.y;
        //检测并且限制hero的x，y坐标
        if (x < -2.2f) x = -2.2f;
        if (x > 2.2f) x = 2.2f;
        if (y < -3.8f) y = -3.8f;
        if (y > 3.4f) y = 3.4f;

        this.transform.position = new Vector3(x, y, 0);
    }

    //2D碰撞检测
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "award") {
            Award award = col.GetComponent<Award>();
            if (award.type == 0) {
                //这个是子弹奖励
                print("吃到子弹奖励");
                audioSource.clip = audioClips[1];
                superGunTime = resetSuperGunTime;
                //AudioSource audioSourse = this.GetComponent<AudioSource>();
                //audioSourse.Play();
            }
            else {
                Enemy enemy = col.GetComponent<Enemy>();
                //这个是炸弹奖励
                print("吃到炸弹奖励");
                audioSource.clip = audioClips[2];//设置音乐 
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemys.Length; i++) {
                    enemys[i].gameObject.SendMessage("ToDie");//对改对象发送消息
                }
            }
            audioSource.Play();
            Destroy(col.gameObject);//让奖励品消失
        }
        else if (col.tag == "enemy") {
            //如果是撞到了敌机
            ToDie();
        }
    }
    private void ToDie() {
        /**
             * 1.英雄机爆炸
             * 2.通知摄像机播放声效
             * 3.游戏停止，并弹出分数
             * **/
        print("英雄机已死");
        isDeath = true;
    }
}
