using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece 
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

        firstMove = true;
        base.Start();
        allTiles = FindObjectsOfType<tile>(); // stores 1d array of tiles
        setPrevPos(); // set previous position at start so it isn't set to 0,0,0
        prevPosition -= new Vector3(0,0.5f,0); // bug fix to make sure pieces dont go through floor
    }

    // Update is called once per frame
    void Update()
    {
        legalTiles = legalPositions();
        legalAttackTiles = legalAttacks();
        
        if(healthSlider.getHealth() <= 0){
            Destroy(gameObject);
            
        }

        if(!isCollision){ //piece falls until it hits a tile
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
        isCollision = true; //when hitting a tile then a collision is detected so no more falling.
        onTile = other.GetComponent<tile>(); //set to the tile that the piece is on.
        mudPawnAbility = true;
    }
    private void OnTriggerExit(Collider other){
        isCollision = false;  //starts falling again.
        mudPawnAbility = false;
    
    }

    public override List<List<tile>> legalPositions(){
        List<List<tile>> legalTileArray = new List<List<tile>>(); //legal positions: current position, position in front and position infront of that. could replace with recursion.
        for(int i = 0; i < allTiles.Length; i++){ //loops through all the tiles until it has found the tile that it is on.
            
            if (allTiles[i] == onTile){
                List<tile> first = new List<tile>();
                allTiles[i].names = "firstTile";
                first.Add(allTiles[i]);
                legalTileArray.Add(first); //set legal position => current position
                List<tile> second = new List<tile>();

                int x = infrontTile(i);
                if(x < allTiles.Length  && x > 0 && allTiles[x].getPieceCount() == 0){ //boundaries to ensure no index out of bounds error, could replace with try, catch
                    second.Add(allTiles[x]);
                }else{
                    legalTileArray.Add(second);
                    break;
                }
                x = infrontTile(x);
                if(x < allTiles.Length  && x > 0 && allTiles[x].getPieceCount() == 0 && firstMove){ //another boundary
                    second.Add(allTiles[x]);
                }
                legalTileArray.Add(second);
                
                
                
            }
        }
        return legalTileArray;
    }

    public override List<tile> legalAttacks(){
        List<tile> legalAttackArray = new List<tile>();
        for(int i = 0; i < allTiles.Length; i++){
            
            if (allTiles[i] == onTile){
                if((allTiles[infrontTile(i)-1].getPieceCount() > 0) && i%8 != 0 && allTiles[infrontTile(i)-1].occupiedPiece().tag != this.tag  ){ //check if there is a piece to attack & make sure piece isn't on the edge.
                    legalAttackArray.Add(allTiles[infrontTile(i)-1]);
                    
                }
                if(allTiles[infrontTile(i)+1].getPieceCount() > 0 && i%8 != 7  && allTiles[infrontTile(i)+1].occupiedPiece().tag != this.tag){ //check if there is a piece to attack & make sure piece isn' ton the other edge.
                    legalAttackArray.Add(allTiles[infrontTile(i)+1]);
                    
                }

                break;
            }
        }
        return legalAttackArray;

    }
    public override void ability(bool x){
        tile abilityCast;
        for(int i = 0; i < allTiles.Length; i++){
            if(allTiles[i] == onTile && !firstMove ){
                abilityCast = allTiles[infrontTile(i)];
                if(x){
                    abilityCast.GetComponent<Renderer>().material.color = new Color(0.6f, 0.4f, 0.2f);
                    abilityCast.pawnAbility = true;
                    
                }else{
                    abilityCast.pawnAbility = false;
                    for (int m = 0; m < allTiles.Length ; m++){
    
                        if(allTiles[m] == abilityCast) {
                            
                            int row = m/8;
                            int col = m%8;
                            if((row+col)%2 == 1){
                                abilityCast.GetComponent<Renderer>().material.color = boardManager.Grid.tileColour1;
                                
                            }else{
                                abilityCast.GetComponent<Renderer>().material.color = boardManager.Grid.tileColour2;
                            }
                        }

                    }
                }
            }
        }
        
        
        
        

    }
    

    public override List<List<tile>> getLegalTiles(){
        return legalTiles;
    }

    public override List<tile> getLegalAttackTiles(){
        return legalAttackTiles;

    }
    
    public int infrontTile(int i){ 
        int row = i/8;
        int col = i%8;
        bool isWhite = this.CompareTag("White");
        int newRow = isWhite ? row-1 : row+1;
        int newIndex = newRow * 8 + col;
        return newIndex;
    }
    

    
    
}

