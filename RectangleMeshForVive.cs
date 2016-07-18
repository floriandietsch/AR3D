using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RectangleMeshForVive :  NetworkBehaviour {

	[SyncVar(hook = "addNewVertexPosition")]
	Vector3 newPosition;

	[SyncVar(hook = "addNewVertexRotation")]
	Quaternion vertexRotation;

	MeshFilter mf;
	Mesh mesh;
	Vector3[] vertices;
	Vector3[] rectanglePattern;
	Vector3[] trianglePattern;
	Vector3[] starPattern;
	Vector3[] normals;
	Vector3[] side1 = new Vector3[1];
	Vector3[] side2 = new Vector3[1];
	float normalx;
	float normaly;
	float normalz;
	int[] triangles;
	Vector3[] NewVertices;
	// Use this for initialization
	void Start () {

		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;

		starPattern = new Vector3[] {

			//Grundmuster
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

		trianglePattern = new Vector3[] {

			//Grundmuster
			new Vector3(0,1,1),  
			new Vector3(-1,-1,1), 
			new Vector3(1,-1,1)   
		};

		rectanglePattern = new Vector3[]{

			//Grundmuster
			new Vector3(-1,1,1),  //left top front
			new Vector3(1,1,1),   //right top front
			new Vector3(-1,-1,1), //left bottom front
			new Vector3(1,-1,1)   //right bottom front
		};




		//zu extrudierendes Pattern nach vertices kopieren
		vertices = new Vector3[rectanglePattern.Length];
		System.Array.Copy (rectanglePattern, vertices, rectanglePattern.Length);


		//Triangles
		triangles = new int[]
		{
			//Grundfläche Rechteck
			0,2,3,//first triangle
			3,1,0//second triangle
		};


		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();


	}

	void addNewVertexPosition(Vector3 newPosition){
		
		Vector3[] NewVertices = new Vector3[vertices.Length + 4];
		System.Array.Resize (ref vertices, NewVertices.Length);
		System.Array.Resize (ref normals, vertices.Length);
		System.Array.Copy (vertices, NewVertices, NewVertices.Length);

		NewVertices [vertices.Length - 4] = rectanglePattern [0] + newPosition;
		NewVertices [vertices.Length - 3] = rectanglePattern [1] + newPosition;
		NewVertices [vertices.Length - 2] = rectanglePattern [2] + newPosition;
		NewVertices [vertices.Length - 1] = rectanglePattern [3] + newPosition;



		System.Array.Copy (NewVertices, vertices, vertices.Length);
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();
		DefineTriangles ();
	}

	void addNewVertexRotation(Quaternion vertexRotation){
		
		NewVertices [vertices.Length - 4] = vertexRotation * NewVertices [vertices.Length - 4];
		NewVertices [vertices.Length - 3] = vertexRotation * NewVertices [vertices.Length - 3];
		NewVertices [vertices.Length - 2] = vertexRotation * NewVertices [vertices.Length - 2];
		NewVertices [vertices.Length - 1] = vertexRotation * NewVertices [vertices.Length - 1];
	}

	void DefineTriangles (){

		int[] newTriangles = new int[triangles.Length + 24];
		System.Array.Resize (ref triangles, newTriangles.Length);
		System.Array.Copy (triangles, newTriangles, newTriangles.Length);
		int m = vertices.Length ; //number of all vertices
		int n = 4; //number of added vertices

		newTriangles[triangles.Length-24] = m-2*n;          //0    // m-2*n+x   9mal
		newTriangles[triangles.Length-23] = m - n;			//4	// m-n+x     13mal
		newTriangles[triangles.Length-22] = m - n + 1;		//5
		newTriangles[triangles.Length-21] = m-2*n+1;		//1
		newTriangles[triangles.Length-20] = m - n + 1;		//5
		newTriangles[triangles.Length-19] = m - 1;			//7
		newTriangles[triangles.Length-18] = m-2*n+2; 		//2
		newTriangles[triangles.Length-17] = m-n+2; 			//6
		newTriangles[triangles.Length-16] = m-n;			//4
		newTriangles[triangles.Length-15] = m-n-1; 			//3
		newTriangles[triangles.Length-14] = m-1; 			//7
		newTriangles[triangles.Length-13] = m-n+2;			//6
		newTriangles[triangles.Length-12] = m-n;			//4
		newTriangles[triangles.Length-11] = m-2*n; 			//0
		newTriangles[triangles.Length-10] = m-2*n+2;		//2
		newTriangles[triangles.Length-9] = m - n + 1; 		//5
		newTriangles[triangles.Length-8] = m - 2 * n + 1;	//1
		newTriangles[triangles.Length-7] = m-2*n;			//0
		newTriangles[triangles.Length-6] = m - n + 2;		//6
		newTriangles[triangles.Length-5] = m - 2 * n + 2; 	//2
		newTriangles[triangles.Length-4] = m - n - 1;		//3
		newTriangles[triangles.Length-3] = m - 1; 			//7
		newTriangles[triangles.Length-2] = m - n - 1; 		//3
		newTriangles[triangles.Length-1]   = m -2*n + 1;  	//1


		System.Array.Copy (newTriangles, triangles, triangles.Length);

		mesh.triangles = triangles;

		//mesh.RecalculateNormals ();
		mesh.Optimize ();
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
		

	}
}