using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemySpawner : MonoBehaviour
{
    public float maxDiff = 7 ;
    public float nextWaveTime;
    public float timeBeforeNextWave;
    public float difficultyLevel;
    public GameObject EnemyPrefab;
    public Vector2 minMaxSize;
    public Vector2 minMaxSpeed;
    Transform player;


    Enemy enemy; 

    
    void Start() 
    {
        difficultyLevel = 1;
        
    if (GameObject.FindGameObjectWithTag("GAMER") != null)
            player = GameObject.FindGameObjectWithTag("GAMER").transform;
    }

    void Update() 
    {
        

        if (Time.timeSinceLevelLoad >= nextWaveTime)
        {
            nextWaveTime += timeBeforeNextWave + difficultyLevel * 4f / 3f;
            CreateSpawnWave();
            float newDiffLevel = difficultyLevel + 0.5f;
            if (newDiffLevel >= maxDiff)
                difficultyLevel = maxDiff;
            else
                difficultyLevel = difficultyLevel + 0.5f;

        }
        
    }


    void CreateSpawnWave()
    {
        float difficultyLeft = difficultyLevel;

        int numberOfEnemiesToSpawn = (int)(Random.Range(1, difficultyLevel));
        Enemy[] toSpawn = new Enemy[numberOfEnemiesToSpawn];

        for (int i = 0; i < toSpawn.Length; i++) 
        {
            float enemyDiff;
            toSpawn[i] = new Enemy(1, 1, 1, 1);
            if (i == toSpawn.Length - 1)
                enemyDiff = difficultyLeft;
            else
            {
                enemyDiff = Random.Range(difficultyLevel / 3, difficultyLevel / 3);
                difficultyLeft -= enemyDiff;
            }

            toSpawn[i].setLevel((int)enemyDiff);


            //sets health
            if (enemyDiff > 2.5) 
            {
                if (Random.Range(0, 1) > 0.7f) 
                {
                    toSpawn[i].setHealth(2);
                    enemyDiff /= 2;
                    if (Random.Range(0, 1) > 0.5f) 
                    {
                        toSpawn[i].setHealth(3);
                        enemyDiff /= 1.5f;

                    }

                }

            }

            if (enemyDiff > 1.5)
            {
                if (Random.Range(0, 1) > 0.7)
                {
                    toSpawn[i].setHealth(2);
                    enemyDiff /= 2;
                }

            }

            //setsSize
            float newSize = Random.Range(Mathf.Max(minMaxSize.x, -Mathf.Sqrt(enemyDiff) + 1), Mathf.Min(minMaxSize.y, Mathf.Sqrt(enemyDiff) + 1));
            enemyDiff -= Mathf.Pow(newSize - 1, 2);
            toSpawn[i].setSize(newSize);

            //setsSpeed
            toSpawn[i].setSpeed(Mathf.Min(minMaxSpeed.x + Mathf.Pow(enemyDiff, 1f / 3f), minMaxSpeed.y));
        }

        spawnEnemies(toSpawn);
    }

    void spawnEnemies(Enemy[] enemiesToSpawn) 
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++) 

        {
            float spawnAngle=0;
            float spawnDistance = Random.Range(7, 15-minMaxSize.y);
           
            if (player != null) 
            {
                float x = player.position.x;
                float y = player.position.y;

                if(x >= 0 && y >= 0)
                    spawnAngle = Random.Range(Mathf.PI, (3f/2f)*Mathf.PI);
                if(x < 0 && y < 0)
                    spawnAngle = Random.Range(0, (1f / 2f) * Mathf.PI);
                if(x >= 0 && y < 0)
                    spawnAngle = Random.Range((1f / 2f) * Mathf.PI, Mathf.PI);
                if(x < 0 && y >= 0)
                    spawnAngle = Random.Range((3f / 2f) * Mathf.PI, 2 * Mathf.PI);
                
                Vector3 newPosition = new Vector3(Mathf.Cos(spawnAngle) * spawnDistance, 0.5f , Mathf.Sin(spawnAngle)*spawnDistance);

                GameObject newEnemy = Instantiate(EnemyPrefab, newPosition, Quaternion.identity);

                newEnemy.GetComponent<Enemy>().setSize(enemiesToSpawn[i].size);
                newEnemy.GetComponent<Enemy>().setSpeed(enemiesToSpawn[i].speed);
                newEnemy.GetComponent<Enemy>().setHealth(enemiesToSpawn[i].health);
                newEnemy.GetComponent<Enemy>().setLevel(enemiesToSpawn[i].level);

            }
        }
    }
}

