using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBullet;
    public static GameObject[] playerBulletPool =
        new GameObject[40];

    [SerializeField]
    private GameObject enemyBullet;
    public static int enemyBulletIndex = 0;
    public static GameObject[] enemyBulletPool =
        new GameObject[100];

    [SerializeField]
    private GameObject[] enemyPrefab = new GameObject[4];

    public static GameObject[] jaco = new GameObject[5];
    public static GameObject[] midori = new GameObject[5];
    public static GameObject[] sasori = new GameObject[5];
    public static GameObject[] goa = new GameObject[5];

    void Start()
    {
        for(int i = 0; i < playerBulletPool.Length; i++)
        {
            playerBulletPool[i] = Instantiate(playerBullet);
            playerBulletPool[i].transform.parent = gameObject.transform;
            playerBulletPool[i].SetActive(false);
        }

        for(int i = 0; i < enemyBulletPool.Length; i++)
        {
            enemyBulletPool[i] = Instantiate(enemyBullet);
            enemyBulletPool[i].transform.parent = gameObject.transform;
            enemyBulletPool[i].SetActive(false);
        }
        for(int i = 0; i < 5; i++)
        {
            jaco[i] = Instantiate(enemyPrefab[0]);
            jaco[i].transform.parent = gameObject.transform;
            jaco[i].SetActive(false);

            midori[i] = Instantiate(enemyPrefab[1]);
            midori[i].transform.parent = gameObject.transform;
            midori[i].SetActive(false);

            sasori[i] = Instantiate(enemyPrefab[2]);
            sasori[i].transform.parent = gameObject.transform;
            sasori[i].SetActive(false);

            goa[i] = Instantiate(enemyPrefab[3]);
            goa[i].transform.parent = gameObject.transform;
            goa[i].SetActive(false);
        }
    }
}
