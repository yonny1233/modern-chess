using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tilePrefab;
    public GameObject pawn;
    public GameObject rook;
    public GameObject knight;
    public GameObject bishop;
    public GameObject king;
    public GameObject queen;
    public int board_z;
    public int board_x;
    public Color tileColour1;
    public Color tileColour2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnPieces(Vector2[,] coords){
        int counter = 0;
        GameObject[] firstRank = { rook, knight, bishop, queen, king, bishop, knight, rook };
        GameObject[] firstRank_black = { rook, knight, bishop, king, queen, bishop, knight, rook };
        
        
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        while(counter <  board_x ){
             GameObject pieceSecond = Instantiate(pawn, new Vector3(((int)coords[counter,1].x),1, ((int)coords[counter,1].y)), rotation);
             GameObject pieceFirst = Instantiate(firstRank[counter], new Vector3(((int)coords[counter,0].x),1, ((int)coords[counter,0].y)), rotation);
              
             pieceSecond.GetComponent<Renderer>().material.SetColor("_Color", Color.white );
             pieceFirst.GetComponent<Renderer>().material.SetColor("_Color", Color.white );
             pieceSecond.tag = "White";
             pieceFirst.tag = "White";
             counter++;
                    
        }
        counter = 0;
        while(counter <  board_x ){
             GameObject pieceSecond2 = Instantiate(pawn, new Vector3(((int)coords[counter,board_z - 2].x), 1, ((int)coords[counter,board_z - 2].y)), rotation);
             GameObject pieceFirst2 = Instantiate(firstRank_black[counter], new Vector3(((int)coords[counter,board_z - 1].x),1, ((int)coords[counter,board_z - 1].y)), rotation);
              
             pieceSecond2.GetComponent<Renderer>().material.SetColor("_Color", Color.black );
             pieceFirst2.GetComponent<Renderer>().material.SetColor("_Color", Color.black );
             pieceSecond2.tag = "Black";
             pieceFirst2.tag = "Black";
             counter++;
                    
        }


    }


    
    public void createGrid(){

        //creation of board saving coordinates
        for(int z = 0; z< board_z; z++){
            for(int x = 0; x < board_x; x++)
            {   
                GameObject newTile = Instantiate(tilePrefab, new Vector3(x,0,z), Quaternion.identity);
                
                
                if ((x+z)% 2 == 1) //switches betweem colours to create checkered effect
                    newTile.GetComponent<Renderer>().material.SetColor("_Color", tileColour1);
                    
                else
                    newTile.GetComponent<Renderer>().material.SetColor("_Color", tileColour2);
                

                
            }
        }
    }
    public Vector2[,] CreateCoordinateGrid()
    {
        Vector2[,] coordinates = new Vector2[board_x, board_z];

        for (int z = 0; z < board_z; z++)
        {
            for (int x = 0; x < board_x; x++)
            {
                coordinates[x, z] = new Vector2(x, z);
            }
        }

        return coordinates;
    }
    
    

    
}

