using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void UpdateScore(SlaveType type)
    {
        slaveType = type;
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
