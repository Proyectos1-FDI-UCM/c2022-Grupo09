using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoJoseju : MonoBehaviour
{
    #region references
    private SpriteRenderer _mySprite;
    #endregion

    #region parameters
    [SerializeField] Color _blinkColor = Color.gray;
    #endregion

    #region properties
    private bool _blinking = true;
    private Color _baseColor;
    private float _baseLimit, _blinkLimit;
    private bool _toBase = false;
    #endregion

    #region methods
    public void SetBlinking(bool blinking)
    {
        _blinking = blinking;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mySprite = GetComponent<SpriteRenderer>();
        _baseColor = _mySprite.color;

        _blinkLimit = _blinkColor.r + ((_baseColor.r - _blinkColor.r) / 6);
        _baseLimit = _blinkColor.r + (((_baseColor.r - _blinkColor.r) / 6) * 5);
    }

    // Update is called once per frame
    void Update()
    {


        if (_blinking)
        {
            if (_mySprite.color.r >= _baseLimit) _toBase = false;
            else if (_mySprite.color.r <= _blinkLimit) _toBase = true;

            if (_toBase) _mySprite.color = Color.Lerp(_mySprite.color, _baseColor, 0.005f);
            else _mySprite.color = Color.Lerp(_mySprite.color, _blinkColor, 0.005f);

        }
        else _mySprite.color = _blinkColor;

    }
}
