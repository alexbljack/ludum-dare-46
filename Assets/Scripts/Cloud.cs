using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -6) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.left * GameManager.runSpeed * 0.7f * Time.deltaTime);
    }
}
