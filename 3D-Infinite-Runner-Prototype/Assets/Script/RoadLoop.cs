using UnityEngine;

public class RoadLoop : MonoBehaviour
{
    public float speed = 10f;       //移动速度
    public float roadLength = 50f;  //单块马路的长
    public int numberOfSegments = 3; //场景里总共有几块马路
    void Update()
    {
       
        transform.Translate(Vector3.back * (speed * Time.deltaTime));


        if (!(transform.position.z <= -roadLength)) return;
        Vector3 offset = new Vector3(0, 0, roadLength * numberOfSegments);
        transform.position += offset;
    }
}