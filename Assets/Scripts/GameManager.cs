using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour{
    
    private static GameManager _instance;
    [SerializeField]
    Transform _MapRoot;
    [SerializeField]
    WorldGen _Generateur;
    [SerializeField]
    Player _McRef;
    public int _CurrFloor=1;

    void Start() {
         if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    public void ResetFloor(){
        _CurrFloor++;
        foreach (Transform child in _MapRoot) {
            Destroy(child.gameObject);
        }
        _McRef.Reset();
        _Generateur.Regenere();
    }
    public IEnumerator EndGame(){
        yield return new WaitForSeconds(1f);
        GetComponent<GotoScene>().GotoParamScene("End");
        _CurrFloor=1;
    }
    void Update() {
        if(Input.GetButtonDown("Enable Debug Button 2")){
            ResetFloor();
        }
    }

    static public GameManager GetManager{
        get{
            return _instance;
        }
    }
}
