using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour {

    private void OnMouseUpAsButton() {
        GameManager._instance.transfromGameState();//切换游戏状态
        AudioSource audioSourse = this.GetComponent<AudioSource>();
        audioSourse.Play();
    }
}
