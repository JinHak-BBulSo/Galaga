using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler PlayerDie;

    private float h;
    private Rigidbody playerRigid;
    private float speed = 7f;
    private int hp = 5;
    [SerializeField]
    private GameManager gameManager;
    public int Hp
    {
        get { return hp; }
        private set { hp = value; }
    }
    [SerializeField]
    private StageData stageData;

    private int bulletIndex = 0;
    bool isAttack = false;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (!isAttack && Input.GetKey(KeyCode.Space))
        {
            Attack();
            isAttack = true;
        }
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x), 0f, Mathf.Clamp(transform.position.z, stageData.LimitMin.z, stageData.LimitMax.z));
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");

        if (h > 0) playerRigid.velocity = Vector3.right * speed;
        else if (h < 0) playerRigid.velocity = Vector3.left * speed;
        else playerRigid.velocity = Vector3.zero;
    }
    void Attack()
    {
        if(bulletIndex < ObjPool.playerBulletPool.Length - 1)
        {
            GameObject bullet1;
            GameObject bullet2;

            bullet1 = ObjPool.playerBulletPool[bulletIndex];
            bullet1.SetActive(true);
            bullet1.transform.position = transform.position + new Vector3(-0.3f, 0, 0);
            bulletIndex++;

            bullet2 = ObjPool.playerBulletPool[bulletIndex];
            bullet2.SetActive(true);
            bullet2.transform.position = transform.position + new Vector3(0.3f, 0, 0);
            bulletIndex++;

            if (bulletIndex == 40) bulletIndex = 0;
        }
        StartCoroutine(AttackDelay());
    }

    public void Hit()
    {
        hp -= 1;
        if (hp == 0)
        {
            gameManager.GameOver();
            PlayerDie();
            gameObject.SetActive(false);
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
    }
}
