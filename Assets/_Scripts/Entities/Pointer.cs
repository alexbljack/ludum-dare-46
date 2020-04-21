using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private Sprite upCursor;
    
    [SerializeField]
    private Sprite downCursor;
    
    [SerializeField]
    private Sprite backCursor;
    
    [SerializeField]
    private Sprite forwardCursor;
    
    [SerializeField]
    private Sprite fireCursor;

    private Control.Commands _command;
    private SpriteRenderer _rndr;

    void Start()
    {
        Cursor.visible = false;
        _rndr = GetComponent<SpriteRenderer>();
        Init();
    }

    void Init() {
        _command = Control.Commands.FORWARD;
        _rndr.sprite = forwardCursor;
    }

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);
        ProcessInput();
    }

    public Control.Commands GetCurrentCommand() {
        return _command;
    }

    void ProcessInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            _command = Control.Commands.UP;
            _rndr.sprite = upCursor;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            _command = Control.Commands.DOWN;
            _rndr.sprite = downCursor;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            _command = Control.Commands.BACK;
            _rndr.sprite = backCursor;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            _command = Control.Commands.FORWARD;
            _rndr.sprite = forwardCursor;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            _command = Control.Commands.FIRE;
            _rndr.sprite = fireCursor;
        }
    }

}
