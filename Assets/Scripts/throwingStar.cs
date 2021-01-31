using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwingStar : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float starLifeTime = .5f;

    [SerializeField] string[] popLayersList;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        Destroy(gameObject, starLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string popLayer in popLayersList)
        {
            if (collision.tag == popLayer)
            {
                Destroy(gameObject);
            }

        }
    }
}
