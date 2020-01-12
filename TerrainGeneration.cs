using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainGeneration : MonoBehaviour
{

    private Mesh mesh;
    private Mesh DetailedMesh;
    public int xSize, zSize, maxHeight;
    public Vector3[] vertices;

    private int[,] Chunks = new int[0,0];

    System.Random rand = new System.Random();

    void Start()
    {
        //GenerateHeights();

        Generate();
    }

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        //Initializes array of vertices equal to (xSize + 1) * (xSize + 1)
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //Nested for loop to add the vertices x,y,z values
        for (int i = 0, z = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; x++, i++) {
                vertices[i] = new Vector3(x , GetHeight(), z);
            }
        }
        //Adds vertices to mesh
        mesh.vertices = vertices;
        //Initializes array of the vertices that make up each triangle equal to xSize * zSize * 6
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

    /*void GenerateHeights()
    {
        int[] heights = new int[(xSize + 1) * (zSize + 1)];
        int numberOfVerts = 1;
        int currentHeight;
        bool increase = true;
        //loops equal to xSize * 2 : loops across the x axis then down the z axis
        for (int x = 0; x < xSize * 2; x++)
        {
            //loops equal to # of vertices diagnal to current x vertex
            for (int z = 0; z < numberOfVerts; z++)
            {
                heights[numberOfVerts + ((xSize - 1) * numberOfVerts)] = 0;//GetHeight();
            }
            if (numberOfVerts >= xSize + 1) {
                increase = false;
            }
            if (increase = true) {
                numberOfVerts++;
            } else {
                numberOfVerts--;
            }
        }
    }*/

    float GetHeight()
    {
        return (rand.Next(0, 20) / 10);
    }

    void UpdateChunk()
    {
       
    }

    void Update()
    {
        
    }
}
