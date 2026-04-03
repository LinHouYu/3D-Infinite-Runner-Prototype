using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("预制体设置")]
    public GameObject enemyPrefab;
    public GameObject boxPrefab;
    public GameObject ammoPrefab;

    private float[] lanes = { -2.5f, 0f, 2.5f }; //三个X轴赛道
    
    private float[] zPositions = { 15f, 35f }; 

    void Start()
    {
        SpawnObstacles(); 
    }
    
    public void ResetAndSpawn()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemy") || child.CompareTag("Box") || child.CompareTag("Ammo"))
            {
                Destroy(child.gameObject);
            }
        }

        //重新生成
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        foreach (float localZ in zPositions)
        {
            int enemyCount = 0; //每一排的敌人独立计数

            for (int i = 0; i < lanes.Length; i++)
            {
                int randomChoice = Random.Range(0, 5); 

                if (randomChoice == 1 && enemyCount >= 2)
                {
                    randomChoice = Random.Range(2, 5); //依然保持禁止3个敌人的完美机制
                }
                
                Vector3 spawnPos = new Vector3(lanes[i], 0, localZ);

                switch (randomChoice)
                {
                    case 1:
                        spawnPos.y = 1.0751f;
                        Instantiate(enemyPrefab, transform.position + spawnPos, Quaternion.identity, transform);
                        enemyCount++;
                        break;
                    case 2:
                        spawnPos.y = 0.5500003f;
                        Instantiate(boxPrefab, transform.position + spawnPos, Quaternion.identity, transform);
                        break;
                    case 3:
                        spawnPos.y = 1.35f;
                        Instantiate(ammoPrefab, transform.position + spawnPos, Quaternion.Euler(90f, 0f, 0f), transform);
                        break;
                }
            }
        }
    }
}