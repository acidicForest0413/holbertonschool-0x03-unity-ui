using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    public Text scoreText, healthText, WinText;
    public Image WinLoseBG;
    private int score;
    [SerializeField] Rigidbody rb;
    [SerializeField] int StartHealth = 5;
    [SerializeField] private float respawnDelay = 3;
    Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        score = 0;
        health = StartHealth;
        SetScoreText();
        SetHealthText();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir = dir.normalized * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (dir * Time.fixedDeltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        HandlePickup(other);
        HandleTrap(other);
        HandleGoal(other);

    }

    private void HandlePickup(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
    }

    private void HandleTrap(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            if(health <= 0)
            {
                //Debug.Log("Game Over!");
                
                WinText.color = Color.white;
                WinText.text = "Game Over";
                WinLoseBG.color = Color.red;
                WinLoseBG.gameObject.SetActive(true);
                StartCoroutine(LoadScene(respawnDelay));
                //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                return;
            }
        }
    }

    private void HandleGoal(Collider other)
    {
        if (!other.gameObject.CompareTag("Goal"))
            return;
        //Debug.Log("You win!");
        WinText.color = Color.black;
        WinText.text = "You Win!";
        WinLoseBG.color = Color.green;
        WinLoseBG.gameObject.SetActive(true);
        StartCoroutine(routine: LoadScene(respawnDelay));
    }

    void SetScoreText()
    {
        if (scoreText == null)
        {
            //Debug.Log("Score: " + score);
            return;
        }
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        if(healthText == null)
        {
            //Debug.Log("Health: " + health);
        }
        healthText.text = $"Health: {health}";
    }

    void ShowWinScreen()
    {
        if (WinLoseBG == null)
            return;
        WinText.color = Color.black;
        WinText.text = "You Win!";
        WinLoseBG.color = Color.green;
        WinLoseBG.gameObject.SetActive(true);
    }

    void ShowLoseScreen()
    {
        if (WinLoseBG == null)
            return;
        WinText.color = Color.white;
        WinText.text = "Game Over";
        WinLoseBG.color = Color.red;
        WinLoseBG.gameObject.SetActive(true);
    }
    IEnumerator LoadScene(float seconds)
    {
        Debug.Log("Seconds: " + seconds);
        yield return new WaitForSeconds(seconds);
        Debug.Log("Ready to transition scenes");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}