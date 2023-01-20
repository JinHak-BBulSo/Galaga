using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaco : MonoBehaviour
{
    public int index = 0;
    private float speed = 20f;
    private Rigidbody JacoRigid;
    [SerializeField]
    private GameObject bullet;
    static int xLocate = 4;
    Vector3 locate;
    private float bulletRate;
    private float bulletTimer;
    void Awake()
    {
        JacoRigid = GetComponent<Rigidbody>();
        JacoRigid.velocity = transform.forward * -1 * speed;
    }


    void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > bulletRate)
        {
            FireBullet();
            bulletTimer = 0;
        }
        if(index < 36)
            JacoRigid.velocity = transform.forward * -1 * speed;
        else
        {
            
            transform.position = Vector3.MoveTowards(transform.position, locate, speed * Time.deltaTime);
        }
        if (transform.position == locate) transform.rotation = Quaternion.LookRotation(Vector3.zero);
    }
    private void OnEnable()
    {
        PlayerController.PlayerDie += IsPlayerDie;
        locate = new Vector3(xLocate, 0, 6);
        xLocate--;
        if (xLocate < -4) xLocate = 4;
        bulletRate = Random.Range(1f, 1.7f);
        transform.rotation = Quaternion.Euler(0, 15, 0);
        InvokeRepeating("Move", 0f, 0.05f);
    }

    private void OnDisable()
    {
        PlayerController.PlayerDie -= IsPlayerDie;
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
        index = 0;
        CancelInvoke();
    }

    void Move()
    {
        if (index > 5 &&  index < 30)
            transform.rotation *= Quaternion.Euler(0, 15, 0);
        else if (index > 35)
        {
            JacoRigid.velocity = Vector3.zero;
            CancelInvoke();
        }

        index++;
    }

    void FireBullet()
    {
        GameObject fireBullet = ObjPool.enemyBulletPool[ObjPool.enemyBulletIndex];
        fireBullet.SetActive(true);
        fireBullet.transform.position = transform.position;
        ObjPool.enemyBulletIndex++;
        if (ObjPool.enemyBulletIndex == ObjPool.enemyBulletPool.Length)
            ObjPool.enemyBulletIndex = 0;
    }
    void IsPlayerDie()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
