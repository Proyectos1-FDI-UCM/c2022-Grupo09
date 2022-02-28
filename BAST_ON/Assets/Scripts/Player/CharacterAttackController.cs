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
    #endregion

    #region methods
    // Recibe la dirección del ataque y lo activa en esa dirección
    public void Bastonazo(int horizontalAttackDirection, int verticalAttackDirection)
    {
        _attackTime = 0f;
        _bastonTransform.rotation = Quaternion.identity;
        // a (-22'5º, 22'5º) Derecha
        if ((horizontalAttackDirection >= (Mathf.Sqrt(2 + Mathf.Sqrt(2)))) && 
            (verticalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) && 
            (verticalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))))) { _bastonTransform.Rotate(Vector3.zero); _baston.SetActive(true); }
        // b (22'5º, 3*22'5º) Arriba derecha
        else if ((horizontalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 45)); _baston.SetActive(true); }
        // c (3*22'5º, 5*22'5º) Arrib
        else if ((horizontalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 90)); _baston.SetActive(true); }
        // d (5*22'5º, 7*22'5º) Arriba izquierda
        else if ((horizontalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 135)); _baston.SetActive(true); }
        // e (7*22'5º, -7*22'5º) Izquierda
        else if ((horizontalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 180)); _baston.SetActive(true); }
        // f (-7*22'5º, -5*22'5º) Abajo izquierda
        else if ((horizontalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 225)); _baston.SetActive(true); }
        // g (-5*22'5º, -3*22'5º) Abajo
        else if ((horizontalAttackDirection >= -(Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection < (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < -(Mathf.Sqrt(2 + Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 270)); _baston.SetActive(true); }
        // h (-3*22'5º, -22'5º) Abajo derecha
        else if ((horizontalAttackDirection >= (Mathf.Sqrt(2 - Mathf.Sqrt(2)))) &&
                (horizontalAttackDirection < (Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (verticalAttackDirection >= -(Mathf.Sqrt(2 + Mathf.Sqrt(2)))) &&
                (verticalAttackDirection < -(Mathf.Sqrt(2 - Mathf.Sqrt(2))))) { _bastonTransform.Rotate(new Vector3(0, 0, 315)); _baston.SetActive(true); }
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
        if (_baston.activeSelf) _elapsedAttackTime += Time.deltaTime;
        // Cuando el ataque se haya completado desactiva el bastón
        if (_elapsedAttackTime > _attackTime) _baston.SetActive(false);
    }
}
