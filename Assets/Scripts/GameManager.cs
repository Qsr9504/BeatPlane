using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState {
    Running,Pause
}
public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public int score = 0;//分数
    public Text scoreText;
    public GameState gameState = GameState.Running;//默认游戏状态为运行中


    //private GameManager() { }//私有化构造函数

    private void Awake() {
        _instance = this;
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Score:" + score;
    }
    public void transfromGameState() {//切换游戏状态
        if (gameState == GameState.Running) {
            PauseGame();
        }
        //if (gameState == GameState.Pause) {
        //    ContinueGame();
        //}
        else {
            /** 注意此处如果换成是if(gameState == GameState.Pause),就会出现问题
                原因就是当执行暂停游戏操作时，PauseGame();执行，
                此时就有可能出现主线程继续走，但是PauseGame()执行了第一行gameState = GameState.Pause;
                主线程继续走，下一个if判断就通过了，就可以再次解锁，导致暂停按钮没有用

                想，如果是PauseGame()里边Time.timeScale = 0;gameState = GameState.Pause;顺序是这样
                就有可能先执行了暂停，所有的动画都暂停了，主线程继续走，但是gameState = GameState.Pause;没有执行
                也就不会被继续判断，就会立刻继续游戏，暂停只会暂停一帧或者几帧
            */
            ContinueGame();
        }
    }
    public void PauseGame() {//暂停游戏
        gameState = GameState.Pause;
        //主要原因是我们所有的物体的移动都是依赖着time.delatTime的
        //如果把这个归零并且不再变化，那么一切物体都会被暂停
        Time.timeScale = 0;//time.delatTime = 0
    }
    public void ContinueGame() {//继续游戏
        gameState = GameState.Running;
        Time.timeScale = 1;
    }
}
