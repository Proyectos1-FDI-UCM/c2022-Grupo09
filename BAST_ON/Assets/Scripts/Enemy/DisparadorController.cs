using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myDisp, _myPlayer,_mySoundArea;
    private Transform _myTransform;
    private CircleCollider2D _dispCollider;
    #endregion

    #region parameters
    [SerializeField]
    private float frecuencia = 6;
    #endregion

    #region properties
    private float timer, ang;
    private Vector2 pos;
    private Vector2 _instanciatePoint;
    private float _startShooting;
    private float _startElapsedTime = 0;
    #endregion


    void Start()
    {
        _myTransform = transform;
        _dispCollider = GetComponent<CircleCollider2D>();
        _startShooting = Random.Range(0, 300) / 100f;

    }

    void Update()
    {
        if (_startElapsedTime > _startShooting)
        {
            timer += Time.deltaTime;
            if (timer >= frecuencia)
            {
                pos = _myPlayer.transform.position - _myTransform.position;
                pos.Normalize();
                ang = Mathf.Acos(pos.x);
                if (pos.y < 0) ang = -ang;
                ang *= 180 / Mathf.PI;

                _instanciatePoint = (Vector2)_myTransform.position + _dispCollider.offset + (pos * (_dispCollider.radius + 0.5f));

                Instantiate(_myDisp, _instanciatePoint, Quaternion.Euler(0, 0, ang));
                timer = 0;
            }
        }
        else _startElapsedTime += Time.deltaTime;
    }
}
