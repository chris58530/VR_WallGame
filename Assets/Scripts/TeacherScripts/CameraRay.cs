using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public GameObject playerCam;
    public Animator leftCrossAnim;
    public Animator rightCrossAnim;
    //private string objTag;

    public enum Hitted
    {
        None,
        Switch,
        Gun,
        DoBt,
        CancelBt,
    }

    private Hitted hitState = Hitted.None;
    public Hitted HitState
    {
        get { return hitState; }
    }

    private void SetState(string myTag){
        switch(myTag)
            {
                case "None":
                    hitState = Hitted.None;
                    break;
                case "Switch":
                    hitState = Hitted.Switch;
                    break;
                case "DoBt":
                    hitState = Hitted.DoBt;
                    break;
                case "CancelBt":
                    hitState = Hitted.CancelBt;
                    break;
            }
    }

    private void FixedUpdate()
    {
        //補充：
        //layerMask是一個int32的資料，但使用二進位制共32位(0~31)來表示layer開啟狀態，例如100000就表示Layer5為開啟狀態
        //位移運算子：左移運算符，左移表示乘以2，左移多少位表示乘以2的幾次冪。
        //例如：var temp = 14 << 2; 表示十進位數14轉化為二進位後向左移動2位。temp最後計算的值為 14乘以2的平方，temp = 56;
        //同理，右移運算符，移動多少位表示除以2的幾次冪。
        //舉例來說 LayerMask mask = 1 << 3; 表示開啟Layer3。
        //因為0001位元值加總為:2的三次方*0+2的二次方*0+2２的一次方*0+2的零次方*1=1)，向左位移後變成了1000，也就是1*2的三次方為8，這裡也可計算每個位元的值的加總8+0+0+0=8，一樣為8)
        //以下為其他應用例子：
        //LayerMask mask = 1 << 1 | 1 << 9; 表示開啟Layer1和Layer9。
        //LayerMask mask = 0 << 4 | 0 << 5; 表示關閉Layer4和Layer5。
        //LayerMask mask = ~（1 << 0） 打開所有的層。
        //LayerMask mask = ~（1 << 9） 打開除了第9之外的層。
        //原文链接：https://blog.csdn.net/u014361280/article/details/112671632

        //設定Layer8用來表示Pplayer，開啟Layer8
        int layerMask = 1 << 8;

        //開啟除Layer8之外的其他層
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 2.0f, layerMask))
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
            leftCrossAnim.SetBool("Focus", true);
            rightCrossAnim.SetBool("Focus", true);

            if(hit.transform.tag=="Switch")
            {
                SetState("Switch");
            }
            else if(hit.collider.gameObject.tag=="DoBt")
            {
                SetState("DoBt");
            }
            else if(hit.collider.gameObject.tag=="CancelBt")
            {
                SetState("CancelBt");
            }
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * 2.0f, Color.white);
    
            leftCrossAnim.SetBool("Focus", false);
            rightCrossAnim.SetBool("Focus", false);
            SetState("None");
        }
    }

}
