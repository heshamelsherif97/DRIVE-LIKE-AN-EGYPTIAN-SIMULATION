using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCity : MonoBehaviour {
    public GameObject CrossRoads;
    public GameObject terrian;
    //int[] grid = new int[20]; 
    public int numberOfCells ;
    Vector3[] grid ;
	// Use this for initialization
	void Start () {
        int terrianCoolDown = 5;
        grid = new Vector3[numberOfCells];
        int turnCoolDown = 0;
        const int up = 0;
        const int left = 1;
        int curTurn = 0;
        grid[0] = new Vector3(0, 0, 0);
        Instantiate(terrian, new Vector3(-250,0,-250), Quaternion.Euler(0, 0, 0));
        Instantiate(CrossRoads, grid[0] , Quaternion.Euler(0, 0, 0));
        for (int i = 1; i< numberOfCells; i++)
        {
            turnCoolDown-- ;
            terrianCoolDown--;
            if (terrianCoolDown <= 0)
            {
                Instantiate(terrian, grid[i-1]+ new Vector3(-250, 0, -250), Quaternion.Euler(0, 0, 0));
                terrianCoolDown = 5;
            }
            if (turnCoolDown <= 0)
            {
                int turn = Random.RandomRange(0, 2);
                if(turn ==1)
                {
                    curTurn = 1 - curTurn;
                    turnCoolDown = 4;
                    
                }


                /*curTurn = 1 - curTurn;
                turnCoolDown = 4;
                print(curTurn);*/
            }
            grid[i] = grid[i - 1] + new Vector3(60 * (1 - curTurn),0, 60 * curTurn);
                Instantiate(CrossRoads, grid[i] , Quaternion.Euler(0, 0, 0));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
