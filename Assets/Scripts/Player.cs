using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float threshold = 30f;
    public bool detectHeadShake = true;
    public bool detectHeadNod = true;

    private bool isHeadShaking = false;
    private bool isHeadNodding = false;
    public static bool canDetect = false;
    private Vector3 initialRotation;

    private void OnEnable()
    {
        Actions.OnPlayerChoose += DetectPlayerHead;
        Actions.AfterPlayerChoose += ReStart;
    }
    private void OnDisable()
    {
        Actions.OnPlayerChoose -= DetectPlayerHead;
        Actions.AfterPlayerChoose -= ReStart;

    }
    public void DetectPlayerHead()
    {
        if (!canDetect) return;
        float currentXRotation = transform.eulerAngles.x;
        float currentYRotation = transform.eulerAngles.y;


        if (Mathf.Abs(currentYRotation - initialRotation.y) > threshold)
        {
            isHeadShaking = true;
            isHeadNodding = false;
        }


        if (Mathf.Abs(currentXRotation - initialRotation.x) > threshold)
        {
            isHeadNodding = true;
            isHeadShaking = false;
        }

        if (isHeadShaking)
        {
            // 在這裡處理搖頭動作
            canDetect = false;
            isHeadShaking = false;
            isHeadNodding = false;
            ReStart();
            Debug.Log("Head shaking detected");


        }
        if (isHeadNodding)
        {
            // 在這裡處理點頭動作
            canDetect = false;
            isHeadShaking = false;
            isHeadNodding = false;
            ReStart();

            Debug.Log("Head nodding detected");
        }
    }
    public void ReStart()
    {
        canDetect = true;
        initialRotation = transform.eulerAngles;
        GameManager.Instance.gameState = GameState.NextRound;
    }

}
