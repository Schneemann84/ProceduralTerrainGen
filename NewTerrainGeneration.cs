using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class NewTerrainGeneration : MonoBehaviour
{

    private Mesh mesh;
    //public int xSize, zSize;
    [Range(0,4)]
    public int currentDetail;
    public float dis;
    private int[] DetailLevels = new int[5] { 3, 5, 9, 17, 33};
    public int worldChunksX,worldChunksZ;
    public float outputDetailMultiplier;
    public Vector3[] vertices;

    int lastDetail = 0;

    System.Random rand = new System.Random();

    void Start()
    {
        GenerateChunk(currentDetail);
    }

    public void GenerateChunk(int worldChunksX, int worldChunksZ, int currentDetail)
    {
        GenerateVertices(currentDetail);
        GenerateTriangles(currentDetail);
    }

    private void GenerateVertices(int currentDetail)
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
                vertices[i] = new Vector3(x2, Mathf.PerlinNoise(GetHeight(),GetHeight()), z2);
            }
        }
        mesh.vertices = vertices;
        //GenerateTriangles(currentDetail);
    }

    private void GenerateTriangles(int currentDetail)
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

    private void GenerateWorldHeights()
    {

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.Find("NewTerrainTest").transform.position, GameObject.Find("Main Camera").transform.position);
        dis = distance;
        if (distance <= 10)
        {
            currentDetail = 4;
        }
        if (distance > 10 && distance <= 15)
        {
            currentDetail = 3;
        }
        if (distance > 15 && distance <= 25)
        {
            currentDetail = 2;
        }
        if (distance > 25 && distance <= 40)
        {
            currentDetail = 1;
        }
        if (distance > 40)
        {
            currentDetail = 0;
        }

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