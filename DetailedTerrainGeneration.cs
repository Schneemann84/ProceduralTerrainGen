using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DetailedTerrainGeneration : MonoBehaviour
{
    private Mesh DetailedMesh;
    public int xSize, zSize, maxHeight;
    public Vector3[] detailedVertices;

    private int[,] Chunks = new int[0, 0];

    System.Random rand = new System.Random();

    void Start()
    {
        DetailedGeneration();
    }

    void DetailedGeneration()
    {
        GetComponent<MeshFilter>().mesh = DetailedMesh = new Mesh();
        DetailedMesh.name = "Detailed Procedural Grid";
        detailedVertices = new Vector3[((xSize + 1) * (zSize + 1)) + (xSize * zSize)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++, i++)
            {
                detailedVertices[i] = new Vector3(x, 0, z);
            }
        }
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize - 1; x++, i++)
            {
                detailedVertices[((xSize + 1) * (zSize + 1)) + i] = new Vector3(x + 0.5f , 0, z + 0.5f);
            }
        }

        DetailedMesh.vertices = detailedVertices;

        int[] triangles = new int[xSize * zSize * 12];

        for (int z = 0, vert = 0, tri = 0 ; z < zSize; z++, vert++)
        {
            for (int x = 0; x < xSize; x++, tri += 12, vert++)
            {
                triangles[tri + 0] = triangles[tri + 3] = vert;
                triangles[tri + 1] = triangles[tri + 4] = triangles[tri + 7] = triangles[tri + 10] = ((xSize + 1) * (zSize + 1)) + vert;
                triangles[tri + 2] = triangles[tri + 11] = vert + 1;
                triangles[tri + 5] = triangles[tri + 6] = xSize + vert;
                triangles[tri + 8] = triangles[tri + 9] = xSize + vert + 1;
            }
        }

        DetailedMesh.triangles = triangles;

    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < detailedVertices.Length; i++)
        {
            Gizmos.DrawSphere(detailedVertices[i], 0.1f);
        }
    }

    float GetHeight()
    {
        return (rand.Next(0, 20) / 10);
    }

    void Update()
    {
        
    }
}
