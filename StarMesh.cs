using UnityEngine;
using System.Collections;

public class StarMesh : MonoBehaviour {
	MeshFilter mf;
	Mesh mesh;
	// Use this for initialization
	void Start () {

		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;

		//Vertices
		Vector3[] vertices = new Vector3[]
		{
			//front
			new Vector3(0,1,1), 
			new Vector3(0.5f,0,1),   
			new Vector3(1.5f,0,1),
			new Vector3(0.5f,-1,1),  
			new Vector3(1,-2.2f,1),   
			new Vector3(0,-1.5f,1),
			new Vector3(-1,-2.2f,1),  
			new Vector3(-0.5f,-1,1),   
			new Vector3(-1.5f,0,1), 
			new Vector3(-0.5f,0,1)   
		};

		//Triangles
		int[] triangles = new int[]
		{
			//front 
			0,9,1,
			1,3,2,
			3,5,4,
			5,7,6,
			7,9,8, // äußerer stern
			3,7,5,  // innerer stern
			3,1,7,
			9,7,1

		};

		//UVs
		Vector2[] uvs = new Vector2[]
		{
			//front, 0,0 bottom left, 1,1 top right
			new Vector2(0,1),
			new Vector2(0,0),
			new Vector2(1,1),
			new Vector2(1,0)
		};

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//	mesh.uv = uvs;
		mesh.Optimize();
		mesh.RecalculateNormals();


	}

	// Update is called once per frame
	void Update () {

	}
}
