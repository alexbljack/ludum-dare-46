using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -6) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.left * GameManager.runSpeed * 3f * Time.deltaTime);
    }
}
