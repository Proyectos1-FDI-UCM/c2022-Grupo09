using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    #region references
    private CapsuleCollider2D _myCollider;
    [SerializeField]
    private LayerMask _wallLayer;
    #endregion

    #region parameters
    [SerializeField]
    private float _horizontalSizeAmplifier = 0.015f;
    #endregion

    #region properties
    private Vector3 _sizeOffset;
    #endregion methods

    public bool isInWall()
    {
        RaycastHit2D _boxCast = Physics2D.BoxCast(_myCollider.bounds.center + _sizeOffset / 2, _myCollider.bounds.size + _sizeOffset, 0f, Vector2.zero, 0f, _wallLayer);

        return _boxCast.collider != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<CapsuleCollider2D>();
        _sizeOffset = Vector2.right * _horizontalSizeAmplifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
