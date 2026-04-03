using UnityEngine;

public class AmmoMovement : MonoBehaviour
{
    public float speed = 10f; //补给包向后飘的速度

    void Update()
    {
        //补给包必须往后退
        transform.position -= Vector3.forward * speed * Time.deltaTime;

        //退到玩家身后就销毁
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}