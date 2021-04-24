using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PickaxeState{
    Held,
    Trown,
    Grounded
}
public class PickaxeFollower : MonoBehaviour{

    [SerializeField]
    private PickaxeState _CurrState=PickaxeState.Held;
    private Rigidbody2D _monBody;
    
    void Awake() {
        _monBody=GetComponent<Rigidbody2D>();
    }
    void Update() {
        if(_CurrState!=PickaxeState.Held){
            return;
        }
        if(Input.GetButtonDown("Fire1")){
            StartCoroutine(Trow());
        };
    }

    public IEnumerator Trow(){
        _CurrState=PickaxeState.Trown;
        _monBody.AddForce(new Vector2(5,0),ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        _monBody.velocity=Vector2.zero;
        _CurrState=PickaxeState.Grounded;
        //DOTween.To(()=>DragValue,x=)
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }

}
