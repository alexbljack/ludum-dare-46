using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour
{
    public enum Commands {
        UP,
        DOWN,
        FORWARD,
        BACK,
        FIRE
    }

    [HideInInspector]
    public Commands command = Commands.UP;

    [SerializeField]
    private Sprite _upCtrl;
    
    [SerializeField]
    private Sprite _downCtrl;
    
    [SerializeField]
    private Sprite _backCtrl;
    
    [SerializeField]
    private Sprite _forwardCtrl;
    
    [SerializeField]
    private Sprite _fireCtrl;

    private SpriteRenderer _rndr;

    public void Init(Commands cmd) {
        _rndr = GetComponent<SpriteRenderer>();
        command = cmd;
        switch (command) {
            case Commands.UP:
                _rndr.sprite = _upCtrl;
                break;
            case Commands.DOWN:
                _rndr.sprite = _downCtrl;
                break;
            case Commands.BACK:
                _rndr.sprite = _backCtrl;
                break;
            case Commands.FORWARD:
                _rndr.sprite = _forwardCtrl;
                break;
            case Commands.FIRE:
                _rndr.sprite = _fireCtrl;
                break;
        }
    }
}
