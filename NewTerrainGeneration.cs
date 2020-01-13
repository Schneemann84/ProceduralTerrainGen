using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class NewTerrainGeneration : MonoBehaviour
{

    private Mesh mesh;
    //public int xSize, zSize;
    public int currentDetail;
    private int maxDetail = 6, minDetail = 0;
    public Vector3[] vertices;

    void Start()
    {
        GenerateVertices(currentDetail);
    }

    void GenerateVertices(int currentDetail)
    {
        int[] DetailLevels = new int[7]{3,5,7,9,11,13,15};
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Mesh";

        int DetailMultiplier = 11 / DetailLevels[currentDetail];

        vertices = new Vector3[(DetailLevels[currentDetail] + 1) * (DetailLevels[currentDetail] + 1)];
        for(int z = 0, i = 0; z < DetailLevels[currentDetail]; z++) {
            for(int x = 0; x < DetailLevels[currentDetail]; x++, i++) {
                vertices[i] = new Vector3(x / DetailMultiplier , 0, z / DetailMultiplier);
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}