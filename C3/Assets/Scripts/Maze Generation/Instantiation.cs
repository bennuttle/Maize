using UnityEngine;
using System.Collections;
using Maize;

public class Instantiation : MonoBehaviour {
	public Transform floor;
	public Transform front;
	public Transform back;
	public Transform ceiling;
	public Transform right;
	public Transform left;
	public Transform EndBox;

	public int xsize = 2;
	public int ysize = 2;
	public int zsize = 2;
	public GameObject characterRef;

	void Start () {
//		createMaze ();
	}

	public void createMaze(int xsize,int ysize,int zsize ) {
		MazeGraph testthis = new MazeGraph (xsize,ysize,zsize);	
		MazeNode [, ,] temp = testthis.getAllNodes();
		MazeNode mazeGoal = testthis.goal;
		for (int x = 0; x < xsize; x++)
		{
			for (int y = 0; y < ysize; y++)
			{
				for (int z = 0; z < zsize; z++)
				{
					//					MazeNode[,,] temp = testthis.graph[x,y,z];
					if(temp[x,y,z].y_minus == null) {
						Instantiate(floor,new Vector3 (x*10, y*10, z*10), floor.transform.rotation);
						//						floor.transform.position = new Vector3 (x*10, y*10, z*10);
					} else { /*print("floor : x="+x+", y="+y+", z="+z);*/}
					if(temp[x,y,z].y_plus == null) {
						Instantiate(ceiling,new Vector3 (x*10,y*10 + 10, z*10),ceiling.transform.rotation);
						//ceiling.transform.position = new Vector3 (x*10,y*10 + 10, z*10);
					} else { /*print("ceiling : x="+x+", y="+y+", z="+z);*/}
					if(temp[x,y,z].z_plus == null) {
						Instantiate(front, new Vector3 (x*10, y*10 + 5,z*10 +  5), front.transform.rotation);
						//						front.transform.position = new Vector3 (x*10, y*10 + 5,z*10 +  5);
					} else { /*print("front : x="+x+", y="+y+", z="+z);*/} 
					if(temp[x,y,z].z_minus == null) {
						Instantiate(back,new Vector3 (x*10, y*10 + 5,z*10 - 5),back.transform.rotation);
						//						back.transform.position = new Vector3 (x*10, y*10 + 5,z*10 - 5);
					} else { /*print("back : x="+x+", y="+y+", z="+z);*/}
					if(temp[x,y,z].x_plus == null) {
						Instantiate(right, new Vector3 (x*10 + 5,y*10 + 5, z*10), right.transform.rotation);
						//						right.transform.position = new Vector3 (x*10 + 5,y*10 + 5, z*10);
					} else { /*print("right : x="+x+", y="+y+", z="+z);*/}
					if(temp[x,y,z].x_minus == null) {
						Instantiate(left, new Vector3 (x*10 - 5,y*10 + 5, z*10), left.transform.rotation);
						//						left.transform.position = new Vector3 (x*10 - 5,y*10 + 5, z*10);
					} else { /*print("left : x="+x+", y="+y+", z="+z);*/}
				}
			}
		}
		Instantiate (EndBox, new Vector3 ((mazeGoal.x_loc) * 10, (mazeGoal.y_loc) * 10 + 5, (mazeGoal.z_loc) * 10), EndBox.transform.rotation);
		Instantiate (characterRef);
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void setX(int xVal) {
		xsize = xVal;
	}

	public void setY(int yVal) {
		ysize = yVal;
	}

	public void setZ(int zVal) {
		zsize = zVal;
	}
}
