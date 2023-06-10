using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuretControl : MonoBehaviour
{
    private float health;
    private bool readyToFire;
    public Transform player;

    private void Start()
    {
        health = 5f;
        readyToFire = true;
    }

    IEnumerator Attack()
    {
        readyToFire = false;
        Debug.Log("Turet Fire1");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Turet Fire2");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Turet Fire3");
        yield return new WaitForSeconds(2f);
        readyToFire = true;
        StopCoroutine(Attack());
    }

    void OnCollisionEnter()
    {

    }

    private void Update()
    {
        transform.LookAt(player.transform);
        float dist = Vector3.Distance(player.position, transform.position);
        if(dist<=8 && readyToFire==true)
        {
            StartCoroutine(Attack());
        }
    }

}
