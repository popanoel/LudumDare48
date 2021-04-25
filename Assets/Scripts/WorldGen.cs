using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour{

    private static WorldGen _instance;
    
    //UN CHUNK EST UN CARRÉ DE 12X12 
    private int _ChunkSize=12;
    [SerializeField]
    private GameObject _Room;
   
    private GameObject _Debut;
    private List<List<Transform>> _Rooms;
   void Start() {
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }


       _Rooms=new List<List<Transform>>();
       Run(Vector2.zero);
       
   }

   void Run(Vector2 init){
       for(int y=3;y>=-4;y--){
           _Rooms.Add(new List<Transform>());
            for(int x=-5;x<=4;x++){

                Vector3 pos=new Vector3(_ChunkSize*x+init.x,_ChunkSize*y+init.y,0);
                GameObject room= Instantiate(_Room,pos,Quaternion.identity,transform);
                room.name="R"+(x+5)+""+(y-3)*-1;
                room.GetComponent<Room>()._maPos=new Vector2 (x+5,(y-3)*-1);
                _Rooms[(y-3)*-1].Add(room.transform);

                //if Starting room
                if(y==0&&x==0){
                    _Debut=room;
                }

            }
        }
   }

    static public WorldGen GetWorldGen{
        get{
            return _instance;
        }
    }
    public List<List<Transform>> getRooms{
        get{
            return _Rooms;
        }
    }
}
