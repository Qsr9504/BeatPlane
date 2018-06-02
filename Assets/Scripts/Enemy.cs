using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    smallEnemy, middleEnemy, bigEnemy
}
public class Enemy : MonoBehaviour {
    public int hp = 1;//血量
    public int speed = 2;//运动速度
    public int score = 100;//敌机被击杀的分数
    public EnemyType type;
    public bool isDeath = false;//标志位

    public Sprite[] explosionSprites;//死亡动画
    private float timer = 0;//计时器
    public int explosionAnimationFrame = 10;//动画播放帧数

    private SpriteRenderer render;

    public float hitTimer = 0.2f;//碰撞时间
    private float resetHitTime;

    public Sprite[] hitSprites;//被击中的动画

    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Use this for initialization
    void Start() {
        render = this.GetComponent<SpriteRenderer>();
        resetHitTime = hitTimer;
        if(type == EnemyType.bigEnemy) {
            //如果是大飞机
            print("大飞机！");
            audioSource = this.GetComponent<AudioSource>();
            audioSource.clip = audioClips[1];
            audioSource.loop = true;
            audioSource.Play();
        }
        hitTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        int frameIndex;
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (this.transform.position.y < -5.5f) {
            //audioSourse.PlayOneShot(boomAudio,1f);//
            Destroy(this.gameObject);
        }
        if (isDeath) {
            timer += Time.deltaTime;
            frameIndex = (int)(timer / (1f / explosionAnimationFrame));
            if (frameIndex >= explosionSprites.Length) {//如果播放帧数大于等于爆炸动画长度
                Destroy(this.gameObject);
            }
            else {
                //播放爆炸动画
                render.sprite = explosionSprites[frameIndex];
            }
        }
        else {
            if (type != EnemyType.smallEnemy)
                if (hitTimer >= 0) {
                    hitTimer -= Time.deltaTime;
                    frameIndex = (int)((resetHitTime - hitTimer) / (1f / explosionAnimationFrame));
                    frameIndex %= 2;
                    render.sprite = hitSprites[frameIndex];//播放击中动画
                }
        }

    }

    public void BeHit() {
        //小敌机直接爆炸
        hp -= 1;
        if (hp <= 0) {
            ToDie();
            //播放死亡动画
        }
        else {
            hitTimer = resetHitTime;//重置
        }
    }

    public void ToDie() {
        if (!isDeath) {
            audioSource = this.GetComponent<AudioSource>();
            //设置声音
            audioSource.clip = audioClips[0];
            audioSource.Play();
            isDeath = true;
            GameManager._instance.score += score;
        }
    }
}
