using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _baston;
    private Transform _bastonTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float _attackTime = 1f;
    #endregion

    #region properties
    private float _elapsedAttackTime;
    private float _defaultDirection;
    #endregion

    #region methods
    public void SetDefaultDirection(float dir)
    {
        _defaultDirection = dir;
    }
    // Recibe la direcci�n del ataque y lo activa en esa direcci�n
    public void Bastonazo(float horizontalAttackDirection, float verticalAttackDirection)
    {
        _attackTime = 0f;
        _bastonTransform.rotation = Quaternion.identity;
        // Si se ha escogido una direcci�n para el ataque
        if(horizontalAttackDirection != 0 && verticalAttackDirection != 0)
        {
            // a (-22'5�, 22'5�) Derecha
            if ((horizontalAttackDirection >= ((Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2)) &&
                (verticalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                (verticalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(Vector3.zero); _baston.SetActive(true); }
            // b (22'5�, 3*22'5�) Arriba derecha
            else if ((horizontalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 45)); _baston.SetActive(true); }
            // c (3*22'5�, 5*22'5�) Arriba
            else if ((horizontalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 90)); _baston.SetActive(true); }
            // d (5*22'5�, 7*22'5�) Arriba izquierda
            else if ((horizontalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 135)); _baston.SetActive(true); }
            // e (7*22'5�, -7*22'5�) Izquierda
            else if ((horizontalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 180)); _baston.SetActive(true); }
            // f (-7*22'5�, -5*22'5�) Abajo izquierda
            else if ((horizontalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 225)); _baston.SetActive(true); }
            // g (-5*22'5�, -3*22'5�) Abajo
            else if ((horizontalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 270)); _baston.SetActive(true); }
            // h (-3*22'5�, -22'5�) Abajo derecha
            else if ((horizontalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2) &&
                    (horizontalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))) / 2) &&
                    (verticalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2))) / 2)) { _bastonTransform.Rotate(new Vector3(0, 0, 315)); _baston.SetActive(true); }
        }
        // Si no se escoge direcci�n del ataque, el ataque es en la direcci�n del jugador
        else
        {
            if(_defaultDirection > 0) { _bastonTransform.Rotate(Vector3.zero); _baston.SetActive(true); }
            else { _bastonTransform.Rotate(new Vector3(0, 0, 180)); _baston.SetActive(true); }
        }
        Debug.Log(_bastonTransform.rotation);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _bastonTransform = _baston.transform;
        _baston.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Si el ataque ha empezado empieza a contar
        if (_baston.activeSelf) 
        { 
            _elapsedAttackTime += Time.deltaTime;
            // Cuando el ataque se haya completado desactiva el bast�n
            if (_elapsedAttackTime > _attackTime) _baston.SetActive(false);
        }
    }
}
