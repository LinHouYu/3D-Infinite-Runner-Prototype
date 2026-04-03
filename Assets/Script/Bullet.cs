using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f; //子弹向前的速度

    void Update()
    {
        //子弹必须往前飞 
        transform.position += Vector3.forward * speed * Time.deltaTime;
        //飞出视野就销毁
        if (transform.position.z > 150f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); //销毁敌人
            Destroy(gameObject);       //销毁子弹自己
        }
        else if (other.CompareTag("Box"))
        {
            Destroy(gameObject);
        }
    }
}