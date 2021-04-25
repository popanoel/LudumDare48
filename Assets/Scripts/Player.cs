using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
   //Movement variables
    private float _H=0f;
    private float _V=0f;
    private Rigidbody2D _monBody;
    private Animator _monAnim;
    private SpriteRenderer _monSpriteRenderer;
    private AudioSource _monSpeaker;
    // private AudioSource _monSon;

    [SerializeField][Range(1f,6f)]
    private float _Speed=0f;

    [SerializeField]
    private Pickaxe _myPick;
    //State bool
    private bool _isBusy=false;
    private bool isFlipped = false;
    [SerializeField]
    private AudioClip _dies;
    [SerializeField]
    private List<AudioClip> _steps;
    void Awake(){
        _monAnim=GetComponent<Animator>();
        _monBody=GetComponent<Rigidbody2D>();
        _monSpriteRenderer=GetComponent<SpriteRenderer>();
        _monSpeaker=GetComponent<AudioSource>();
    //    _monSon=GetComponent<AudioSource>();
    }
    void Start() {
        StartCoroutine(WalkingSound());
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

        if ((_H < 0 && !isFlipped) || (_H > 0 && isFlipped)) {
            Flip();
        }

        _monAnim.SetFloat("AxeX",_H);
        _monAnim.SetFloat("AxeY",_V);
    }

    void FixedUpdate(){
        _monBody.velocity=new Vector3(_H*_Speed,_V*_Speed,0f); 
    }

    public void Reset(){
        _isBusy=true;
        transform.position=Vector3.zero;
        _isBusy=false;
        _myPick.GetPickedUp();
    }

    public void Die(){
        _isBusy=true;
        _monSpeaker.PlayOneShot(_dies);
        //Play Animation ---> Fuck taht
        StartCoroutine(GameManager.GetManager.EndGame());
    }
    IEnumerator WalkingSound(){
        while(true){
            yield return new WaitForSecondsRealtime(0.5f);
            if(_H!=0 || _V!=0){
                _monSpeaker.panStereo*=-1;
                _monSpeaker.PlayOneShot(_steps[Random.Range(0,_steps.Count)]);
                
            }
        }

    }
    void Flip() {
        isFlipped = !isFlipped;
        _monSpriteRenderer.flipX=isFlipped;
        
    }
}
