using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroManager : Singleton<IntroManager>
{
    [SerializeField, TextArea(4, 10)] private string[] text;

    [SerializeField]private bool shake;
    [SerializeField]private bool nod ;
    [SerializeField]private IntroPlayer player;

    private void Start()
    {

    }
    private void Update()
    {
        DectectIntro();
    }
    private void DectectIntro()
    {
        if (player.isHeadShaking)
        {
            shake = true;
            GameObject[] texts = GameObject.FindGameObjectsWithTag("Dialog");

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].GetComponent<TMP_Text>().text = "再來請點頭給我看好不好?";

            }
        }
        if (shake)
        {
            if (player.isHeadNodding)
            {
                nod = true;
                StartCoroutine(GoToNext());
                return;
            }

        }
    }
    IEnumerator GoToNext()
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Dialog");

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].GetComponent<TMP_Text>().text = "長官你恢復記憶了! 請馬上來蓋城牆";

        }
        yield return new WaitForSeconds(3);
    UnityEngine.SceneManagement.SceneManager.LoadScene("VR6DOFfWalking");
    }
}
