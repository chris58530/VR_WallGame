using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveManager : Singleton<SlaveManager>
{
    [SerializeField] private Transform[] slavesPoint;
    [SerializeField] private Transform[] slavesLeavePoint;
    [SerializeField] private GameObject[] slavePrefab;
    [SerializeField] public List<Slave> slavesList = new List<Slave>();
    public Slave currentSlave = null;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        Actions.GameInitialize += Initailize;
        Actions.BeforePlayerChoose += RoundStart;
        Actions.AfterPlayerChoose += NextRound;
    }


    private void Initailize()
    {
        for (int i = 0; i < slavesPoint.Length; i++)
        {
            SpawnSlave(i);

            currentSlave = slavesList[0];
        }

    }
    private void SpawnSlave(int point)
    {
        int i = Random.Range(0, slavePrefab.Length);
        GameObject slaveObj = Instantiate(slavePrefab[i], slavesPoint[point].position, slavesPoint[point].rotation);
        slaveObj.GetComponent<Slave>().slavesPoint = slavesPoint;
        slaveObj.GetComponent<Slave>().slavesLeavePoint = slavesLeavePoint;
        slaveObj.GetComponent<Slave>().currentPoint = point;
        slavesList.Add(slaveObj.GetComponent<Slave>());
        Debug.Log("Spawn Slave");
    }
    private void NextRound()
    {
        SpawnSlave(0);
        foreach (Slave slave in slavesList)
        {
            slave.currentPoint += 1;
        }
    }
    private void RoundStart()
    {
        foreach (Slave slave in slavesList)
        {
            if (slave.currentPoint == slavesPoint.Length)
            {
                currentSlave = slave;
                Debug.Log(slave.currentPoint);
            }
        }
    }
    private void OnDisable()
    {
        Actions.GameInitialize -= Initailize;
        Actions.BeforePlayerChoose -= RoundStart;

        Actions.AfterPlayerChoose -= NextRound;

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
