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
    [SerializeField]
    private float _verticalSizeReducer = 0.02f;
    #endregion

    #region properties
    private Vector3 _sizeOffset;
    private Vector3 _verticalReducer;
    #endregion methods

    public bool isInWall()
    {
        RaycastHit2D _boxCast = Physics2D.BoxCast(_myCollider.bounds.center + _sizeOffset / 2, _myCollider.bounds.size + _sizeOffset - _verticalReducer, 0f, Vector2.zero, 0f, _wallLayer);
        /*
        Debug.DrawRay(_myCollider.bounds.center + new Vector3(_myCollider.bounds.extents.x  + _sizeOffset.x, 0), Vector2.down * (_myCollider.bounds.extents.y  - _verticalSizeReducer));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x + _sizeOffset.x, 0), Vector2.down * (_myCollider.bounds.extents.y - _verticalSizeReducer));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x, _myCollider.bounds.extents.y - _verticalSizeReducer), Vector2.right * 2 * (_myCollider.bounds.extents.x));
        */
        return _boxCast.collider != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<CapsuleCollider2D>();
        _sizeOffset = Vector2.right * _horizontalSizeAmplifier;
        _verticalReducer = Vector2.up * _verticalSizeReducer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
