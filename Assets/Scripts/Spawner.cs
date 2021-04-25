using UnityEngine;

public class Spawner : MonoBehaviour{
    
    [SerializeField]
    private int _Size=10;
    [SerializeField]
    private float _Spacing=1f;
    [SerializeField]
    private float _Offset=0.5f;
    [SerializeField]
    private Deco[] _Decos;

    void Start() {

        for(float x=0;x<_Size;x+=_Spacing){
            for(float y=0;y<_Size;y+=_Spacing){


                for(int d=0;d<_Decos.Length;d++){

                    
                    Deco deco = _Decos[d];

                    if(deco.canPlace()){

                        Vector3 pos = new Vector3(transform.position.x+x,transform.position.y+y,0f);
                        Vector3 offset = new Vector3(Random.Range(-_Offset,_Offset),Random.Range(-_Offset,_Offset),0f);
                        
                        GameObject newDeco = Instantiate(deco.GetRandom());
                        newDeco.transform.SetParent(transform);
                        newDeco.transform.position=pos+offset;

                        break;
                    }

                }
            }
        }
    }
}
[System.Serializable]
public class Deco{
    public string name;
    [Range(1,30)]
    public int density;
    public GameObject[] prefabs;

    public bool canPlace(){
        if(Random.Range(0,30)<density)
            return true;
        else
            return false;
    }

    public GameObject GetRandom(){
        return prefabs[Random.Range(0,prefabs.Length)];
    }
}