using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class CreatePolygonMesh : MonoBehaviour
    {
        private Mesh mesh;

        [SerializeField]
        Vector3 vertex1 = new Vector3(-2.0f, 0.0f, 4.0f);
        [SerializeField]
        Vector3 vertex2 = new Vector3(-2.0f, 0.0f, -2.0f);
        [SerializeField]
        Vector3 vertex3 = new Vector3(2.0f, 0.0f, -2.0f);

        // Start is called before the first frame update
        void Start()
        {
            mesh = new Mesh();
            Vector3[] newVertices = new Vector3[3];
            Vector2[] newUV = new Vector2[3];
            int[] newTriangles = new int[3];

            // 頂点座標の指定.
            newVertices[0] = vertex1;
            newVertices[1] = vertex2;
            newVertices[2] = vertex3;

            // UVの指定 (頂点数と同じ数を指定すること).
            newUV[0] = new Vector2(0.0f, 0.0f);
            newUV[1] = new Vector2(0.0f, 1.0f);
            newUV[2] = new Vector2(1.0f, 1.0f);
            //newUV[3] = new Vector2(1.0f, 0.0f);

            // 三角形ごとの頂点インデックスを指定.
            newTriangles[0] = 2;
            newTriangles[1] = 1;
            newTriangles[2] = 0;

            mesh.vertices = newVertices;
            mesh.uv = newUV;
            mesh.triangles = newTriangles;

            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.RecalculateBounds();

            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshFilter>().sharedMesh.name = "myMesh";
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
