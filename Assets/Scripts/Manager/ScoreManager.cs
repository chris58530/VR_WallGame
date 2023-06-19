using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private GameObject[] wall;
    [SerializeField] private Transform PlayerPoiont;
    [SerializeField] private Transform EndPoint;
    [SerializeField] private float Timer;
    [SerializeField] private GameObject winEffect;
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
        {
            ViewCanvas.Instance.PlayAnswerView(true);
            AudioManager.Instance.PlayUI("Right");
            PlayerScore += 5;

        }
        else
        {
            ViewCanvas.Instance.PlayAnswerView(false);
            AudioManager.Instance.PlayUI("Wrong");

            PlayerScore -= 1;
        }
        Debug.Log($"目前玩家分數 : {PlayerScore}");
        // TMP_Text text = GameObject.Find("Score").GetComponent<TMP_Text>();
        // text.text = PlayerScore.ToString();
        return;
    }
    public void TimeCount()
    {
        if (!GameManager.Instance.GameStart) return;
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Timer");

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = "剩餘時間 : " + Mathf.Floor(Timer).ToString();

            }
        }
        else
        {
            GameManager.Instance.gameState = GameState.End;
            if (PlayerScore > 100)
            {
                PlayerScore = 100;
            }
            else if (PlayerScore < 0)
            {
                PlayerScore = 0;
            }
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = "城牆完成度 : " + PlayerScore.ToString() + "%";
            }
        }


    }
    private void SetPlayerCam()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;
        player.GetComponent<ARCameraBackground>().enabled = false;
        player.GetComponent<ARCameraManager>().enabled = false;
        player.transform.position = EndPoint.transform.position;
        player.transform.rotation = EndPoint.transform.rotation;
    }
    private void WallActive()
    {

        if (PlayerScore >= 75)
        {
            StartCoroutine(WallActive(3));
            Actions.GameEnd -= WallActive;

        }
        else if (PlayerScore < 75 && PlayerScore >= 50)
        {
            StartCoroutine(WallActive(2));
            Actions.GameEnd -= WallActive;


        }
        else if (PlayerScore < 50 && PlayerScore >= 25)
        {

            StartCoroutine(WallActive(1));
            Actions.GameEnd -= WallActive;

        }
        else
        {
            StartCoroutine(WallActive(0));

            Actions.GameEnd -= WallActive;

        }

    }
    IEnumerator WallActive(int count)
    {
        for (int i = 0; i <= count; i++)
        {
            wall[i].SetActive(true);
            AudioManager.Instance.PlaySFX("Boom");
            yield return new WaitForSeconds(1);

        }
        AudioManager.Instance.PlayUI("Win");
        winEffect.SetActive(true);
        yield return null;

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
