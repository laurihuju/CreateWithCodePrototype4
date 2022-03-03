using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 lookDirection = (PlayerController.GetInstance().transform.position - transform.position).normalized;
        rb.AddForce(lookDirection * speed);
    }
}
