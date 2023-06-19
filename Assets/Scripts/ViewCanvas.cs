using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCanvas : Singleton<ViewCanvas>
{
    [SerializeField] private GameObject[] endCam;

    [SerializeField] private GameObject[] answerRight;
    [SerializeField] private GameObject[] answerWrong;

    void OnEnable()
    {
        Actions.GameEnd += MakeActive;
    }
    void OnDisable()
    {
        Actions.GameEnd -= MakeActive;
    }
    void MakeActive()
    {
        for (int i = 0; i < endCam.Length; i++)
        {
            endCam[i].SetActive(true);

        }
    }
    public void PlayAnswerView(bool b)
    {
        if (b)
        {
            for (int i = 0; i < answerRight.Length; i++)
            {
                answerRight[i].GetComponent<Animator>().SetTrigger("Answer");
            }

        }
        else
        {
            for (int i = 0; i < answerWrong.Length; i++)
            {
                answerWrong[i].GetComponent<Animator>().SetTrigger("Answer");
            }

        }
    }
}
