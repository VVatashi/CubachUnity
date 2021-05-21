using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Material material;

    public Block[,,] blocks = new Block[0, 0, 0];

    public void UpdateMesh()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.sharedMaterial = material;
        }

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uv = new List<Vector2>();
        var tris = new List<int>();

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                for (int k = 0; k < blocks.GetLength(2); k++)
                {
                    Block block = blocks[i, j, k];
                    if (!block.BlockType.Opaque)
                    {
                        continue;
                    }

                    var position = new Vector3(i, j, k);

                    if (i == 0 || i > 0 && !blocks[i - 1, j, k].BlockType.Opaque)
                    {
                        Block.GenBlockLeftVertices(block, position, vertices, normals, uv, tris);
                    }

                    if (i == blocks.GetLength(0) - 1 || i < blocks.GetLength(0) - 1 && !blocks[i + 1, j, k].BlockType.Opaque)
                    {
                        Block.GenBlockRightVertices(block, position, vertices, normals, uv, tris);
                    }

                    if (j == 0 || j > 0 && !blocks[i, j - 1, k].BlockType.Opaque)
                    {
                        Block.GenBlockBottomVertices(block, position, vertices, normals, uv, tris);
                    }

                    if (j == blocks.GetLength(1) - 1 || j < blocks.GetLength(1) - 1 && !blocks[i, j + 1, k].BlockType.Opaque)
                    {
                        Block.GenBlockTopVertices(block, position, vertices, normals, uv, tris);
                    }

                    if (k == 0 || k > 0 && !blocks[i, j, k - 1].BlockType.Opaque)
                    {
                        Block.GenBlockBackwardVertices(block, position, vertices, normals, uv, tris);
                    }

                    if (k == blocks.GetLength(2) - 1 || k < blocks.GetLength(2) - 1 && !blocks[i, j, k + 1].BlockType.Opaque)
                    {
                        Block.GenBlockForwardVertices(block, position, vertices, normals, uv, tris);
                    }
                }
            }
        }

        var mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        mesh.triangles = tris.ToArray();

        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh = mesh;
        }

        var meshCollider = GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            meshCollider.sharedMesh = mesh;
        }
    }
}
