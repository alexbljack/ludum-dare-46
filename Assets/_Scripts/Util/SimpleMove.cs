using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    private enum Direction {
        Left,
        Right
    }

    [SerializeField]
    private Direction _direction = Direction.Right;
    
    [SerializeField]
    private float _speedMult = 1f;

    [SerializeField]
    private float _speedUpperBound = -1;

    public static float rightBound = 10;
    public static float leftBound = -10;

    void Start() {
        if (_speedUpperBound > 0) {
            _speedMult = Random.Range(_speedMult, _speedUpperBound);
        }
    }

    void Update()
    {
        float x = transform.position.x;
        float baseSpeed = _speedMult * Time.deltaTime;

        if (x < leftBound || x > rightBound) {
            Destroy(this.gameObject);
        }

        switch (_direction) {
            case Direction.Right:
                transform.Translate(Vector2.right * baseSpeed);
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * Player.runSpeed * baseSpeed);
                break;
        }
    }
}
