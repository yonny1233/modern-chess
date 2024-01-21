using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Piece : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 mousePosition;
    public Vector3 prevPosition;
    public boardManager boardManager;

    public healthSlider healthSlider;
    public int pieceHealth;
    

    public virtual void Start(){

        boardManager = FindObjectOfType<boardManager>();
        healthSlider.setMaxHealth(pieceHealth);
        
    }

    public Vector3 getMousePos(){
        return Camera.main.WorldToScreenPoint(transform.position);
        
    }

    private void OnMouseDown(){
        
        if(gameObject.tag == blackOrWhite()){
            displayPositions(true);
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            mousePosition = Input.mousePosition - getMousePos(); 
            setPrevPos();  
            Debug.Log(boardManager.whosTurn);
        }
        
    }
    private void OnMouseUp()
    {
        displayPositions(false);
        if(gameObject.tag == "White"){
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        if(gameObject.tag == "Black"){
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
        if(gameObject.tag == blackOrWhite()){
            transform.position = transform.position - new Vector3(0,1,0);
        }

    }

    private void OnMouseDrag(){
        if(gameObject.tag == blackOrWhite()){
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            newPos.y = 2f;
            transform.position = newPos;
        }
    }

    public void setPrevPos(){
        prevPosition = transform.position;
    }
    public Vector3 getPrevPos(){
        return prevPosition;
    }

    public string blackOrWhite(){
        if(boardManager.whosTurn == true ){
            return "White";

        }else{
            return "Black";
        }

    }
    public void displayPositions(bool x){

        List<tile> both = getLegalTiles();
        both.AddRange(getLegalAttackTiles());
    
        foreach(tile Tile in both ){
            if(x){
                Tile.GetComponent<Renderer>().material.color = Color.red;
            }else{
                for (int i = 0; i < boardManager.allTiles.Length ; i++){
                    if(boardManager.allTiles[i] == Tile) {
                        int row = i/8;
                        int col = i%8;
                        if((row+col)%2 == 1){
                            Tile.GetComponent<Renderer>().material.color = boardManager.Grid.tileColour1;
                            
                        }else{
                            Tile.GetComponent<Renderer>().material.color = boardManager.Grid.tileColour2;
                        }
                    }

                }

            }
        }
    }
    public abstract List<tile> legalPositions();

    public abstract List<tile> legalAttacks();

    public abstract List<tile> getLegalTiles();

    public abstract List<tile> getLegalAttackTiles();

    

    


    



}
