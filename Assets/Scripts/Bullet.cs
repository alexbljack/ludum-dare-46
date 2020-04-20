using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        if (transform.position.x > 6) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
