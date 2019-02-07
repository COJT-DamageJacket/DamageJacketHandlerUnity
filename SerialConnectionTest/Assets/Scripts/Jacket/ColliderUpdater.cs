using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUpdater : MonoBehaviour {

    MeshCollider meshCollider;
    SkinnedMeshRenderer skinMesh;
    Mesh bakeMesh;

	// Use this for initialization
	void Start () {
        Debug.Log("hogehoge");
        // SkinnedMeshRendererでできたvertexとMeshColliderのvertexを紐付け
        meshCollider = this.GetComponent<MeshCollider>();
        skinMesh = this.GetComponent<SkinnedMeshRenderer>();

        bakeMesh = new Mesh();
        skinMesh.BakeMesh(bakeMesh);
        meshCollider.sharedMesh = skinMesh.sharedMesh;

        meshCollider.enabled = true;
        meshCollider.convex = true;
        Debug.Log("hoge");
    }

    // Update is called once per frame
    void Update()
    {
        // MeshColliderのvertexを紐付けされたSkinnedMeshRendererのvertexに移動する
        skinMesh.BakeMesh(bakeMesh);
        // skinMesh.sharedMesh = bakeMesh;
        // meshCollider.sharedMesh = bakeMesh;
        // meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = bakeMesh;

        for (int i = 0; i < meshCollider.sharedMesh.vertices.Length; i++)
        {
            Debug.Log(i + " " + meshCollider.sharedMesh.vertices[i]);
        }
    }
}
