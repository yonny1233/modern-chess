using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardManager : MonoBehaviour
{

    public grid Grid;
    public tile[] allTiles;
    public Piece[] allPieces;

    public bool whosTurn;
    // Start is called before the first frame update
    void Start()
    {
        whosTurn = true;
        Vector2[,] coordinates = Grid.CreateCoordinateGrid();
        Grid.createGrid();
        Grid.spawnPieces(coordinates);
        
        allTiles = FindObjectsOfType<tile>();
        allPieces = FindObjectsOfType<Piece>();
    }

    // Update is called once per frame
    void Update()
    {
        bool switchs = false;
        for(int i = 0; i < allTiles.Length; i++ ){
            if(allTiles[i].publicLegal == true){
                whosTurn = !whosTurn;
                switchs = true;
                
                
                allTiles[i].publicLegal = false;

            }
        }
        for(int i = 0; i < allPieces.Length; i++){ //locks so that the piece doesn't randomly switch state whilst initiating attack
            if(switchs == true ){
                allPieces[i].locks = true;
                
            }
        }

    }

    
}
