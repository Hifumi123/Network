using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public float moveSpeed = 4;

    public float damage = 20;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if (isServer)
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider && isServer)
        {
            if (collider.tag == "Player")
                collider.GetComponent<Health>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
