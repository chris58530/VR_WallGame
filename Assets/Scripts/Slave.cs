using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slave : MonoBehaviour
{
    [HideInInspector] public Transform[] slavesPoint;
    [HideInInspector] public Transform[] slavesLeavePoint;
    [SerializeField] private float speed;
    public SlaveType slaveType; 
    [SerializeField,TextArea(4, 10)]private string text;
    [HideInInspector]public int currentPoint;
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
        
        if(currentPoint == slavesPoint.Length - 1)
        {
            ShowTextOnUI();
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
    public void ShowTextOnUI(){
        TMP_Text[] text = FindObjectsOfType<TMP_Text>();
        foreach(TMP_Text tmp in text)
        {
            tmp.text = text.ToString();
        }
    }
}
