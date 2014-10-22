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
	private MazeGraph testthis;
	private MazeNode[, ,] temp; 
	public int size = 2;

	void Start () {
		this.testthis = new MazeGraph (size);	
		this.temp = testthis.graph;
		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				for (int z = 0; z < size; z++)
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
		Instantiate (EndBox, new Vector3 ((size - 1) * 10, (size - 1) * 10 + 5, (size - 1) * 10), EndBox.transform.rotation);
	}

	// Update is called once per frame
	void Update () {
		
	}
	public MazeNode[, ,] getGraph() {
		return temp;
	}
}
