using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    public GameObject[] prefabs; //放入敌人、箱子、子弹包的预制体
    public float spawnInterval = 2f; //每2秒生成一排
    public float spawnZ = 50f; //在前方50米处生成
    private float[] lanes = { -2.5f, 0f, 2.5f };

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRow();
            timer = 0;
        }
    }

    void SpawnRow()
    {
        int enemyCount = 0;
        for (int i = 0; i < lanes.Length; i++)
        {
            int rand = Random.Range(0, 6); // 0-5 随机
            
            // 你的“三排禁令”逻辑
            if (rand == 1 && enemyCount >= 2) rand = 0; 

            Vector3 pos = new Vector3(lanes[i], 0, spawnZ);
            
            if (rand == 1) { //敌人
                pos.y = 1.0751f;
                Instantiate(prefabs[0], pos, Quaternion.identity);
                enemyCount++;
            } else if (rand == 2) { //箱子
                pos.y = 0.55f;
                Instantiate(prefabs[1], pos, Quaternion.identity);
            } else if (rand == 3) { //子弹包
                pos.y = 1.35f;
                Instantiate(prefabs[2], pos, Quaternion.identity);
            }
        }
    }
}