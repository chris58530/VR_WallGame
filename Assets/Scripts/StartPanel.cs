using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.BeforePlayerChoose += SetAct;
    }
    private void SetAct()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Actions.BeforePlayerChoose -= SetAct;
    }
}
