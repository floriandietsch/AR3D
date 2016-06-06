using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	MeshFilter mf;
	Mesh mesh;
	
	public float speed;

	void Start () {
		mf = GetComponent<MeshFilter>();     //Grab the Mesh Filter
	}
	
	void Update () {
		if(Input.GetMouseButton(0)) {     //If the Left Mouse Button is down
			
				mesh = mf.mesh;     //Grab the current mesh from the Mesh Filter
				Mesh newMesh = new Mesh();     //Create a blank mesh to buil
				Vector3[] newVertices = mesh.vertices;     //Create an array of vertices for our new mesh and load in the vertices from the original mesh
				for(int i = 0;i < mesh.vertices.Length;i++) {     //Loop through every vertex
					newVertices[i] += new Vector3(Input.GetAxis("Mouse X") * newVertices[i].x * speed,0.0f,0.0f);     //Add (The mouse axis * the original vertex to maintain the sign and tell whether the vertex should go negative or positive on the x-axis * a speed value) to each vertex's x coordinate
				}
				newMesh.vertices = newVertices;     //Load in our new vertices
				newMesh.triangles = mesh.triangles;     //We don't need to change anything other than vertices, so the rest can be the same as the original mesh
				newMesh.uv = mesh.uv;
				newMesh.normals = mesh.normals;
				mf.mesh = newMesh;     //Finalize the mesh by loading it into the Mesh Filter
			
		}
	}
	
	void OnGUI() {
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"Left mouse button to extrude on x-axis\nRight mouse button to rotate");
	}
	
}

