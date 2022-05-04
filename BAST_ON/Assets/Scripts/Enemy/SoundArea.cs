using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour {
    #region parameters
    private bool _soundPlayed=true;
    #endregion
    #region references
    private AudioSource _myAudioSource;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Character_HealthManager Player = collision.gameObject.GetComponent<Character_HealthManager>();
        if (Player == null)
        {
            
            _soundPlayed = false;

        }
        else
        {
            //Debug.Log("a");
            _soundPlayed = true;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!_soundPlayed)_myAudioSource.Play();
        _soundPlayed = true;
    }
}
