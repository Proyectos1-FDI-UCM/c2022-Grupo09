using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    #region references
    private CapsuleCollider2D _myCollider;
    [SerializeField]
    private LayerMask _floorLayer;
    #endregion
    
    #region methods
    public bool IsGrounded()
    {
        float _floorDetectorOffset = 0.1f;
        RaycastHit2D _boxCast = Physics2D.BoxCast(_myCollider.bounds.center, _myCollider.bounds.size, 0f, Vector2.down, _floorDetectorOffset, _floorLayer);
        /*
        if (_boxCast.collider != null) Debug.Log(_boxCast.collider);
        Debug.DrawRay(_myCollider.bounds.center + new Vector3(_myCollider.bounds.extents.x, 0), Vector2.down * (_myCollider.bounds.extents.y + _floorDetectorOffset));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x, 0), Vector2.down * (_myCollider.bounds.extents.y + _floorDetectorOffset));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x, _myCollider.bounds.extents.y + _floorDetectorOffset), Vector2.right * 2*(_myCollider.bounds.extents.x));
        */
        return _boxCast.collider != null;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
