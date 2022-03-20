using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalatorWallDetector : MonoBehaviour
{
    #region references
    [SerializeField] Collider2D _escalatorCollider;
    [SerializeField] LayerMask _wallLayer;
    #endregion
    #region parameters
    [SerializeField]
    private float _horizontalSizeAmplifier = 0.015f;
    [SerializeField]
    private float _verticalSizeReducer = 0.02f;
    #endregion 
    #region properties
    private Vector3 _sizeOffset;
    private Vector3 _verticalReducer;
    #endregion methods

    #region methods

    public bool WallAlert()
    {
        RaycastHit2D _boxCast = Physics2D.BoxCast(_escalatorCollider.bounds.center, _escalatorCollider.bounds.size + _sizeOffset - _verticalReducer, 0f, Vector2.zero, 0f, _wallLayer);
        return _boxCast.collider != null;
    }
    #endregion
    void Start()
    {
        _escalatorCollider = GetComponent<Collider2D>();
        _sizeOffset = Vector2.right * _horizontalSizeAmplifier;
        _verticalReducer = Vector2.up * _verticalSizeReducer;
    }
}
