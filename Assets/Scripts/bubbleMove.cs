using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleMove : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Camera Limits" && rb.velocity == Vector2.zero)
        {
            //rb.velocity = transform.right * (Random.Range((speed * -1), speed));
            //rb.velocity = transform.up * (Random.Range((speed * -1), speed));
            rb.velocity = new Vector2(Random.Range((speed * -1), speed), Random.Range((speed * -1), speed));
        }
    }
}
