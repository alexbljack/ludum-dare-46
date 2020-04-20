using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject bgPrefab;
    private bool _created = false;
    private SpriteRenderer _rndr;

    void Start() {
        _rndr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        if (!_created) {
            if (transform.position.x < 6f) {
                _created = true;
                float x = _rndr.sprite.bounds.size.x - 0.1f; // Just for a small overlap
                Instantiate(bgPrefab, transform.position + new Vector3(x, 0, 0), Quaternion.identity);
            }
        }
        if (transform.position.x < -6) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.left * GameManager.runSpeed * 0.5f * Time.deltaTime);
    }
}
