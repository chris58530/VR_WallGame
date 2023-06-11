using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slave : MonoBehaviour
{
    [SerializeField] public Transform[] slavesPoint;
    [SerializeField] public Transform[] slavesLeavePoint;
    [SerializeField] private float speed;
    public int currentPoint;
    private int leavePoint = 0;
    private void OnEnable()
    {
        Actions.SlaveMove += MoveToNextPoint;

    }
    private void OnDisable()
    {
        Actions.SlaveMove -= MoveToNextPoint;

        Actions.SlaveLeave -= MoveToLeavePoint;
    }
    private void MoveToNextPoint()
    {
        if (currentPoint >= slavesPoint.Length)
        {
            Actions.SlaveMove -= MoveToNextPoint;
            Actions.SlaveLeave += MoveToLeavePoint;
            return;
        }
        if (Vector3.Distance(transform.position, slavesPoint[currentPoint].position) < 1f) return;
        transform.position = Vector3.MoveTowards(transform.position, slavesPoint[currentPoint].position, speed * Time.deltaTime);
    }
    private void MoveToLeavePoint()
    {

        if (leavePoint == 0)
        {
            if (Vector3.Distance(transform.position, slavesLeavePoint[0].position) < 1f)
                leavePoint = 1;
            transform.position = Vector3.MoveTowards(transform.position, slavesLeavePoint[0].position, speed * Time.deltaTime);

        }
        if (leavePoint == 1)
        {

            transform.position = Vector3.MoveTowards(transform.position, slavesLeavePoint[1].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, slavesLeavePoint[1].position) < 1f)
            {
                Destroy(gameObject);
                SlaveManager.Instance.slavesList.Remove(this);
            }

        }

    }
}
