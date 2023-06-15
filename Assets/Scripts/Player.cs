using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float threshold = 10f;
    public bool positiveY = false;
    public bool negativeY = false;
    public bool positiveX = false;
    public bool negativeX = false;
    private bool isHeadShaking = false;
    private bool isHeadNodding = false;
    public bool canDetect = false;

    [SerializeField] private Vector3 initialRotation;

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
    private void Start()
    {
        initialRotation = transform.eulerAngles;
        ReStart();
    }
    public void DetectPlayerHead()
    {
        Vector3 minAngles = new Vector3(-5f, -185f, -5f);
        Vector3 maxAngles = new Vector3(5f, 185f, 5f);

        if (transform.eulerAngles.x >= minAngles.x && transform.eulerAngles.x <= maxAngles.x &&
            transform.eulerAngles.y >= minAngles.y && transform.eulerAngles.y <= maxAngles.y
           )
        {
            canDetect = true;
        }
        if (!canDetect) return;

        Debug.Log("DetectPlayerHead");
        float currentXRotation = transform.eulerAngles.x;
        float currentYRotation = transform.eulerAngles.y;

        if ((currentYRotation - initialRotation.y) > threshold)
        {
            negativeY = true;
            // negativeX = false;
            // positiveX = false;
        }

        if ((initialRotation.y - currentYRotation) > threshold)
        {
            positiveY = true;

            // negativeX = false;
            // positiveX = false;
        }

        if ((currentXRotation >= threshold))
        {
            negativeX = true;
            Debug.Log("nagative true");
            // negativeX = false;
            // positiveX = false;
        }

        // if (currentXRotation < -threshold)
        // {
        //     positiveX = true;

        // }

        if (positiveY && negativeY)
        {
            isHeadShaking = true;
            isHeadNodding = false;
        }


        if (negativeX)
        {
            isHeadNodding = true;
            isHeadShaking = false;
        }

        if (isHeadShaking)
        {
            // 在這裡處理搖頭動作

            ScoreManager.Instance.UpdateScore(SlaveType.right);
            ReStart();
            Debug.Log("Head shaking detected");


        }
        if (isHeadNodding)
        {
            // 在這裡處理點頭動作

            ScoreManager.Instance.UpdateScore(SlaveType.wrong);

            ReStart();

            Debug.Log("Head nodding detected");
        }

    }
    public void ReStart()
    {
        isHeadShaking = false;
        isHeadNodding = false;
        canDetect = false;
        positiveX = false;
        positiveY = false;
        negativeX = false;
        negativeY = false;
        GameManager.Instance.gameState = GameState.NextRound;
    }

}
