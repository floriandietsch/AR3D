using UnityEngine;
using System.Collections;

public class TriangleMesh : MonoBehaviour {
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
			new Vector3(0,1,1),  //top
			new Vector3(-1,-1,1), //left bottom front
			new Vector3(1,-1,1)   //right bottom front
		};

		//Triangles
		int[] triangles = new int[]
		{
			//front 
			0,1,2//first triangle

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
