using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speedMult = 1.4f;
    
    public float shootDelay = 2f;
    public GameObject bulletPrefab;

    [SerializeField]
    private GameObject deathPrefab;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (transform.position.x < -6) {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector2.left * Time.deltaTime * GameManager.runSpeed * speedMult);
    }

    public void Init() {
        speedMult = Random.Range(1f, 1.5f);
    }

    IEnumerator Shoot() {
        while (true) {
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.6f, 0.11f, 0), Quaternion.identity);
            yield return new WaitForSeconds(shootDelay);
        }
    }

    void Die() {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "pBullet") {
            Destroy(other.gameObject);
            Die();
        }
    }
}
