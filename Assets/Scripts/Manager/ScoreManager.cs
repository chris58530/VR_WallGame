using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private GameObject[] wall;
    [SerializeField] private Transform PlayerPoiont;
    [SerializeField] private Transform EndPoint;
    float Timer = 10;
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
        Actions.GameEnd += WallActive;
        // Actions.GameBegin += SetPlayerCam;
        Actions.GameEnd += SetPlayerCam;
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
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Timer");

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = Mathf.Floor(Timer).ToString();

            }
        }
        else
        {
            GameManager.Instance.gameState = GameState.End;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = "你的分數 : " + PlayerScore.ToString();

            }
        }


    }
    private void SetPlayerCam()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;
        player.transform.position = EndPoint.transform.position;
        player.transform.rotation = EndPoint.transform.rotation;
    }
    private void WallActive()
    {

        if (PlayerScore >= 75)
        {
            wall[0].SetActive(true);
        }
        else if (PlayerScore < 75 && PlayerScore >= 50)
        {
            wall[1].SetActive(true);

        }
        else if (PlayerScore < 50 && PlayerScore >= 25)
        {

            wall[2].SetActive(true);
        }
        else
        {
            wall[3].SetActive(true);

        }

    }
    private void OnDisable()
    {
        Actions.GameInitialize -= Initailize;
        Actions.GameUpdate -= TimeCount;
        Actions.GameEnd -= WallActive;
        // Actions.GameBegin -= SetPlayerCam;
        Actions.GameEnd -= SetPlayerCam;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
