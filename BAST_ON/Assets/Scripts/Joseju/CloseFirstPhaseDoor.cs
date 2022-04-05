using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFirstPhaseDoor : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _joseju;
    private JoseJuanController _joseJuanController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            _door.SetActive(true);
            _joseJuanController.PlaceFirstPhaseCamera();
        }
    }
    #endregion
    private void Start()
    {
        _joseJuanController = _joseju.GetComponent<JoseJuanController>();
    }
}
