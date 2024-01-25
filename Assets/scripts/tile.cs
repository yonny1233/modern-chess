using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{

    private int pieceCount = 0;
    //private Piece chessPiece;
    public bool publicLegal;
    private Piece currentChessPiece;
    public bool pawnAbility = false; // mudPawnAbility stored locally
    public string names = "";


    void Start()
    {
        
        
    }

    private void Update(){
        if(currentChessPiece is Pawn && currentChessPiece.mudPawnAbility == true ){
            currentChessPiece.ability(true);
            
        }
        
    }
    private void OnTriggerEnter(Collider other){
        
        Piece chessPiece = other.GetComponent<Piece>();
        if (chessPiece != null){
            pieceCount++;
            if(pieceCount == 1){ //any less means to chess piece and anymore means that the current onTriggerEnter collider is an object that isn't mean
                currentChessPiece = chessPiece;
            }
            
        }
        
       Piece pieceToMove; 

        switch(chessPiece){
            case Pawn pawnPiece:
                pieceToMove = pawnPiece;
                break;

            case Bishop bishopPiece:
                pieceToMove = bishopPiece;
                break;

            case Knight knightPiece:
                pieceToMove = knightPiece;
                break;

            case Rook rookPiece:
                pieceToMove = rookPiece;
                break;
            case Queen queenPiece:
                pieceToMove = queenPiece;
                break;

            default:
                pieceToMove = null;
                break;
            
        }
        if((pieceToMove is Pawn  || pieceToMove is Bishop || pieceToMove is Knight || pieceToMove is Rook || pieceToMove is Queen) && pieceToMove.locks == false){
            bool legal = false;
          
            //pieceToMove.getLegalTiles().Any(sublist => sublist.Contains(this));
            foreach(List<tile> list in pieceToMove.getLegalTiles()){
                if(list.Contains(this)){
                    legal = true;
                }
            }
            if(pieceToMove.getLegalTiles()[0][0] != this){
                publicLegal = true;
                if(pieceToMove is Pawn){
                    pieceToMove.firstMove = false;
                }
            }

            
            if(pieceToMove.getLegalAttackTiles().Contains(this)){
                legal = true;
                publicLegal = true;
                currentChessPiece.healthSlider.setHealth(currentChessPiece.healthSlider.getHealth() - pieceToMove.attackDamage());
                
                if(currentChessPiece.healthSlider.getHealth() <= 0){
                    pieceCount--;
                    currentChessPiece = pieceToMove; //sets the current piece on the tile to the piece that has killed the previous piece
                }
        
            }

            if(!legal){
                pieceToMove.transform.position = chessPiece.getPrevPos()  +  new Vector3(0,0.1f,0);; 
                
                publicLegal = false;  
            }
        }

        if (pieceCount>1 ){
            chessPiece.transform.position = chessPiece.getPrevPos() + new Vector3(0,1,0);;
            //chessPiece = currentChessPiece;
            
        }
        

    }

    void OnTriggerExit(Collider other)
    {
        // Reduce the piece count when a piece exits the tile

        Piece chessPiece = other.GetComponent<Piece>();
        if (chessPiece != null){
            pieceCount--;
           /*if (chessPiece is Pawn){
                chessPiece.mudPawnAbility = false;
           }*/
        }
        
    }

    public Piece occupiedPiece(){
        if (currentChessPiece != null){
           return currentChessPiece; 
        }else  
            return null;
        
    }
    public int getPieceCount(){
        return pieceCount;
    }
    public void setPieceCount(int x){
        pieceCount = x;
        
    }
}