using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCanvas : MonoBehaviour
{
    [SerializeField] private GameObject[] endCam;

    void OnEnable()
    {
        Actions.GameEnd += mackeActive;
    }
    void OnDisable()
    {
        Actions.GameEnd -= mackeActive;
    }
    void mackeActive()
    {
        for (int i = 0; i < endCam.Length; i++)
        {
            endCam[i].SetActive(true);

        }
    }
}
