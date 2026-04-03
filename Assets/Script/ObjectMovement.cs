using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed = 10f; 
    public float destroyZ = -10f; 

    void Update()
    {
        transform.Translate(Vector2.left * 0); 
        transform.position += Vector3.back * speed * Time.deltaTime;
        
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}