using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece 
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
                List<tile> second =  new List<tile>();
                for(int n = 0; n < 2; n++){ // for backwards and forwards
                    
                    int x = infrontTile( infrontTile( i, isForward ), isForward )-1;
                    if(x <= allTiles.Length && x >= 0 && i%8 != 0 && allTiles[x].getPieceCount() == 0   ){
                        second.Add(allTiles[x] );
                    }
                    x = infrontTile(i, isForward )-2;
                    
                    if(x <= allTiles.Length && x >= 0 && i%8 != 0 && i%8 != 1 && allTiles[x].getPieceCount() == 0 ){
                        second.Add(allTiles[x] );
                        
                    }
                    x = infrontTile( infrontTile( i, isForward ), isForward )+1;
                    if(x <= allTiles.Length && x >= 0 && i%8 != 7 && allTiles[x].getPieceCount() == 0) {
                        second.Add(allTiles[x] ); 
                        
                    }
                    x = infrontTile( i, isForward )+2;
                    if(x <= allTiles.Length && x >= 0  && i%8 != 7 && i%8 != 6 && allTiles[x].getPieceCount() == 0){
                        second.Add(allTiles[x] );
                    }
                    isForward = false;
                }
                legalTileArray.Add(second);
                break;
            }
        }
       
        return legalTileArray;
    }
    public override List<tile> legalAttacks(){
        List<tile> legalAttackArray = new List<tile>();
        for(int i = 0; i < allTiles.Length; i++){ //loops through all the tiles until it has found the tile that it is on.
            
            if (allTiles[i] == onTile){

                bool isForward = true;
                for(int n = 0; n < 2; n++){ // for backwards and forwards

                    int x = infrontTile( infrontTile( i, isForward ), isForward )-1;
                    if(x <= allTiles.Length && x >= 0 && i%8 != 0 && allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag != this.tag ){
                        legalAttackArray.Add(allTiles[x] );
                    }
                    x = infrontTile(i, isForward )-2;
                    
                    if(x <= allTiles.Length && x >= 0 && i%8 != 0 && i%8 != 1 && allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag != this.tag ){
                        legalAttackArray.Add(allTiles[x] );
                        
                    }
                    x = infrontTile( infrontTile( i, isForward ), isForward )+1;
                    if(x <= allTiles.Length && x >= 0 && i%8 != 7 && allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag != this.tag ) {
                        legalAttackArray.Add(allTiles[x] ); 
                        
                    }
                    x = infrontTile( i, isForward )+2;
                    if(x <= allTiles.Length && x >= 0 && i%8 != 7 && i%8 != 6  && allTiles[x].getPieceCount() > 0 && allTiles[x].occupiedPiece().tag != this.tag ){
                        legalAttackArray.Add(allTiles[x] );
                    }
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
