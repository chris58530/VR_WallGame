using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoBt : MonoBehaviour
{
    public GameObject gun;
    public GameObject rightHand;

    public void CloseWindow()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    public void TakeGun()
    {
        gun.transform.position = rightHand.transform.position;
        gun.transform.parent = rightHand.transform;
        gun.transform.rotation = Quaternion.identity;
        //Debug.Log("TakeGun");
    }

}
