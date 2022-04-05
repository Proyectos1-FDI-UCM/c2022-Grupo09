using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    #region properties
    /// <summary>
    /// Valor que registra si el gameObject estaba activo en el instante anterior
    /// </summary>
    private bool _wasActive;
    #endregion

    #region references
    [SerializeField] private GameObject _josejuGameObject;
    private JoseJuanController _josejuController;
    #endregion

    #region methods
    public void OnActivateGameObject()
    {
        _josejuController.RegisterWaveEnemy(this);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _josejuController = _josejuGameObject.GetComponent<JoseJuanController>();
        _wasActive = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !_wasActive) OnActivateGameObject();
        _wasActive = gameObject.activeSelf;
    }
}
