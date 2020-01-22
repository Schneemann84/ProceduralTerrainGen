using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class NewTerrainGeneration : MonoBehaviour
{

    private Mesh mesh;
    //public int xSize, zSize;
    [Range(0,6)]
    public int currentDetail;
    private int[] DetailLevels = new int[7] { 3, 5, 7, 9, 11, 13, 15 };
    public float outputDetailMultiplier;
    public Vector3[] vertices;

    int lastDetail = 0;

    System.Random rand = new System.Random();

    void Start()
    {
        GenerateVertices(currentDetail);
    }

    void GenerateVertices(int currentDetail)
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Mesh";

        float x2,z2;

        float DetailMultiplier =(float) 10 / (DetailLevels[currentDetail] - 1);

        outputDetailMultiplier = DetailMultiplier;

        vertices = new Vector3[(DetailLevels[currentDetail]) * (DetailLevels[currentDetail])];
        int i = 0;
        for (float z = 0; z < (DetailLevels[currentDetail]); z++) {
            for (float x = 0; x < (DetailLevels[currentDetail]); x++, i++) {
                if (x != 0){
                    x2 = x * DetailMultiplier;
                } else { x2 = 0;
                }
                if (z != 0) {
                    z2 = z * DetailMultiplier;
                } else { z2 = 0; 
                }
                vertices[i] = new Vector3(x2, GetHeight(), z2);
            }
        }
        mesh.vertices = vertices;
        GenerateTriangles(currentDetail);
    }

    void GenerateTriangles(int currentDetail)
    {
        int[] triangles = new int[(DetailLevels[currentDetail] - 1) * (DetailLevels[currentDetail] - 1) * 6];

        for (int z = 0, vert = 0, tri = 0; z < (DetailLevels[currentDetail] - 1); z++, vert++)
        {
            for (int x = 0; x < (DetailLevels[currentDetail] - 1); x++, tri += 6, vert++)
            {
                triangles[tri + 0] = vert;
                triangles[tri + 1] = triangles[tri + 4] = (DetailLevels[currentDetail] - 1) + vert + 1;
                triangles[tri + 2] = triangles[tri + 3] = vert + 1;
                triangles[tri + 5] = (DetailLevels[currentDetail] - 1) + vert + 2;
            }
        }
        mesh.triangles = triangles;
    }

    void Update()
    {
        if (DetailLevels[currentDetail] == lastDetail) { } else {
            GenerateVertices(currentDetail); }
        lastDetail = DetailLevels[currentDetail];
        //OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    float GetHeight()
    {
        return (rand.Next(0, 10) / 10f);
    }
}