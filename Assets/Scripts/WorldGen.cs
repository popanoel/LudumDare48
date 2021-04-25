using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour{

    private static WorldGen _instance;
    private Vector2 caveSize = new Vector2(6,6);
    private Cave[,] caves;
    private List<Vector2> takenCaves = new List<Vector2>();
    private int gridX, gridY;
    private int nbCaves=15;
    [SerializeField]
    private GameObject _caveObj;
    [SerializeField]
    private Transform caveRoot;

   void Start() {
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        // make sure we dont try to make more rooms than can fit in our grid
        if (nbCaves >= (caveSize.x * 2) * (caveSize.y * 2)){ 
			nbCaves = Mathf.RoundToInt((caveSize.x * 2) * (caveSize.y * 2));
		}

        gridX = Mathf.RoundToInt(caveSize.x);
        gridY = Mathf.RoundToInt(caveSize.y);

        Genere();
        SetRoomDoors();
        DrawMap();
       
   }

    #region Code I took online
    private void Genere(){
        //Setup
        caves = new Cave[gridX*2,gridY*2];
        caves[gridX,gridY] = new Cave(Vector2.zero);
        takenCaves.Insert(0,Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        //magic numbers //Tweaked 
		float randomCompare = 0.2f, randomCompareStart = 0.75f, randomCompareEnd = 0.01f;
		//add Caves
		for (int i =0; i < nbCaves -1; i++){
			float randomPerc = ((float) i) / (((float)nbCaves - 1));
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			//grab new position
			checkPos = NewPosition();
			//test new position
			if (NumberOfNeighbors(checkPos, takenCaves) > 1 && Random.value > randomCompare){
				int iterations = 0;
				do{
					checkPos = SelectiveNewPosition();
					iterations++;
				}while(NumberOfNeighbors(checkPos, takenCaves) > 1 && iterations < 100);
				if (iterations >= 50)
					print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenCaves));
			}
			//finalize position
			caves[(int) checkPos.x + gridX, (int) checkPos.y + gridY] = new Cave(checkPos);
			//fuck it add ending to last one generated
			if(i==nbCaves-2){
				caves[(int) checkPos.x + gridX, (int) checkPos.y + gridY]._isLast=true;
			}
			takenCaves.Insert(0,checkPos);
		}
    }

    Vector2 NewPosition(){
		int x = 0, y = 0;
		Vector2 checkingPos = Vector2.zero;
		do{
			int index = Mathf.RoundToInt(Random.value * (takenCaves.Count - 1)); // pick a random room
			x = (int) takenCaves[index].x;//capture its x, y position
			y = (int) takenCaves[index].y;
			bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
			bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
			if (UpDown){ //find the position bnased on the above bools
				if (positive){
					y += 1;
				}else{
					y -= 1;
				}
			}else{
				if (positive){
					x += 1;
				}else{
					x -= 1;
				}
			}
			checkingPos = new Vector2(x,y);
		}while (takenCaves.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY); //make sure the position is valid
		return checkingPos;
	}
    Vector2 SelectiveNewPosition(){ // method differs from the above in the two commented ways
		int index = 0, inc = 0;
		int x =0, y =0;
		Vector2 checkingPos = Vector2.zero;
		do{
			inc = 0;
			do{ 
				//instead of getting a room to find an adject empty space, we start with one that only 
				//as one neighbor. This will make it more likely that it returns a room that branches out
				index = Mathf.RoundToInt(Random.value * (takenCaves.Count - 1));
				inc ++;
			}while (NumberOfNeighbors(takenCaves[index], takenCaves) > 1 && inc < 100);
			x = (int) takenCaves[index].x;
			y = (int) takenCaves[index].y;
			bool UpDown = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);
			if (UpDown){
				if (positive){
					y += 1;
				}else{
					y -= 1;
				}
			}else{
				if (positive){
					x += 1;
				}else{
					x -= 1;
				}
			}
			checkingPos = new Vector2(x,y);
		}while (takenCaves.Contains(checkingPos) || x >= gridX || x < -gridX || y >= gridY || y < -gridY);
		if (inc >= 100){ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
			print("Error: could not find position with only one neighbor");
		}
		return checkingPos;
	}
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions){
		int ret = 0; // start at zero, add 1 for each side there is already a room
		if (usedPositions.Contains(checkingPos + Vector2.right)){ //using Vector.[direction] as short hands, for simplicity
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.left)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.up)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector2.down)){
			ret++;
		}
		return ret;
	}
    void DrawMap(){
		foreach (Cave cave in caves){
			if (cave == null){
				continue; //skip where there is no room
			}
			Vector2 drawPos = cave.gridPos;
			drawPos.x *= 12;//aspect ratio of caves
			drawPos.y *= 12;
			//create map obj and assign its variables
			Room room = Object.Instantiate(_caveObj, drawPos, Quaternion.identity).GetComponent<Room>();
			room._up = cave.openTop;
			room._down = cave.openBot;
			room._right = cave.openRight;
			room._left = cave.openLeft;
			room._imLast = cave._isLast;
			room.gameObject.transform.parent = caveRoot;
		}
	}
    void SetRoomDoors(){
		for (int x = 0; x < ((gridX * 2)); x++){
			for (int y = 0; y < ((gridY * 2)); y++){
				if (caves[x,y] == null){
					continue;
				}
				Vector2 gridPosition = new Vector2(x,y);
				if (y - 1 < 0){ //check above
					caves[x,y].openBot = false;
				}else{
					caves[x,y].openBot = (caves[x,y-1] != null);
				}
				if (y + 1 >= gridY * 2){ //check bellow
					caves[x,y].openTop = false;
				}else{
					caves[x,y].openTop = (caves[x,y+1] != null);
				}
				if (x - 1 < 0){ //check left
					caves[x,y].openLeft = false;
				}else{
					caves[x,y].openLeft = (caves[x - 1,y] != null);
				}
				if (x + 1 >= gridX * 2){ //check right
					caves[x,y].openRight = false;
				}else{
					caves[x,y].openRight = (caves[x+1,y] != null);
				}
			}
		}
	}

    #endregion
	public void Regenere(){
		takenCaves=new List<Vector2>();
		   // make sure we dont try to make more rooms than can fit in our grid
        if (nbCaves >= (caveSize.x * 2) * (caveSize.y * 2)){ 
			nbCaves = Mathf.RoundToInt((caveSize.x * 2) * (caveSize.y * 2));
		}

        gridX = Mathf.RoundToInt(caveSize.x);
        gridY = Mathf.RoundToInt(caveSize.y);

        Genere();
        SetRoomDoors();
        DrawMap();
	}
}
