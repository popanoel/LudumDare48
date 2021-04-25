using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Exit : MonoBehaviour
{
    
    [SerializeField]
    private AudioClip _next;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            GetComponent<AudioSource>().PlayOneShot(_next);
            GameManager.GetManager.ResetFloor();
        }
    }
}
