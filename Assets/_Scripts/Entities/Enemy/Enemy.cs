using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float _shootDelay = 2f;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _deathPrefab;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        while (true) {
            Vector3 offset = new Vector3(-0.6f, 0.11f);
            Instantiate(_bulletPrefab, transform.position + offset, Quaternion.identity);
            yield return new WaitForSeconds(_shootDelay);
        }
    }

    void Die() {
        Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "player_bullet") {
            Destroy(other.gameObject);
            Die();
        }
    }
}
