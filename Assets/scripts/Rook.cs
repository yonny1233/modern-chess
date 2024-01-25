using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece 
{

    private tile onTile;
    public float fallSpeed;
    public bool isCollision;

    private tile[] allTiles;
    public List<List<tile>> legalTiles;
    public List<tile> legalAttackTiles;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        allTiles = FindObjectsOfType<tile>();
        setPrevPos();
        prevPosition -= new Vector3(0,0.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        legalTiles = legalPositions();
        legalAttackTiles = legalAttacks();
        if(healthSlider.getHealth() <= 0){
            Destroy(gameObject);
            
        }
        if(!isCollision){
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        }
        Ray ray = new Ray(transform.position, Vector3.down); // Assuming the chessboard is on the XZ plane
        RaycastHit hit;
        
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask("Chessboard")))
        {
            GameObject hitObject = hit.collider.gameObject;
            transform.position = new Vector3(hitObject.transform.position.x, transform.position.y, hitObject.transform.position.z);
           
        }    
    }
   
    private void OnTriggerEnter(Collider other)
    {
        isCollision = true;
        onTile = other.GetComponent<tile>();
    }
    private void OnTriggerExit(Collider other){
        isCollision = false;  
        
    }

    public override List<List<tile>> legalPositions(){
        List<List<tile>> legalTileArray = new List<List<tile>>(); 

        for(int i = 0; i < allTiles.Length; i++){ //loops through all the tiles until it has found the tile that it is on.
            
            if (allTiles[i] == onTile){
                List<tile> first = new List<tile>();
                first.Add(onTile);
                legalTileArray.Add(first);

                bool isForward = true;
                int leftOrRight = 1;
                for(int n = 0; n < 2; n++){ // for backwards, left, forwards and right
                    
                    int x = infrontTile(i, isForward);
                    int counter = 0;
                    List<tile> second = new List<tile>();
                    while(x < allTiles.Length && x >= 0 &&  allTiles[x].getPieceCount() == 0){
                        second.Add(allTiles[x]);
                        
                        x = infrontTile(x, isForward);
                        
                    }
                    legalTileArray.Add(second);

                    x = i + leftOrRight;
                    List<tile> third = new List<tile>();
                    while(x >= 0 && x < allTiles.Length &&  x%8 != 0 && x%8 != 7 && allTiles[x].getPieceCount() == 0){
                        third.Add(allTiles[x]);
                        x += leftOrRight;
                        counter++;
                    }
                    if(x < allTiles.Length && x > 0 && counter>0 && allTiles[x].getPieceCount() == 0){
                        third.Add(allTiles[x]);

                    }
                    legalTileArray.Add(third);
                    leftOrRight = -1;
                    isForward = false;
                    
                }
                break;
            }
        }
       
        return legalTileArray;
    }
    public override List<tile> legalAttacks(){
        List<tile> legalAttackArray = new List<tile>();
        for(int i = 0; i < allTiles.Length; i++){ //loops through all the tiles until it has found the tile that it is on.
            
            if (allTiles[i] == onTile){
                legalAttackArray.Add(onTile);

                bool isForward = true;
                int leftOrRight = 1;
                for(int n = 0; n < 2; n++){ // for backwards, left, forwards and right
                    
                    int x = infrontTile(i, isForward);
                    int counter = 0;
                    while(x < allTiles.Length && x >= 0 ){

                        if(allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag == this.tag){
                            
                            break;
                        }
                        else if(allTiles[x].getPieceCount() > 0){
                            legalAttackArray.Add(allTiles[x]);
                            break;
                        }
                        x = infrontTile(x, isForward);
                        
                    }

                    x = i + leftOrRight;
                    while(x >= 0 && x < allTiles.Length &&  x%8 != 0 && x%8 != 7 ){
                        if(allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag == this.tag){
                            break;
                        }
                        else if(allTiles[x].getPieceCount() > 0){
                            legalAttackArray.Add(allTiles[x]);
                            break;
                        }
                        x += leftOrRight;
                        counter++;
                        
                    }
                    if(x < allTiles.Length && x > 0 && counter>0){
                        if(allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag == this.tag){
                            
                        }
                        
                        legalAttackArray.Add(allTiles[x]);

                    }
                    leftOrRight = -1;
                    isForward = false;
                    
                }
                break;
            }
        }
        return legalAttackArray;
    }
    public override void ability(bool x){

    }

    public override List<List<tile>> getLegalTiles(){
        return legalTiles;
    }

    public override List<tile> getLegalAttackTiles(){
        return legalAttackTiles;

    }

    public int infrontTile(int i, bool isForward){ 
        int row = i/8;
        int col = i%8;
        int newRow = isForward ? row-1 : row+1;
        int newIndex = newRow * 8 + col;
        return newIndex;
    }
        

    
    
}
