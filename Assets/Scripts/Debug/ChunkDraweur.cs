using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDraweur : MonoBehaviour
{
    private float _MoitierChunk=6f;
    //private float _Chunk=8f; ----- Obselete -----
    // Update is called once per frame
    void Update(){
        //top
        Debug.DrawLine(new Vector3(transform.position.x-_MoitierChunk,transform.position.y+_MoitierChunk,transform.position.z),new Vector3(transform.position.x+_MoitierChunk,transform.position.y+_MoitierChunk,transform.position.z),Color.cyan);
        //bottom
        Debug.DrawLine(new Vector3(transform.position.x-_MoitierChunk,transform.position.y-_MoitierChunk,transform.position.z),new Vector3(transform.position.x+_MoitierChunk,transform.position.y-_MoitierChunk,transform.position.z),Color.cyan);
        //Left
        Debug.DrawLine(new Vector3(transform.position.x-_MoitierChunk,transform.position.y+_MoitierChunk,transform.position.z),new Vector3(transform.position.x-_MoitierChunk,transform.position.y-_MoitierChunk,transform.position.z),Color.cyan);
        //Right
        Debug.DrawLine(new Vector3(transform.position.x+_MoitierChunk,transform.position.y-_MoitierChunk,transform.position.z),new Vector3(transform.position.x+_MoitierChunk,transform.position.y+_MoitierChunk,transform.position.z),Color.cyan);
    }
}
