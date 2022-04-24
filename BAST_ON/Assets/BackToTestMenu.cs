using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTestMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToTestMenuFunction()
    {
        SceneManager.LoadSceneAsync("MenuPruebas");
    }
}
