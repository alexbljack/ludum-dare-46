using UnityEngine;

public class FontFix : MonoBehaviour
{
    [SerializeField]
    private Font _font;

    void Start()
    {
        _font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
