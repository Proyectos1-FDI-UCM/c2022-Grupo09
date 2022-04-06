using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGearController : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myGear;
    private Transform _gearTransform;
    #endregion

    #region parameters
    [SerializeField]
    private float _frequency = 2.0f;

    [SerializeField]
    private float _rangoGeneracion = 15.0f;

    private float _timer = 0.0f;
    #endregion

    private float _originalFrequency;

    #region methods

    public void RandomGear()
    {
        float posXGen = _gearTransform.position.x + Random.Range(-_rangoGeneracion, _rangoGeneracion);
        float posYGen = _gearTransform.position.y;

        Vector3 posAletaoria = new Vector3( posXGen, posYGen, 0);

        Instantiate(_myGear, posAletaoria, Quaternion.Euler(_gearTransform.rotation.eulerAngles));
    }

    public void DuplicateFrecuence()
    {
        _frequency = _originalFrequency / 2f;
    }
    public void RestoreFrecuence(){
        _frequency = _originalFrequency;
    }

    #endregion
    void Start()
    {
        _gearTransform = transform;
        _originalFrequency = _frequency;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _frequency) 
        {
            RandomGear();
            _timer = 0.0f;
        }
    }

}
