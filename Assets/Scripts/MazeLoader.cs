using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	// public GameObject player;
	// public GameObject player2; //player2
	public float size = 2f;

	private MazeCell[,] mazeCells;

	// Use this for initialization
	void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

		//  GameObject Player = Random.Range(0,100)>Random.Range(0,100) ? Instantiate (player, new Vector3 (Random.Range(0,100)>Random.Range(0,100) ? -10 : 210, 3, Random.Range(-10,221) ), Quaternion.identity) as GameObject : Instantiate (player, new Vector3 (Random.Range(-10,221), 3, Random.Range(0,100)>Random.Range(0,100) ? -10 : 210 ), Quaternion.identity) as GameObject ;
		//  GameObject Player2 = Random.Range(0,100)>Random.Range(0,100) ? Instantiate (player2, new Vector3 (Random.Range(0,100)>Random.Range(0,100) ? -10 : 210, 3, Random.Range(-10,221) ), Quaternion.identity) as GameObject : Instantiate (player2, new Vector3 (Random.Range(-10,221), 3, Random.Range(0,100)>Random.Range(0,100) ? -10 : 210 ), Quaternion.identity) as GameObject ; //player2
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r =0 ; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				if(c != mazeColumns-1 )
				{
					if((c<0.40*mazeColumns || c>0.60*mazeColumns) || (r<0.40*mazeRows || r>0.60*mazeRows))
					{
				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size+3, 0, (c*size) + (size/2f)+3), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;
					}
				}

				if(r != mazeRows-1 )
				{
					if((c<0.40*mazeColumns || c>0.60*mazeColumns) || (r<0.40*mazeRows || r>0.60*mazeRows))
					{
				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f)+3, 0, c*size+3), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
					}
				}			
			}
		}
		
	}
}
