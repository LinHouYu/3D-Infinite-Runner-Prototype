using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float sideSpeed = 10f;    //左右躲闪的速度
    public float xBoundary = 4.5f;   //左右边界
    public float jumpforce = 5f;
    public bool isGrounded;

    void Update()
    {
        //获取玩家的左右按键输入
        var horizontalInput = Input.GetAxis("Horizontal");
        Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.right * (horizontalInput * sideSpeed * Time.deltaTime));

        
        var currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -xBoundary, xBoundary);
        transform.position = currentPosition;

        if (!Input.GetButtonDown("Jump")) return;
        transform.Translate(Vector3.up * (jumpforce * Time.deltaTime));











    }
}