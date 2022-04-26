using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartJoseju : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _joseju;
    private JoseJuanController _josejuController;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character_HealthManager player = collision.GetComponent<Character_HealthManager>();
        if (player != null)
        {
            _josejuController.StartJoseju();
            Destroy(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _josejuController = _joseju.GetComponent<JoseJuanController>();
    }
}
