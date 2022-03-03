using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float destroyY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 lookDirection = (PlayerController.GetInstance().transform.position - transform.position).normalized;
        rb.AddForce(lookDirection * speed);
    }

    private void Update()
    {
        if(transform.position.y < destroyY)
        {
            Destroy(this.gameObject);
            SpawnManager.GetInstance().EnemyDeath();
        }
    }
}
