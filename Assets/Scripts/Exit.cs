using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Exit : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            GameManager.GetManager.ResetFloor();
        }
    }
}
