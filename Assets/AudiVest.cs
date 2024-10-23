using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiVest : MonoBehaviour
{
    private AudioSource _vestibul;
    void Start()
    {
        _vestibul = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (!_vestibul.isPlaying)
        {
            gameObject.SetActive(false);
            Destroy(_vestibul.GetComponent<AudioSource>());
            //AudioSource
        }
    }
}
