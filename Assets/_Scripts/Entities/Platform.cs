using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject _controlPrefab;

    private bool _canSpawn = false;
    private bool _playerIsOn = false;
    private bool _blocked = false;

    private SpriteRenderer _rndr;
    private Pointer _pointer;
    private GameObject _parentObj;

    void Start()
    {
        _rndr = GetComponent<SpriteRenderer>();
        _pointer = GameObject.FindGameObjectWithTag("pointer").GetComponent<Pointer>();
        _parentObj = GameObject.Find("DynamicFloor");
        this.transform.parent = _parentObj.transform;
    }

    void Update()
    {   
        if (Input.GetMouseButtonDown(0) && _canSpawn) {
            GameObject obj = Instantiate(_controlPrefab, transform.position + new Vector3(0.5f, 0), Quaternion.identity);
            obj.GetComponent<Control>().Init(_pointer.GetCurrentCommand());
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
