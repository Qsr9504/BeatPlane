using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Award : MonoBehaviour {
    public int type = 0;//0 gun 1 boom
    public float speed = 1.5f;//速度
    private bool isStartAudio = false;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (this.transform.position.y < -4.7)
        {
            Destroy(this.gameObject);
        }
    }
}
