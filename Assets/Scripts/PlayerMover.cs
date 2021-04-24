using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour{
   //Movement variables
    private float _H=0f;
    private float _V=0f;
    private Rigidbody2D _monBody;
    // private AudioSource _monSon;

    [SerializeField][Range(1f,6f)]
    private float _Speed=0f;

    //State bool
    private bool _isBusy=false;
    void Awake(){
        _monBody=GetComponent<Rigidbody2D>();
    //    _monSon=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){

        if(_isBusy){
            _H=0f;
            _V=0f;
            return;
        }

        _H=Input.GetAxis("Horizontal");
        _V=Input.GetAxis("Vertical");
    }

    void FixedUpdate(){
        _monBody.velocity=new Vector3(_H*_Speed,_V*_Speed,0f);
        
    }


}
