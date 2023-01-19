using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sasori : MonoBehaviour
{
    public int index = 0;
    private float speed = 20f;
    private Rigidbody MidoriRigid;
    [SerializeField]
    private GameObject bullet;
    private float bulletRate;
    private float bulletTimer;
    void Awake()
    {
        MidoriRigid = GetComponent<Rigidbody>();
        MidoriRigid.velocity = transform.forward * -1 * speed;
    }


    void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > bulletRate)
        {
            FireBullet();
            bulletTimer = 0;
        }
        MidoriRigid.velocity = transform.forward * -1 * speed;
        if (transform.position.z < -6)
            transform.position = new Vector3(-6, 0, 8);
        if (transform.position.x > 6)
            transform.position += new Vector3(-12, 0, 0);
        else if (transform.position.x < -6)
            transform.position += new Vector3(12, 0, 0);
    }
    private void OnEnable()
    {
        bulletRate = Random.Range(1f, 1.7f);
        InvokeRepeating("Move", 0f, 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Move()
    {
        if (index % 2 == 0)
            transform.rotation = Quaternion.LookRotation(Vector3.left);
        else
            transform.rotation = Quaternion.Euler(0, 45, 0);

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
}
