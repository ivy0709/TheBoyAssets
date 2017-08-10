using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    private Transform playerPos;
    public float smoothing = 4.0f;

    // Use this for initialization
    void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
    }

    // Update is called once per frame
    void FixedUpdate() {
        // 原来:
        // 直接移动过去
        // transform.position = playerPos.position + offset;
        // 改进:
        // 缓慢的移动过去
        Vector3 targetPos = playerPos.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
	}
}
