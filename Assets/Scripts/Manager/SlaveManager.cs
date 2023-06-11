using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveManager : Singleton<SlaveManager>
{
    [SerializeField] private Transform[] slavesPoint;
    [SerializeField] private Transform[] slavesLeavePoint;
    [SerializeField] private GameObject slavePrefab;
    [SerializeField] public List<Slave> slavesList = new List<Slave>();
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
        for (int i = 0; i < slavesPoint.Length; i++)
        {
            SpawnSlave(i);
        }
    }
    private void SpawnSlave(int point)
    {
        GameObject slaveObj = Instantiate(slavePrefab, slavesPoint[point].position, slavesPoint[point].rotation);
        slaveObj.GetComponent<Slave>().slavesPoint = slavesPoint;
        slaveObj.GetComponent<Slave>().slavesLeavePoint = slavesLeavePoint;
        slaveObj.GetComponent<Slave>().currentPoint = point;
        slavesList.Add(slaveObj.GetComponent<Slave>());
    }
    public void NextRound()
    {
        foreach (Slave slave in slavesList)
        {
            slave.currentPoint += 1;

            GameManager.Instance.gameState = GameState.Start;

        }

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
