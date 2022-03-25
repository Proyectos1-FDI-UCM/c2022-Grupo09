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

    /// <summary>
    /// Método que detecta si el jugador está en una pared. Devuelve 1 si la pared está a la derecha y -1 si está a la izquierda. En caso de no estar e una pared devuelve 0.
    /// </summary>
    /// <returns></returns>
    public int isInWall()
    {
        //Bounds Cordenadas xyz de la caja del collider
        RaycastHit2D _leftBoxCast = Physics2D.BoxCast(_myCollider.bounds.center - (_myCollider.bounds.size / 4), (_myCollider.bounds.size / 2) + _sizeOffset, 0f, Vector2.zero, 0f, _wallLayer);
        RaycastHit2D _rightBoxCast = Physics2D.BoxCast(_myCollider.bounds.center + (_myCollider.bounds.size / 4), (_myCollider.bounds.size / 2) + _sizeOffset, 0f, Vector2.zero, 0f, _wallLayer);
        /*
        Debug.DrawRay(_myCollider.bounds.center + new Vector3(_myCollider.bounds.extents.x  + _sizeOffset.x, 0), Vector2.down * (_myCollider.bounds.extents.y  - _verticalSizeReducer));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x + _sizeOffset.x, 0), Vector2.down * (_myCollider.bounds.extents.y - _verticalSizeReducer));
        Debug.DrawRay(_myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x, _myCollider.bounds.extents.y - _verticalSizeReducer), Vector2.right * 2 * (_myCollider.bounds.extents.x));
        */
        if (_rightBoxCast) return 1;
        else if (_leftBoxCast) return -1;
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<CapsuleCollider2D>();
        _sizeOffset = Vector2.right * _horizontalSizeAmplifier - Vector2.up * _verticalSizeReducer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
