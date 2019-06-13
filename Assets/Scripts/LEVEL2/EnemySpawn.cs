using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject Type;
        public int price;
    }


    public EnemyType[] Enemies;
    public int StarterCapital = 5;
    public float X1;
    public float X2;
    public float Y1;
    public float Y2;

    private int capitalForNow = 100;
    private int stageCount = 0;
    private bool isWorking = false;


    void Update()
    {
        EnemyFire[] enemy = FindObjectsOfType<EnemyFire>();
        if (enemy.Length == 0 && isWorking == false)
        {
            StartCoroutine(NewWave());
            isWorking = true;
        }
    }

    IEnumerator NewWave()
    {
        stageCount++;
        StarterCapital += Random.Range(1, 5);

        yield return new WaitForSeconds(3);
        capitalForNow = StarterCapital;

        StartCoroutine(NewStage());
    }
        IEnumerator NewStage()
        {
            List<EnemyType> awailableEnemies = new List<EnemyType>();

            foreach (var en in Enemies)
            {
                if (capitalForNow >= en.price)
                {
                    awailableEnemies.Add(en);
                }
            }

            Vector3 pos = new Vector3(Random.Range(X1, X2), Random.Range(Y1, Y2), 0);
            int whichOne = Random.Range(0, awailableEnemies.Count);

            Instantiate(awailableEnemies[whichOne].Type, pos, Quaternion.identity);

            capitalForNow -= awailableEnemies[whichOne].price;

        yield return new WaitForSeconds(Random.Range(1, (awailableEnemies[whichOne].price-(awailableEnemies[whichOne].price / 100 * 20)) / stageCount));
            if (capitalForNow >= Enemies[0].price)
                StartCoroutine(NewStage());
            else
                isWorking = false;
    }
}