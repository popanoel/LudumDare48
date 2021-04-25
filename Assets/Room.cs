using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour{
    
    public Vector2 _maPos;
    private List<Room> _mesVoisins;
    private List<List<Transform>> _Rooms;

    public bool _HasRoom=false; 

    void Start() {
    }

    public void Run(){
        _Rooms=WorldGen.GetWorldGen.getRooms;
        //find Voisin
        //RefreshVoisin();
        List<Room> _FutureRooms = new List<Room>();

        _FutureRooms.Add(_Rooms[(int)_maPos.y+1][(int)_maPos.x].GetComponent<Room>());
        _FutureRooms.Add(_Rooms[(int)_maPos.y+1][(int)_maPos.x].GetComponent<Room>());
        _FutureRooms.Add(_Rooms[(int)_maPos.y+1][(int)_maPos.x].GetComponent<Room>());
        _FutureRooms.Add(_Rooms[(int)_maPos.y+1][(int)_maPos.x].GetComponent<Room>());

        //is already Room?
        foreach(Room voisin in _FutureRooms){
            if(voisin._HasRoom){
                _FutureRooms.Remove(voisin);
            }
        }
        //is voisin has voisin?
        foreach(Room voisin in _FutureRooms){
            if(voisin.HasVoisinRoom()){
                _FutureRooms.Remove(voisin);
            }
        }
        //is Big Enough  
        //50% drop
        foreach(Room voisin in _FutureRooms){
            if(Random.value<0.5){
                _FutureRooms.Remove(voisin);
            }
        }
        //is now Room
        foreach(Room voisin in _FutureRooms){
            voisin._HasRoom=true;
            GetComponent<SpriteRenderer>().enabled=true;
            voisin.Run();
        }
    }

    void RefreshVoisin(){
        _mesVoisins=new List<Room>();
        //_mesVoisins.Add(_Rooms[(int)_maPos.y+1][(int)_maPos.x].GetComponent<Room>());
       // _mesVoisins.Add(_Rooms[(int)_maPos.y-1][(int)_maPos.x].GetComponent<Room>());
       // _mesVoisins.Add(_Rooms[(int)_maPos.y][(int)_maPos.x+1].GetComponent<Room>());
       // _mesVoisins.Add(_Rooms[(int)_maPos.y][(int)_maPos.x-1].GetComponent<Room>());
    }
    
    public bool HasVoisinRoom(){
        RefreshVoisin();
        int nbVoisin=0;
        foreach(Room voisin in _mesVoisins){
            if(voisin._HasRoom==true){
                nbVoisin++;
            }
        }
        if(nbVoisin>1){
            return true;
        }
        return false;
    }

}
