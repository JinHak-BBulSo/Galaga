using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRigid;
    private float speed = 15f;
    private GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.up * speed;
    }

    private void OnEnable()
    {
        PlayerController.PlayerDie += IsPlayerDie;
        StartCoroutine(ReturnPool());
    }

    private void OnDisable()
    {
        PlayerController.PlayerDie -= IsPlayerDie;
    }

    IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            StopAllCoroutines();
            gameManager.GetComponent<GameManager>().GetScore();
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void IsPlayerDie()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
