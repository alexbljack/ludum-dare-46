using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{   
    [SerializeField]
    private GameObject backgroundPrefab;

    private GameObject _parentObj;
    private bool _created = false;
    private SpriteRenderer _rndr;

    void Start() {
        _rndr = GetComponent<SpriteRenderer>();
        _parentObj = GameObject.Find("Paralax");
    }

    void Update()
    {   
        if (!_created) {
            if (transform.position.x < SimpleMove.rightBound) {
                _created = true;
                float x = _rndr.sprite.bounds.size.x - 0.1f; // Just for a small overlap
                GameObject obj = Instantiate(backgroundPrefab, transform.position + new Vector3(x, 0, 0), Quaternion.identity);
                obj.transform.parent = _parentObj.transform;
            }
        }
        if (transform.position.x < SimpleMove.leftBound) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.left * Player.runSpeed * 0.5f * Time.deltaTime);
    }
}
