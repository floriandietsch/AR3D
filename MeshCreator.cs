using UnityEngine;
using System.Collections;

public class MeshCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        //Vertices
        Vector3[] vertices = new Vector3[]
        {
            //front
            new Vector3(-1,1,1),  //left top front
            new Vector3(1,1,1),   //right top front
            new Vector3(-1,-1,1), //left bottom front
            new Vector3(1,-1,1)   //right bottom front
        };

        //Triangles
        int[] triangles = new int[]
        {
            //front 
            0,2,3,//first triangle
            3,1,0//second triangle
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
        mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
