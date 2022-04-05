using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{

    #region references
    [SerializeField] private GameObject _josejuGameObject;
    private JoseJuanController _josejuController;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _josejuController = _josejuGameObject.GetComponent<JoseJuanController>();
        _josejuController.RegisterWaveEnemy(this);
    }
}
