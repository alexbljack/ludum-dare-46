using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private SpriteRenderer _rndr;
    private bool _canSpawn = false;
    private bool _playerIsOn = false;

    private bool _blocked = false;
    
    public GameObject _controlPrefab;

    void Start()
    {
        _rndr = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        if (transform.position.x < -10) {
            Destroy(gameObject);
        }
        
        transform.Translate(Vector2.left * GameManager.runSpeed * Time.deltaTime);
        
        if (Input.GetMouseButtonDown(0) && _canSpawn) {
            GameObject obj = Instantiate(_controlPrefab, transform.position + new Vector3(0.5f, 0), Quaternion.identity);
            obj.GetComponent<Control>().SetCommand(GameManager._currentCmd);
            obj.transform.parent = this.gameObject.transform;
            _blocked = true;
        }
    }

    void OnMouseEnter() {
        if (!_blocked && _rndr != null && gameObject.transform.childCount == 0 && !_playerIsOn) {
            _canSpawn = true;
            _rndr.color = new Color(0.8f, 0.8f, 1f); 
        }
    }

    void OnMouseExit() {
        _canSpawn = false;
        if (_rndr != null) {
            _rndr.color = new Color(255, 255, 255);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            _playerIsOn = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            _playerIsOn = false;
        }
    }
}
