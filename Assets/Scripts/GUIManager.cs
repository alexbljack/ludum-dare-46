using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Sprite[] lifeImages;
    private Warrior _player;
    public Text bulletsTxt;
    public Image livesImg;
    public Text distText;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Warrior>();
    }

    public void SetDistText(float distance) {
        distText.text = (int)distance + " m";
    }

    public void SetBulletsText(int count) {
        bulletsTxt.text = count.ToString();
    }

    public void SetLivesImg(int lives) {
        livesImg.sprite = lifeImages[lives];
    }

    void Update()
    {
        
    }
}

