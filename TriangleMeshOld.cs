using UnityEngine;
using System.Collections;

public class TriangleMeshOld : MonoBehaviour {
	MeshFilter mf;
	Mesh mesh;
	Vector3[] vertices;
	Vector3[] trianglePattern;
	Vector3[] normals;
	Vector3[] side1 = new Vector3[1];
	Vector3[] side2 = new Vector3[1];
	int[] triangles;
	int count = 0;
	float addz = 0.0f;
	float addx = 0.0f;
	float addy = 0.0f;
	bool Durchlauf = false;
	float normalx;
	float normaly;
	float normalz;
	// Use this for initialization
	void Start () {

		mf = GetComponent<MeshFilter>();
		mesh = mf.mesh;

		//Vertices
		trianglePattern = new Vector3[] {

			//Grundmuster
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

		Vector3[] NewVertices = new Vector3[vertices.Length + 3];
		System.Array.Resize (ref vertices, NewVertices.Length);
		System.Array.Resize (ref normals, vertices.Length);
		System.Array.Copy (vertices, NewVertices, NewVertices.Length);
		if (Durchlauf == false) {
			addx += 0.1f;
			addy += 0.1f;
		} else {
			addx -= 0.1f;
			addy -= 0.1f;
		}


		NewVertices [vertices.Length-3] = new Vector3 (0+addx, 1+addy, 1+addz );		// add vive.x, vive.y, vive.z
		NewVertices [vertices.Length-2] = new Vector3 (-1+addx, -1+addy, 1+addz );		// add vive.x, vive.y, vive.z
		NewVertices [vertices.Length-1] = new Vector3 (1+addx, -1+addy, 1+addz );		// add vive.x, vive.y, vive.z
		// die 3 Vektoren mit nem Quaternion der vive transformieren 

		//Seiten für die Normalenberechnung
		side1[0] = NewVertices [vertices.Length - 6] - NewVertices[vertices.Length - 3];
		side2[0] = NewVertices [vertices.Length - 2] - NewVertices[vertices.Length - 3];

		//Kreuzprodukt
		normalx = (side1 [0].y * side2 [0].z) - (side1 [0].z - side2 [0].y);
		normaly = (side1 [0].z * side2 [0].x) - (side1 [0].x - side2 [0].z);
		normalz = (side1 [0].x * side2 [0].y) - (side1 [0].y - side2 [0].x);

		//vertices.Length - 6 ist der start des normals array
		normals[vertices.Length-6] = new Vector3 (normalx, normaly, normalz ); 
		normals[vertices.Length-5] = new Vector3 (normalx, normaly, normalz ); 
		normals[vertices.Length-4] = new Vector3 (normalx, normaly, normalz ); 

		System.Array.Copy (NewVertices, vertices, vertices.Length);
		mesh.vertices = vertices;
	//	mesh.normals = normals;
		addz += 0.1f;

		
	
	}

	void DefineTriangles (){

		int[] newTriangles = new int[triangles.Length + 18];
		System.Array.Resize (ref triangles, newTriangles.Length);
		System.Array.Copy (triangles, newTriangles, newTriangles.Length);
		int m = vertices.Length ; //number of all vertices
		int n = 3; //number of added vertices
					
		newTriangles[triangles.Length-18] = m - 2*n ;		//referenziert auf einen Index im Vertices Array
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
		AddNewVertices ();
		DefineTriangles ();

		if (count == 40) {
			count = 0;
			if (Durchlauf == false) {
				Durchlauf = true;
			} else
				Durchlauf = false;
		}
		count++;
	}
}
