using UnityEngine;
using System.Collections;

public class RectangleMesh : MonoBehaviour {
	MeshFilter mf;
	Mesh mesh;


	// Use this for initialization
	void Start () {

//		mf = GetComponent<MeshFilter>();
//		mesh = mf.mesh;
//
//		//Vertices
//
//		Vector3[] GrundmusterVertices = new Vector3[]
//		{
//			//front
//			new Vector3(-1,1,1),  //left top front, x,y,z
//			new Vector3(1,1,1),   //right top front
//			new Vector3(-1,-1,1), //left bottom front
//			new Vector3(1,-1,1)   //right bottom front
//		};
//		//Triangles
//		int[] triangles = new int[]
//		{
//			//front 
//			0,2,3,//first triangle
//			3,1,0//second triangle
//		};
//
//
//		mesh.Clear();
//		mesh.vertices = GrundmusterVertices;
//		mesh.triangles = triangles;
//		mesh.Optimize();
//		mesh.RecalculateNormals();



	//	mesh.uv = uvs;


	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) ) {
			
			mf = GetComponent<MeshFilter>();
			mesh = mf.mesh;
			var pos = Input.mousePosition;
			pos.z = 1;
			pos = Camera.main.ScreenToWorldPoint (pos);

			Vector3[] GrundmusterVertices = new Vector3[] {
				new Vector3 (pos.x - 1, pos.y + 1, 1),
				new Vector3 (pos.x + 1, pos.y + 1, 1),
				new Vector3 (pos.x - 1, pos.y - 1, 1),
				new Vector3 (pos.x + 1, pos.y - 1, 1),

				new Vector3 (pos.x - 1, pos.y + 1, 2),
				new Vector3 (pos.x + 1, pos.y + 1, 2),
				new Vector3 (pos.x - 1, pos.y - 1, 2),
				new Vector3 (pos.x + 1, pos.y - 1, 2)

		
			};
			print (pos.x);

			int[] triangles = new int[]
			{
				//front 
				0,2,3,//first triangle
				3,1,0,//second triangle

				0,4,5,  // m-2n, m-n, m-n+1
				1,5,7,  // m-2n+1, m-n+1, m-1
				2,6,4,	// m-2n+2, m-n+2, m-n
				3,7,6,  // m-n-1, m-1, m-n+2
				4,0,2,  // m-n, m-2n, m-2n+2
				5,1,0,  // m-n+1, m-2n+1, m-2n
				6,2,3,  // m-n+2, m-2n+2, m-n-1
				7,3,1,  // m-1, m-n-1, m-2n+1
		

				4,6,7, //Abschluss
				7,5,4
			};
			mesh.Clear();
			mesh.vertices = GrundmusterVertices;
			mesh.triangles = triangles;
			mesh.Optimize();
			mesh.RecalculateNormals();

}
	}
}
