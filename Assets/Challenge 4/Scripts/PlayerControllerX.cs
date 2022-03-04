using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float originalSpeed = 500;
    private float boostSpeedMultiplier = 2;
    private float speed;
    private GameObject focalPoint;

    private bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    [SerializeField] private ParticleSystem boostParticle;
    private bool speedBoostActive;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        speed = originalSpeed;

        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void FixedUpdate()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 
    }

    private void Update()
    {
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && !speedBoostActive)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    private IEnumerator SpeedBoost()
    {
        speed = originalSpeed * boostSpeedMultiplier;
        speedBoostActive = true;
        boostParticle.Play();

        yield return new WaitForSeconds(5f);

        speed = originalSpeed;
        boostParticle.Stop();

        yield return new WaitForSeconds(3f);

        speedBoostActive = false;
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup") && !hasPowerup)
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        hasPowerup = true;
        powerupIndicator.SetActive(true);

        yield return new WaitForSeconds(powerUpDuration);

        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
