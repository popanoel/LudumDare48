using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PickaxeState{
    Held,
    Trown,
    Grounded
}
public class Pickaxe : MonoBehaviour{

    [SerializeField]
    private PickaxeState _CurrState=PickaxeState.Held;
    [SerializeField]
    private Transform _RefMc;
    [SerializeField]
    private ParticleSystem _RefParticules;
    private Rigidbody2D _monBody;
    private Animator _monAnim;

    private Vector2 _MousePos;
    
    
    void Awake() {
        _monBody=GetComponent<Rigidbody2D>();
        _monAnim=GetComponent<Animator>();
    }
    void Update() {
        //Si pas dans ta main
        if(_CurrState!=PickaxeState.Held){
            return;
        }

        transform.position=new Vector3(_RefMc.position.x-0.1f,_RefMc.position.y+0.2f,_RefMc.position.z);

        if(Input.GetButtonDown("Fire1")){
            StartCoroutine(Trow());
        };
        
        
    }

    public IEnumerator Trow(){
        //Calculate le Angle
        Vector3 leTarget=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Its not exactly on le MC and its gonna stay like that
        float DeltaX=leTarget.x-transform.position.x;
        float DeltaY=leTarget.y-transform.position.y;
        
        Vector2 FinalTarget=new Vector2(DeltaX,DeltaY).normalized;

        _CurrState=PickaxeState.Trown;
        _monAnim.SetBool("IsThrown",true);
                //Random Ass hardcoded number for Speed
        _monBody.AddForce(FinalTarget*9,ForceMode2D.Impulse);

        //After Enough time Land
        yield return new WaitForSeconds(0.5f);

        Land();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_CurrState==PickaxeState.Held){
            return;
        }
        if(_CurrState==PickaxeState.Trown){
            if(other.tag=="Evil"){
                //TODO Wack le Evil
                StopCoroutine(Trow());
                Land();
            }
        }
        if(_CurrState==PickaxeState.Grounded){
            if(other.tag=="Player"){
                _CurrState=PickaxeState.Held;
            }
        }
    }
    
    void Land(){

        _monBody.velocity=Vector2.zero;
        _monAnim.SetBool("IsThrown",false);
        _CurrState=PickaxeState.Grounded;
        _RefParticules.Play(true);

    }

}
