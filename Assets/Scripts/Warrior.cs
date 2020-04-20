using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Warrior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject noAmmoMsgPrefab;
    public GameObject deathPrefab;

    public int lives = 1;
    public int bullets = 2;

    private float distance = 0;
    private bool canBeHit = true;
    private SpriteRenderer _rndr;

    private GUIManager _ui;

    void Start()
    {
        _rndr = GetComponent<SpriteRenderer>();
        _ui = GameObject.Find("Canvas").GetComponent<GUIManager>();
        _ui.SetBulletsText(bullets);
        _ui.SetLivesImg(lives);
        _ui.SetDistText(distance);
    }

    void Update()
    {
        distance += GameManager.runSpeed * Time.deltaTime * 5;
        _ui.SetDistText(distance);
    }

    public IEnumerator Move(Vector2 target, float moveSpeed=2f) {
        float timeLerped = 0.0f;
        float startTime = Time.time;
        Vector2 source = transform.position;
        float distance = Vector2.Distance(transform.position, target); 

        while(Vector2.Distance(transform.position, target) > 0.05f)
        {
            timeLerped += Time.deltaTime;
            float t = (timeLerped * moveSpeed) / distance;
            transform.position = Vector2.Lerp(transform.position, target, t);
            yield return null;
        }
        transform.position = target;
    }

    void Fire() {
        if (bullets > 0) {
            bullets--;
            _ui.SetBulletsText(bullets);
            Instantiate(bulletPrefab, transform.position + new Vector3(0.66f, 0.11f, 0), Quaternion.identity);
        } else {
            GameObject msg = Instantiate(noAmmoMsgPrefab, transform.position + new Vector3(0.55f, 0.8f), Quaternion.identity);
            msg.transform.parent = transform;
            StartCoroutine(HideNoAmmoMessage(msg));
        }
    }

    IEnumerator HideNoAmmoMessage(GameObject msg) {
        yield return new WaitForSeconds(1);
        Destroy(msg);
    }

    void Damage() {
        lives--;
        _ui.SetLivesImg(lives);
        if (lives == 0) {
            Instantiate(deathPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        StartCoroutine(BlinkWhileHit());
    }

    IEnumerator BlinkWhileHit() {
        canBeHit = false;
        for (int i=0; i < 15; i++) {
            _rndr.enabled = !_rndr.enabled;
            yield return new WaitForSeconds(0.15f);
        }
        _rndr.enabled = true;
        canBeHit = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "eBullet") {
            if (canBeHit) {
                Damage();
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "ammo") {
            bullets += 2;
            Destroy(other.gameObject);
            _ui.SetBulletsText(bullets);
        }

        if (other.tag == "Control") {
            string cmd = other.GetComponent<Control>().command;
            Vector3 target;

            switch (cmd) {
                case "UP":
                    target = transform.position + Vector3.up;
                    if (target.y < 0.7) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case "DOWN":
                    target = transform.position + Vector3.down;
                    if (target.y > -1.7) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case "FORWARD":
                    target = transform.position + Vector3.right;
                    if (target.x < 4.7) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case "BACK":
                    target = transform.position + Vector3.left;
                    if (target.x > -4.7) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case "FIRE":
                    Fire();
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
