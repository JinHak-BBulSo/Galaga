using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    private int spawnIndex = 0;
    private int monsterCount = 0;
    private bool spawnPossible = true;
    void Start()
    {

    }

    void Update()
    {
        if(monsterCount == 5)
        {
            CancelInvoke();
            monsterCount = 0;
        }
        if (spawnPossible)
        {
            Spawn();
            spawnPossible = false;
            StartCoroutine(SpawnRate());
        }
    }

    void Spawn()
    {
        switch (spawnIndex)
        {
            case 0:
                InvokeRepeating("SpawnJaco", 0f, 0.3f);
                break;
            case 1:
                InvokeRepeating("SpawnMidori", 0f, 0.3f);
                break;
            case 2:
                InvokeRepeating("SpawnSasori", 0f, 0.3f);
                break;
            case 3:
                InvokeRepeating("SpawnGoA", 0f, 0.3f);
                break;
        }
        spawnIndex++;
        if (spawnIndex == 4)
            spawnIndex = 0;
    }

    void SpawnJaco()
    {
        int ran = Random.Range(-4, 4 + 1);
        ObjPool.jaco[monsterCount].transform.position = new Vector3(ran, 0, 9);
        ObjPool.jaco[monsterCount].SetActive(true);
        monsterCount++;
    }
    void SpawnMidori()
    {
        ObjPool.midori[monsterCount].transform.position = new Vector3(6, 0, 9);
        ObjPool.midori[monsterCount].SetActive(true);
        monsterCount++;
    }
    void SpawnSasori()
    {
        ObjPool.sasori[monsterCount].transform.position = new Vector3(-6, 0, 8);
        ObjPool.sasori[monsterCount].SetActive(true);
        monsterCount++;
    }
    void SpawnGoA()
    {
        int ran = Random.Range(-5, 5 + 1);
        ObjPool.goa[monsterCount].transform.position = new Vector3(ran, 0, 9);
        ObjPool.goa[monsterCount].SetActive(true);
        monsterCount++;
    }

    IEnumerator SpawnRate()
    {
        yield return new WaitForSeconds(10);
        spawnPossible = true;
    }
}
