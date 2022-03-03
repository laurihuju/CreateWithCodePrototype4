using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private GameObject powerupIndicator;

    [SerializeField] private float speed;
    [SerializeField] private float powerupStrength;

    private bool hasPowerup = false;

    private static PlayerController instance;

    private void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        powerupIndicator.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !hasPowerup)
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(Vector3.Normalize(collision.transform.position - transform.position) * powerupStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        hasPowerup = true;
        powerupIndicator.SetActive(true);

        yield return new WaitForSeconds(7);

        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    public static PlayerController GetInstance()
    {
        return instance;
    }
}
