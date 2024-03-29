using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    private float threshold = 20f;
    public bool positiveY = false;
    public bool negativeY = false;
    public bool positiveX = false;
    public bool negativeX = false;
    public bool isHeadShaking;
    public bool isHeadNodding ;
    public bool canDetect = false;

    [SerializeField] private Vector3 initialRotation;
    void Update()
    {
        DetectPlayerHead();
        CamRay();
    }
  
    private void Start()
    {
        initialRotation = transform.eulerAngles;
    }
    public void DetectPlayerHeadToStart()
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
            if (currentXRotation > 100) return;
            negativeX = true;
            Debug.Log("nagative true");
        }



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
            GameManager.Instance.gameState = GameState.Start;
            GameManager.Instance.GameStart = true;
            ReStart();
            Debug.Log("start game");
            return;

        }
        if (isHeadNodding)
        {
            GameManager.Instance.gameState = GameState.Start;
            GameManager.Instance.GameStart = true;
            Debug.Log("start game");

            ReStart();
            return;
        }
    }
    public void DetectPlayerHead()
    {
        Vector3 minAngles = new Vector3(-10f, -185f, -10f);
        Vector3 maxAngles = new Vector3(10f, 185f, 10f);

        // if (transform.eulerAngles.x >= minAngles.x && transform.eulerAngles.x <= maxAngles.x &&
        //     transform.eulerAngles.y >= minAngles.y && transform.eulerAngles.y <= maxAngles.y
        //    )
        // {
        //     canDetect = true;
        // }
        if (!canDetect) return;

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
            if (currentXRotation > 100) return;
            negativeX = true;
            // negativeX = false;
            // positiveX = false;
        }

        // if (currentXRotation < 350 && currentXRotation > 300)
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

            if (ScoreManager.Instance != null)
                ScoreManager.Instance.UpdateScore(SlaveType.wrong);
            Debug.Log("Head shaking detected");

            return;
        }
        if (isHeadNodding)
        {
            // 在這裡處理點頭動作
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.UpdateScore(SlaveType.right);


            Debug.Log("Head nodding detected");
            return;
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
    }
    public void CamRay()
    {
        Debug.Log("camera ray +++++++++++");
        GameObject playerCam = this.gameObject;
        int layerMask = 1 << 8;

        //開啟除Layer8之外的其他層
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 4.0f, layerMask))
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");

            if (hit.transform.tag == "Slave")
            {
                canDetect = true;
            }

        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * 4.0f, Color.red);


        }
    }
}
