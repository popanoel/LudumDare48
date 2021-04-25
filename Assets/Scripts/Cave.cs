using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave{
    
    public Vector2 gridPos;
    public bool openTop, openBot, openLeft, openRight;
    public bool _isLast=false;

    public Cave(Vector2 _gridPos){
        gridPos = _gridPos;
    }
}
