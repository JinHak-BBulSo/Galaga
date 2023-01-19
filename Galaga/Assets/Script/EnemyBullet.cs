using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody bulletRigid;
    private float speed = 15f;

    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.velocity = transform.up * -1 * speed;
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnPool());
    }

    IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Hit();
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}
