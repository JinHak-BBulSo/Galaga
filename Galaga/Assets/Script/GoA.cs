using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GoA : MonoBehaviour
{
    public int index = 0;
    private float speed = 20f;
    private Rigidbody goaRigid;
    [SerializeField]
    private GameObject bullet;
    private float bulletRate;
    private float bulletTimer;

    void Awake()
    {
        goaRigid = GetComponent<Rigidbody>();
        goaRigid.velocity = transform.forward * -1 * speed;
    }

    
    void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > bulletRate)
        {
            FireBullet();
            bulletTimer = 0;
        }
        goaRigid.velocity = transform.forward * -1 * speed;
        if (transform.position.z < -6)
            transform.position += new Vector3(0, 0, 24);
    }

    private void OnEnable()
    {
        PlayerController.PlayerDie += IsPlayerDie;
        bulletRate = Random.Range(1f, 1.7f);
        InvokeRepeating("Move", 0f, 0.15f);
    }

    private void OnDisable()
    {
        PlayerController.PlayerDie -= IsPlayerDie;
        CancelInvoke();
    }

    void Move()
    {
        if (index % 7 < 3)
            transform.rotation = Quaternion.Euler(0, 15, 0);
        else if (index % 7 >= 4 && index % 7 < 7)
            transform.rotation = Quaternion.Euler(0, -15, 0);
        else
            transform.rotation = Quaternion.LookRotation(Vector3.zero);

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
