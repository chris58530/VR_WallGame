using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : Singleton<ScoreManager>
{
    public int PlayerScore;
    SlaveType slaveType;

    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        Actions.GameInitialize += Initailize;
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
    private void OnDisable()
    {
        Actions.GameInitialize -= Initailize;

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
