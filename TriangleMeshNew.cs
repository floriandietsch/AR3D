using UnityEngine;
using System.Collections;

public class TriangleMesh : MonoBehaviour {
	MeshFilter mf;
	Mesh mesh;
	Vector3[] vertices;
	Vector3[] trianglePattern;
	Vector3[] normals;
	Vector3[] side1 = new Vector3[1];
	Vector3[] side2 = new Vector3[1];
	Vector3[] side3 = new Vector3[1];
	Vector3[] side4 = new Vector3[1];
	Vector3[] side5 = new Vector3[1];
	Vector3[] side6 = new Vector3[1];
	int[] triangles;
	float count = 0.1f;
	float normalx1;
	float normaly1;
	float normalz1;
	float normalx2;
	float normaly2;
	float normalz2;
	float normalx3;
	float normaly3;
	float normalz3;
	// Use this for initialization
	void Start () {

		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;

		//Vertices
		trianglePattern = new Vector3[] {

			//Grundmuster
			new Vector3(0,1,1),  
			new Vector3(-1,-1,1), 
			new Vector3(1,-1,1),
			new Vector3(0,1,1),
			new Vector3(-1,-1,1),
			new Vector3(1,-1,1)
		};

		//Triangles
		triangles = new int[]
		{
			//front 
			0,1,2//first triangle

		};

		//Normals
		normals = mesh.normals;

		//zu extrudierendes Pattern nach vertices kopieren
		vertices = new Vector3[trianglePattern.Length];
		System.Array.Copy (trianglePattern, vertices, trianglePattern.Length);

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		//	mesh.uv = uvs;
		mesh.Optimize();
		mesh.RecalculateNormals();


	}

	void AddNewVertices() {

		Vector3[] NewVertices = new Vector3[vertices.Length + 6];
		System.Array.Resize (ref vertices, NewVertices.Length);
		System.Array.Resize (ref normals, vertices.Length);
		System.Array.Copy (vertices, NewVertices, NewVertices.Length);

		NewVertices [vertices.Length-6] = new Vector3 (0, 1, 1 + count);	//1. neuer Punkt
		NewVertices [vertices.Length-5] = new Vector3 (-1, -1, 1 + count);	//2. neuer Punkt
		NewVertices [vertices.Length-4] = new Vector3 (1, -1, 1 + count);	//3. neuer Punkt
		NewVertices [vertices.Length-3] = new Vector3 (0, 1, 1 + count);	//1. doppelter Punkt
		NewVertices [vertices.Length-2] = new Vector3 (-1, -1, 1 + count);	//2. doppelter Punkt
		NewVertices [vertices.Length-1] = new Vector3 (1, -1, 1 + count);	//3. doppelter Punkt

		//Seiten für die Normalenberechnung
		side1[0] = NewVertices [vertices.Length - 11] - NewVertices[vertices.Length - 12];
		side2[0] = NewVertices [vertices.Length - 6] - NewVertices[vertices.Length - 12];
		side3[0] = NewVertices [vertices.Length - 10] - NewVertices[vertices.Length - 12];
		side4[0] = NewVertices [vertices.Length - 6] - NewVertices[vertices.Length - 12];
		side5[0] = NewVertices [vertices.Length - 10] - NewVertices[vertices.Length - 11];
		side6[0] = NewVertices [vertices.Length - 5] - NewVertices[vertices.Length - 11];

		//Kreuzprodukt
		normalx1 = (side1 [0].y * side2 [0].z) - (side1 [0].z - side2 [0].y);
		normaly1 = (side1 [0].z * side2 [0].x) - (side1 [0].x - side2 [0].z);
		normalz1 = (side1 [0].x * side2 [0].y) - (side1 [0].y - side2 [0].x);

		normalx2 = (side3 [0].y * side4 [0].z) - (side3 [0].z - side4 [0].y);
		normaly2 = (side3 [0].z * side4 [0].x) - (side3 [0].x - side4 [0].z);
		normalz2 = (side3 [0].x * side4 [0].y) - (side3 [0].y - side4 [0].x);

		normalx3 = (side5 [0].y * side6 [0].z) - (side5 [0].z - side6 [0].y);
		normaly3 = (side5 [0].z * side6 [0].x) - (side5 [0].x - side6 [0].z);
		normalz3 = (side5 [0].x * side6 [0].y) - (side5 [0].y - side6 [0].x);

		//vertices.Length - 12 ist der start des normals array
		normals[vertices.Length-12] = new Vector3 (normalx1, normaly1, normalz1 ); 
		normals[vertices.Length-11] = new Vector3 (normalx1, normaly1, normalz1 ); 
		normals[vertices.Length-10] = new Vector3 (normalx2, normaly2, normalz2 ); 
		normals[vertices.Length-9] = new Vector3 (normalx2, normaly2, normalz2 );
		normals[vertices.Length-8] = new Vector3 (normalx3, normaly3, normalz3 );
		normals[vertices.Length-7] = new Vector3 (normalx3, normaly3, normalz3 );
		normals[vertices.Length-6] = new Vector3 (normalx1, normaly1, normalz1 );
		normals[vertices.Length-5] = new Vector3 (normalx1, normaly1, normalz1 );
		normals[vertices.Length-4] = new Vector3 (normalx2, normaly2, normalz2 );
		normals[vertices.Length-3] = new Vector3 (normalx2, normaly2, normalz2 );
		normals[vertices.Length-2] = new Vector3 (normalx3, normaly3, normalz3 );
		normals[vertices.Length-1] = new Vector3 (normalx3, normaly3, normalz3 );

		System.Array.Copy (NewVertices, vertices, vertices.Length);
		mesh.vertices = vertices;
		mesh.normals = normals;
		count += 0.1f;
	}

	void DefineTriangles (){

		int[] newTriangles = new int[triangles.Length + 18];
		System.Array.Resize (ref triangles, newTriangles.Length);
		System.Array.Copy (triangles, newTriangles, newTriangles.Length);
		int m = vertices.Length/2 ; //number of all vertices
		int n = 3; //number of added vertices

		newTriangles[triangles.Length-18] = m - 2*n ;		
		newTriangles[triangles.Length-17] = m - n; 	
		newTriangles[triangles.Length-16] = m - 1;		
		newTriangles[triangles.Length-15] = m - 1; 		
		newTriangles[triangles.Length-14] = m - 2*n+2; 	
		newTriangles[triangles.Length-13] = m -2*n; 
		newTriangles[triangles.Length-12] = m-2*n+2;			
		newTriangles[triangles.Length-11] = m-1; 			
		newTriangles[triangles.Length-10] = m-2;		
		newTriangles[triangles.Length- 9] = m - 2; 		
		newTriangles[triangles.Length- 8] = m - 2 * n +1;	
		newTriangles[triangles.Length- 7] = m-2*n+2;			
		newTriangles[triangles.Length- 6] = m - 2*n+1;		
		newTriangles[triangles.Length- 5] = m - 2; 	
		newTriangles[triangles.Length- 4] = m - n;		
		newTriangles[triangles.Length- 3] = m - n; 		
		newTriangles[triangles.Length- 2] = m - 2*n; 	
		newTriangles[triangles.Length- 1]   = m -2*n +1; 


		System.Array.Copy (newTriangles, triangles, triangles.Length);

		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();

	}

	// Update is called once per frame
	void Update () {
		print (vertices.Length + "vertices");
		print (normals.Length + "normals");
		AddNewVertices ();
		DefineTriangles ();

	}
}