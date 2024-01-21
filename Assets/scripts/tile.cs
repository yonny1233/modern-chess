using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{

    private int pieceCount = 0;
    private Piece chessPiece;
    public bool publicLegal;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        chessPiece = other.GetComponent<Piece>();
        if (chessPiece != null){
            pieceCount++;
            
        }
        if (pieceCount>1 ){
            chessPiece.transform.position = chessPiece.getPrevPos() + new Vector3(0,1,0);;
            
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
        if(pieceToMove is Pawn  || pieceToMove is Bishop || pieceToMove is Knight || pieceToMove is Rook || pieceToMove is Queen){
            bool legal = false;
          
            for(int i = 0; i < pieceToMove.getLegalTiles().Count; i++){
                

                if(pieceToMove.getLegalTiles().Contains(this)){
                    legal = true;
            
                    if(pieceToMove.getLegalTiles()[0] != this){
                        publicLegal = true;
                    }
                    
                    break;
                }
            }
            int index = 0;

            for(int i = 0; i < pieceToMove.getLegalAttackTiles().Count; i++){
                
                if(pieceToMove.getLegalAttackTiles().Contains(this)){
                    legal = true;
                    if(pieceToMove.getLegalAttackTiles()[0] != this){
                        publicLegal = true;
                    }
                    
                    index = i;
                  
                    break;
                }
            }

            if(!legal){
                pieceToMove.transform.position = chessPiece.getPrevPos()  +  new Vector3(0,0.1f,0);; 
                publicLegal = false;   
            }
            if(legal){
                

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reduce the piece count when a piece exits the tile

        Piece chessPiece = other.GetComponent<Piece>();
        if (chessPiece != null){
            pieceCount--;
            
        }
    }
    public Piece occupiedPiece(){
        if (chessPiece != null){
           return chessPiece; 
        }else  
            return null;
        
    }
    public int getPieceCount(){
        return pieceCount;
    }
}