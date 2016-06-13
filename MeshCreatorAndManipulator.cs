using UnityEngine;
using System.Collections;

public class MeshCreatorAndManipulator : MonoBehaviour {
	MeshFilter mf;
	Mesh mesh;
	Vector3[] vertices;
	int[] triangles;
	float count = 0.1f;
	// Use this for initialization
	void Start () {

		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;

		//Vertices
		vertices = new Vector3[]
		{
			//Grundmuster
			new Vector3(-1,1,1),  //left top front
			new Vector3(1,1,1),   //right top front
			new Vector3(-1,-1,1), //left bottom front
			new Vector3(1,-1,1)   //right bottom front
		};

		//Triangles
		triangles = new int[]
		{
			//Grundfläche
			0,2,3,//first triangle
			3,1,0//second triangle
		};


		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();


	}
	void AddNewVertices() {
		
		Vector3[] NewVertices = new Vector3[vertices.Length + 4];
		System.Array.Resize (ref vertices, NewVertices.Length);
		System.Array.Copy (vertices, NewVertices, NewVertices.Length);

		NewVertices [vertices.Length-4] = new Vector3 (-1, 1, 1 + count);
		NewVertices [vertices.Length-3] = new Vector3 (1, 1, 1 + count);
		NewVertices [vertices.Length-2] = new Vector3 (-1, -1, 1 + count);
		NewVertices [vertices.Length-1] = new Vector3 (1, -1, 1 + count);


		System.Array.Copy (NewVertices, vertices, vertices.Length);
		mesh.vertices = vertices;
		count += 0.1f;
	}
	void DefineTriangles (){
		
		int[] newTriangles = new int[triangles.Length + 24];
		System.Array.Resize (ref triangles, newTriangles.Length);
		System.Array.Copy (triangles, newTriangles, newTriangles.Length);
		int m = vertices.Length ; //number of all vertices
		int n = 4; //number of added vertices

		newTriangles[triangles.Length-24] = m-2*n;
		newTriangles[triangles.Length-23] = m - n;
		newTriangles[triangles.Length-22] = m - n + 1;
		newTriangles[triangles.Length-21] = m-2*n+1;
		newTriangles[triangles.Length-20] = m - n + 1;
		newTriangles[triangles.Length-19] = m - 1;
		newTriangles[triangles.Length-18] = m-2*n+2; 
		newTriangles[triangles.Length-17] = m-n+2; 
		newTriangles[triangles.Length-16] = m-n;
		newTriangles[triangles.Length-15] = m-n-1; 
		newTriangles[triangles.Length-14] = m-1; 
		newTriangles[triangles.Length-13] = m-n+2;
		newTriangles[triangles.Length-12] = m-n;
		newTriangles[triangles.Length-11] = m-2*n; 
		newTriangles[triangles.Length-10] = m-2*n+2;
		newTriangles[triangles.Length-9] = m - n + 1; 
		newTriangles[triangles.Length-8] = m - 2 * n + 1;
		newTriangles[triangles.Length-7] = m-2*n;
		newTriangles[triangles.Length-6] = m - n + 2;
		newTriangles[triangles.Length-5] = m - 2 * n + 2; 
		newTriangles[triangles.Length-4] = m - n - 1;
		newTriangles[triangles.Length-3] = m - 1; 
		newTriangles[triangles.Length-2] = m - n - 1; 
		newTriangles[triangles.Length-1]   = m - 2 * n + 1;


		System.Array.Copy (newTriangles, triangles, triangles.Length);

		mesh.triangles = triangles;
			// m-2n, m-n, m-n+1
		 	// m-2n+1, m-n+1, m-1
			// m-2n+2, m-n+2, m-n
		 	// m-n-1, m-1, m-n+2
		 	// m-n, m-2n, m-2n+2
		 	// m-n+1, m-2n+1, m-2n
		 	// m-n+2, m-2n+2, m-n-1
		  	// m-1, m-n-1, m-2n+1

	}
	// Update is called once per frame
	void Update () {
		print (vertices.Length);
		AddNewVertices ();
				// call after new vertices have been set
		DefineTriangles ();

	}
}