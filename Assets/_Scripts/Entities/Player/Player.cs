using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{   
    [SerializeField]
    private GameObject _bulletPrefab;
    
    [SerializeField]
    private GameObject _noAmmoMsgPrefab;
    
    [SerializeField]
    private GameObject _deathPrefab;

    [SerializeField]
    private GameObject _pointerPrefab;
    
    [SerializeField]
    private float _initialSpeed = 1.5f;
    public static float runSpeed;

    public int lives = 1;
    public int bullets = 2;

    [SerializeField]
    private GameObject _canvas;

    private float _distance;
    private bool _canBeHit = true;

    private SpriteRenderer _rndr;
    private GUIManager _ui;

    void Start()
    {
        InitPointer();
        _distance = 0;
        runSpeed = _initialSpeed;
        _rndr = GetComponent<SpriteRenderer>();
        _ui = _canvas.GetComponent<GUIManager>();
    }

    void InitPointer() {
        Instantiate(_pointerPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        SpeedUp();
        UpdateUI();
    }

    void SpeedUp() {
        runSpeed += Time.deltaTime * 0.03f;
        _distance += runSpeed * Time.deltaTime * 5;
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
            Vector3 offset = new Vector3(0.66f, 0.11f, 0);
            Instantiate(_bulletPrefab, transform.position + offset, Quaternion.identity);
        } else {
            Vector3 offset = new Vector3(0.55f, 0.8f);
            GameObject msg = Instantiate(_noAmmoMsgPrefab, transform.position + offset, Quaternion.identity);
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
        if (lives == 0) {
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        StartCoroutine(BlinkWhileHit());
    }

    IEnumerator BlinkWhileHit() {
        _canBeHit = false;
        for (int i=0; i < 15; i++) {
            _rndr.enabled = !_rndr.enabled;
            yield return new WaitForSeconds(0.15f);
        }
        _rndr.enabled = true;
        _canBeHit = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "enemy_bullet") {
            if (_canBeHit) {
                Damage();
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "ammo") {
            bullets += 2;
            Destroy(other.gameObject);
        }

        if (other.tag == "control") {
            Control.Commands cmd = other.GetComponent<Control>().command;
            Vector3 target = transform.position;

            float bTop = 0.7f;
            float bBottom = -1.7f;
            float bLeft = -4.7f;
            float bRight = 4.7f;

            switch (cmd) {
                case Control.Commands.UP:
                    target += Vector3.up;
                    if (target.y < bTop) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case Control.Commands.DOWN:
                    target += Vector3.down;
                    if (target.y > bBottom) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case Control.Commands.FORWARD:
                    target += Vector3.right;
                    if (target.x < bRight) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case Control.Commands.BACK:
                    target += Vector3.left;
                    if (target.x > bLeft) {
                        StartCoroutine(Move(target));
                    }
                    break;
                case Control.Commands.FIRE:
                    Fire();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void UpdateUI() {
        _ui.SetBulletsText(bullets);
        _ui.SetLivesImg(lives);
        _ui.SetDistText(_distance);
    }
}
