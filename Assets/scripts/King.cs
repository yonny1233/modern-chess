using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece 
{

    private tile onTile;
    public float fallSpeed;
    public bool isCollision;

    private tile[] allTiles;
    public List<tile> legalTiles;
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

    public override List<tile> legalPositions(){
        List<tile> legalTileArray = new List<tile>(); 
       
        return legalTileArray;
    }
    public override List<tile> legalAttacks(){
        List<tile> legalAttackArray = new List<tile>();
        return legalAttackArray;
        
    }
    public override List<tile> getLegalTiles(){
        return legalTiles;
    }

    public override List<tile> getLegalAttackTiles(){
        return legalAttackTiles;

    }

    
    
}
