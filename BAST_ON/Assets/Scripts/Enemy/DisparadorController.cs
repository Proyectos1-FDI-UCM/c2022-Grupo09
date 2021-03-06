using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorController : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _myDisp, _myPlayer;
    private GameObject _mySoundArea;
    private Transform _myTransform;
    private CircleCollider2D _dispCollider;
    #endregion

    #region parameters
    [SerializeField]
    private float frecuencia = 6;
    [SerializeField] private float _shootingRange = 20;
    #endregion

    #region properties
    private float timer, ang;
    private Vector2 pos;
    private Vector2 _instanciatePoint;
    private float _startShooting;
    private float _startElapsedTime = 0;
    private float _originalFrequency;
    #endregion

    #region methods
    private void SlowDown(float slowDown)
    {
        frecuencia = _originalFrequency * slowDown;
    }
    #endregion


    void Start()
    {
        _myTransform = transform;
        _dispCollider = GetComponent<CircleCollider2D>();
        _startShooting = Random.Range(0, 300) / 100f;
        _originalFrequency = frecuencia;
    }

    void Update()
    {
        if (_startElapsedTime > _startShooting)
        {
            timer += Time.deltaTime;
            if (timer >= frecuencia)
            {
                pos = _myPlayer.transform.position - _myTransform.position;
                // Comprobaci�n para no disparar si el jugador est� lejos
                if (pos.magnitude < _shootingRange)
                {
                    pos.Normalize();
                    ang = Mathf.Acos(pos.x);
                    if (pos.y < 0) ang = -ang;
                    ang *= 180 / Mathf.PI;

                    _instanciatePoint = (Vector2)_myTransform.position + _dispCollider.offset + (pos * (_dispCollider.radius + 0.5f));

                    Instantiate(_myDisp, _instanciatePoint, Quaternion.Euler(0, 0, ang));
                    timer = 0;
                }
            }
        }
        else _startElapsedTime += Time.deltaTime;

        // Comprobación de si hay un Kiwi Activo
        if (GameManager.Instance.GetKiwiActive()) SlowDown(GameManager.Instance.GetKiwiSlowDown());
        else frecuencia = _originalFrequency;
    }
}
