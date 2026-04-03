using UnityEngine;

public class RoadLoop : MonoBehaviour
{
    public float speed = 10f;       
    public float roadLength = 50f;  
    public int numberOfSegments = 3; 
    
    private ObstacleSpawner spawner; //获取生成器

    void Start()
    {
        
        spawner = GetComponent<ObstacleSpawner>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= -roadLength)
        {
            Vector3 offset = new Vector3(0, 0, roadLength * numberOfSegments);
            transform.position += offset;

            
            if (spawner != null)
            {
                spawner.ResetAndSpawn();
            }
        }
    }
}