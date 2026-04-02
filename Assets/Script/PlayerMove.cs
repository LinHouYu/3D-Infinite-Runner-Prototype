using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float sideSpeed = 10f;    //左右躲闪的速度
    public float xBoundary = 4.5f;   //左右边界
    public float jumpForce = 5f;     //跳跃的力度
    public bool isGrounded;          

    private Rigidbody _rb;            

    void Start()
    {
       
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
   
        float horizontalInput = Input.GetAxis("Horizontal");
        

        transform.Translate(Vector3.right * (horizontalInput * sideSpeed * Time.deltaTime));


        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -xBoundary, xBoundary);
        transform.position = currentPosition;

       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            
            isGrounded = false;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("isGrounded"))
        {
            isGrounded = true;
        }
    }
}