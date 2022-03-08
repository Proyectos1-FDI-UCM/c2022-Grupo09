using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myDisp, _myPlayer;
    private Transform _myTransform;
    private CircleCollider2D _collider;
    #endregion

    #region parameters
    [SerializeField]
    private float frecuencia = 6;
    #endregion

    #region properties
    private float timer, ang;
    private Vector2 pos;
    private Vector2 _instanciatePoint;
    #endregion


    void Start()
    {
        _myTransform = transform;
        _collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frecuencia)
        {
            pos = _myPlayer.transform.position - _myTransform.position;
            pos.Normalize();
            ang = Mathf.Acos(pos.x);
            if (pos.y < 0) ang = -ang;
            ang *= 180 / Mathf.PI;
            // LIMPIAR ESTO
            _instanciatePoint = _myTransform.position;
            if (pos.x > 0) _instanciatePoint.x += _collider.offset.x + _collider.radius + 0.2f;
            else _instanciatePoint.x += _collider.offset.x - (_collider.radius + 0.2f);
            if (pos.y > 0) _instanciatePoint.x += _collider.offset.x + _collider.radius + 0.2f;
            else _instanciatePoint.y += _collider.offset.y - (_collider.radius + 0.2f);

            Instantiate(_myDisp, _instanciatePoint, Quaternion.Euler(0, 0, ang));
            timer = 0;
        }
            
    }
}
