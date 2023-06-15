using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : Singleton<ScoreManager>
{
    public float Timer = 30;
    public int PlayerScore;
    SlaveType slaveType;

    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        Actions.GameInitialize += Initailize;
        Actions.GameUpdate += TimeCount;
    }

    private void Initailize()
    {
        PlayerScore = 0;
    }
    public void UpdateScore(SlaveType type)
    {
        slaveType = SlaveManager.Instance.currentSlave.slaveType;
        if (type == slaveType)
            PlayerScore += 1;
        else PlayerScore -= 1;

        Debug.Log($"目前玩家分數 : {PlayerScore}");
        TMP_Text text = GameObject.Find("Score").GetComponent<TMP_Text>();
        text.text = PlayerScore.ToString();
    }
    public void TimeCount()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Timer");
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].GetComponent<TMP_Text>().text = Mathf.Floor(Timer).ToString();

        }
    }
    private void OnDisable()
    {
        Actions.GameInitialize -= Initailize;
        Actions.GameUpdate -= TimeCount;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
