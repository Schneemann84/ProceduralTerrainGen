using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainGeneration : MonoBehaviour
{

    private Mesh mesh;
    public int xSize, zSize, maxHeight;
    public float incrementMultiplier;
    public Vector3[] vertices;
    private int height;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        System.Random rand = new System.Random();

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; x++, i++) {
                height = rand.Next(0, 5);
                vertices[i] = new Vector3(x * incrementMultiplier, 0.1f * height, z * incrementMultiplier);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * zSize * 6];

        for (int z = 0, vert = 0, tri = 0; z < zSize; z++, vert++) {
            for (int x = 0; x < xSize; x++, tri += 6, vert++)
            {
                triangles[tri + 0] = vert;
                triangles[tri + 1] = triangles[tri + 4] = xSize + vert + 1;
                triangles[tri + 2] = triangles[tri + 3] = vert + 1;
                triangles[tri + 5] = xSize + vert + 2;
            }
        }
        mesh.triangles = triangles;
    }

    void Update()
    {
        
    }
}
