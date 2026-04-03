using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class PlayerMove : MonoBehaviour
{
 
    public float sideSpeed = 10f;
    public float xBoundary = 4.5f;
    public float jumpForce = 7f;
    public bool isGrounded;
    private Rigidbody _rb;
    
 
    public int currentAmmo = 0;          //当前拥有的子弹数
    public GameObject bulletPrefab;      //子弹预制体
    public Transform firePoint;          //枪口的位置
    
 
    public TextMeshProUGUI ammoText;       //子弹显示的文本
    public TextMeshProUGUI scoreText;      //得分显示的文本
    public GameObject gameOverPanel;       //游戏结束面板

    private float score = 0f;              //当前得分
    private bool isGameOver = false;       //判断游戏是否结束

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        // 游戏刚开始时，刷新一下UI界面
        UpdateAmmoUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false); //确保一开始结束面板是隐藏的
    }

    void Update()
    {
        if (isGameOver) return; 
        
        score += 10f * Time.deltaTime; //随时间自动加分
        if (scoreText != null) scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontalInput * sideSpeed * Time.deltaTime));

        //限制边界
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -xBoundary, xBoundary);
        transform.position = currentPosition;

        //跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J)))
        {
            Shoot();
        }
    }

    //开火方法
    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--; //消耗一发子弹
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // 生成子弹
            
            UpdateAmmoUI(); //开火后立刻刷新UI数字
        }
        else
        {
            Debug.Log("没有子弹了！只能跳过去！");
        }
    }

    //落地检测
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isGrounded")) 
        {
            isGrounded = true;
        }
    }

    //碰撞与拾取逻辑
    private void OnTriggerEnter(Collider other)
    {
        //如果碰到了弹药包
        if (other.CompareTag("Ammo"))
        {
            currentAmmo += 3; //捡到一个弹药包
            Destroy(other.gameObject); //吃掉弹药包
            
            UpdateAmmoUI(); //捡到子弹后立刻刷新UI数字
        }
        //如果撞到了敌人或箱子
        else if (other.CompareTag("Enemy") || other.CompareTag("Box"))
        {
            GameOver(); //发游戏结束方法
        }
    }

    // 刷新子弹UI显示
    void UpdateAmmoUI()
    {
        if (ammoText != null) ammoText.text = "Municion: " + currentAmmo;
    }

    //游戏结束逻辑
    void GameOver()
    {
        isGameOver = true;          
        Time.timeScale = 0;        
        
        if (gameOverPanel != null) 
            gameOverPanel.SetActive(true); 
    }

    //重新开始游戏
    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}