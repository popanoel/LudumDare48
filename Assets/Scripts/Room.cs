using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{
    //Mur a Blow up
    public bool _up, _down, _left, _right;
    public bool _imLast=false;
    [SerializeField]
    private GameObject _leExit;
    [SerializeField]
    private GameObject _murUp, _murDown, _murLeft, _murRight;
    

    void Start() {
        
        Setup();
    }

    void Setup(){
        _murUp.SetActive(!_up);
        _murDown.SetActive(!_down);
        _murLeft.SetActive(!_left);
        _murRight.SetActive(!_right);

        if(_imLast){
            Instantiate(_leExit,transform.position,Quaternion.identity,transform);
        }
    }
}
