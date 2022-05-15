using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsquivDisparo : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _myShot, _myOrigin;
    private Transform _myTransform;
    #endregion
    #region parameters
    [SerializeField]
    private float freq = 2;
    #endregion
    #region properties
    private float timer;
    private float _originalFrequency;
    #endregion

    #region methods
    private void SlowDown(float slowDown)
    {
        freq = _originalFrequency * slowDown;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = _myOrigin.transform;
        _originalFrequency = freq;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= freq)
        {
            Instantiate(_myShot, _myTransform.position, Quaternion.Euler(_myTransform.rotation.eulerAngles));
            timer = 0;
        }

        // Comprobación de si hay un Kiwi Activo
        if (GameManager.Instance.GetKiwiActive()) SlowDown(GameManager.Instance.GetKiwiSlowDown());
        else freq = _originalFrequency;
    }
}
