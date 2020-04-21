using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{   
    [SerializeField]
    private Text _bulletsTxt;

    [SerializeField]
    private Text _distanceText;

    [SerializeField]
    private Sprite[] _lifeImages;
    
    [SerializeField]
    private Image _livesImage;

    public void SetDistText(float distance) {
        _distanceText.text = (int)distance + " m";
    }

    public void SetBulletsText(int count) {
        _bulletsTxt.text = count.ToString();
    }

    public void SetLivesImg(int lives) {
        _livesImage.sprite = _lifeImages[lives];
    }
}

