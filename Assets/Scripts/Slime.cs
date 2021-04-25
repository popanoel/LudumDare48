using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime : MonoBehaviour{
    private bool _IsMoving=false;
    private float _Speed=0.6f;
    void FixedUpdate() {
        if(_IsMoving){
            return;
        }
        if(Random.value>0.97f){
            Move();
        }
    }
    void Move(){
        float dir=Random.value;

        if(dir<0.25f){
            //drette
            transform.DOMove(new Vector3(transform.position.x+_Speed,transform.position.y,transform.position.z),0.5f);
        }else if(dir<0.5f){
            //Bas
            transform.DOMove(new Vector3(transform.position.x,transform.position.y-_Speed,transform.position.z),0.5f);

        }else if(dir<0.75f){
            //Goche
            transform.DOMove(new Vector3(transform.position.x-_Speed,transform.position.y,transform.position.z),0.5f);

        }else{
            //Haut
            transform.DOMove(new Vector3(transform.position.x,transform.position.y+_Speed,transform.position.z),0.5f);

        }
    }
    public void Smite(){
        GetComponent<BoxCollider2D>().enabled=false;
        _IsMoving=true;
        //Play animation
        GameObject.Destroy(gameObject,1f);
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            other.GetComponent<Player>().Die();
        }
    }
}
