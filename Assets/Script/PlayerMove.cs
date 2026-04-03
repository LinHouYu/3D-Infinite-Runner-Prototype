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
    
    public int currentAmmo = 0;
    public GameObject bulletPrefab;
    public Transform firePoint;
    
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText; 
    
    [Range(0f, 1f)] public float bgmVolume = 0.2f; 
    private AudioSource audioSource;
    public AudioClip bgmMusic;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    public AudioClip noAmmoSound;
    public AudioClip collectAmmoSound;
    public AudioClip hitMonsterSound;
    public AudioClip deathSound;

    private float score = 0f;
    private bool isGameOver = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if (bgmMusic != null)
        {
            audioSource.clip = bgmMusic;
            audioSource.loop = true;  
            audioSource.volume = bgmVolume; 
            audioSource.Play();
        }

        UpdateAmmoUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return; 
        
        score += 10f * Time.deltaTime;
        if (scoreText != null) scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontalInput * sideSpeed * Time.deltaTime));

        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -xBoundary, xBoundary);
        transform.position = currentPosition;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            PlaySound(jumpSound);
        }
        
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J)))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            UpdateAmmoUI();
            PlaySound(shootSound);
        }
        else
        {
            PlaySound(noAmmoSound);
            Debug.Log("没有子弹了！");
        }
    }

    public void PlayHitMonsterSound()
    {
        PlaySound(hitMonsterSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("isGrounded")) 
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            currentAmmo += 3;
            Destroy(other.gameObject);
            UpdateAmmoUI();
            PlaySound(collectAmmoSound);
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Box"))
        {
            GameOver();
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null) ammoText.text = "Municion: " + currentAmmo;
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        audioSource.Stop(); 
        PlaySound(deathSound); 

        Time.timeScale = 0;
        
        if (finalScoreText != null) 
        {
            finalScoreText.text = "Final Score: " + Mathf.FloorToInt(score).ToString();
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}