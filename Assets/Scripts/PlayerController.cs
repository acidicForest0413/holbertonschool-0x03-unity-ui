using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    private int score;
    [SerializeField] Rigidbody rb;
    [SerializeField] int StartHealth = 5;
    Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        score = 0;
        health = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir = dir.normalized * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + dir * Time.fixedDeltaTime);
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
            Debug.Log("Score: " + score);
        }
    }

    private void HandleTrap(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            if(--health <= 0)
            {
                Debug.Log("Game Over!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                return;
            }
            Debug.Log("Health: " + health);
        }
    }

    private void HandleGoal(Collider other)
    {
        if (!other.gameObject.CompareTag("Goal"))
            return;
        Debug.Log("You win!");
    }
}
