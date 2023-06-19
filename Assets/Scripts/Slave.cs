using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slave : MonoBehaviour
{
    [HideInInspector] public Transform[] slavesPoint;
    [HideInInspector] public Transform[] slavesLeavePoint;
    private float speed = 10;
    public SlaveType slaveType;
    [SerializeField, TextArea(4, 10)] private string[] text;
    public int currentPoint;
    private int leavePoint = 0;
    private int textNum;
    Animator ani;
    void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        Actions.SlaveMove += MoveToNextPoint;

    }
    private void Start()
    {
        textNum = Random.Range(0, text.Length);
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

        if (currentPoint == slavesPoint.Length - 1)
        {
            ShowTextOnUI();
        }
        if (Vector3.Distance(transform.position, slavesPoint[currentPoint].position) < 1f)
        {
            ani.Play("Idle");
            return;
        }
        ani.Play("Walking");

        transform.position = Vector3.MoveTowards(transform.position, slavesPoint[currentPoint].position, speed * Time.deltaTime);
    }
    private void MoveToLeavePoint()
    {
        ani.Play("Walking");

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
    public void ShowTextOnUI()
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Dialog");
        if (GameManager.Instance.gameState == GameState.End)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = "呼! 城牆蓋好囉大爺!";

            }
        }
        else
        {

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = text[textNum].ToString();

            }
        }
    }
}
