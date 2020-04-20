using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour
{
    public string command;

    private SpriteRenderer _rndr;

    public Sprite upCtrl;
    public Sprite downCtrl;
    public Sprite backCtrl;
    public Sprite forwardCtrl;
    public Sprite fireCtrl;

    public void SetCommand(string cmd) {
        _rndr = GetComponent<SpriteRenderer>();
        command = cmd;
        _rndr.sprite = GetSprite();
    }

    Sprite GetSprite() {
        switch (command) {
            case "UP":
            return upCtrl;
            case "DOWN":
            return downCtrl;
            case "BACK":
            return backCtrl;
            case "FORWARD":
            return forwardCtrl;
            case "FIRE":
            return fireCtrl;
        }
        return null;
    }

    void Update()
    {
        
    }
}
