using System.Collections.Generic;
using UnityEngine;

public struct Block
{
    private const int TILESET_WIDTH = 16;
    private const int TILESET_HEIGHT = 16;

    public int BlockTypeId;

    public Block(int blockTypeId)
    {
        BlockTypeId = blockTypeId;
    }

    public BlockType BlockType
    {
        get
        {
            return BlockType.BlockTypes[BlockTypeId];
        }
    }

    public static void GenBlockRightVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.SideTextureId % TILESET_WIDTH;
        int v = (block.BlockType.SideTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(1, 0, 0),
            position + new Vector3(1, 0, 1),
            position + new Vector3(1, 1, 0),
            position + new Vector3(1, 1, 1),
        });

        normals.AddRange(new[] {
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
        });

        uv.AddRange(new[] {
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMin),
            new Vector2(uMax, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 2, verticesCount + 1,
            verticesCount + 2, verticesCount + 3, verticesCount + 1,
        });
    }

    public static void GenBlockLeftVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.SideTextureId % TILESET_WIDTH;
        int v = (block.BlockType.SideTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(0, 0, 0),
            position + new Vector3(0, 0, 1),
            position + new Vector3(0, 1, 0),
            position + new Vector3(0, 1, 1),
        });

        normals.AddRange(new[] {
            -Vector3.left,
            -Vector3.left,
            -Vector3.left,
            -Vector3.left,
        });

        uv.AddRange(new[] {
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMin),
            new Vector2(uMin, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 1, verticesCount + 2,
            verticesCount + 2, verticesCount + 1, verticesCount + 3,
        });
    }

    public static void GenBlockTopVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.TopTextureId % TILESET_WIDTH;
        int v = (block.BlockType.TopTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(0, 1, 0),
            position + new Vector3(1, 1, 0),
            position + new Vector3(0, 1, 1),
            position + new Vector3(1, 1, 1),
        });

        normals.AddRange(new[] {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
        });

        uv.AddRange(new[] {
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMin),
            new Vector2(uMax, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 2, verticesCount + 1,
            verticesCount + 2, verticesCount + 3, verticesCount + 1,
        });
    }

    public static void GenBlockBottomVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.BottomTextureId % TILESET_WIDTH;
        int v = (block.BlockType.BottomTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(0, 0, 0),
            position + new Vector3(1, 0, 0),
            position + new Vector3(0, 0, 1),
            position + new Vector3(1, 0, 1),
        });

        normals.AddRange(new[] {
            -Vector3.up,
            -Vector3.up,
            -Vector3.up,
            -Vector3.up,
        });

        uv.AddRange(new[] {
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMin),
            new Vector2(uMin, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 1, verticesCount + 2,
            verticesCount + 2, verticesCount + 1, verticesCount + 3,
        });
    }

    public static void GenBlockForwardVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.SideTextureId % TILESET_WIDTH;
        int v = (block.BlockType.SideTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(0, 0, 1),
            position + new Vector3(1, 0, 1),
            position + new Vector3(0, 1, 1),
            position + new Vector3(1, 1, 1),
        });

        normals.AddRange(new[] {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
        });

        uv.AddRange(new[] {
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMin),
            new Vector2(uMin, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 1, verticesCount + 2,
            verticesCount + 2, verticesCount + 1, verticesCount + 3,
        });
    }

    public static void GenBlockBackwardVertices(Block block, Vector3 position, List<Vector3> vertices, List<Vector3> normals, List<Vector2> uv, List<int> tris)
    {
        int verticesCount = vertices.Count;

        int u = block.BlockType.SideTextureId % TILESET_WIDTH;
        int v = (block.BlockType.SideTextureId - u) / TILESET_HEIGHT;

        float uMin = (float)u / TILESET_WIDTH;
        float uMax = uMin + 1.0f / TILESET_WIDTH;

        float vMin = (float)v / TILESET_HEIGHT;
        float vMax = vMin + 1.0f / TILESET_HEIGHT;

        vMin = 1.0f - vMin;
        vMax = 1.0f - vMax;

        vertices.AddRange(new[] {
            position + new Vector3(0, 0, 0),
            position + new Vector3(1, 0, 0),
            position + new Vector3(0, 1, 0),
            position + new Vector3(1, 1, 0),
        });

        normals.AddRange(new[] {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        });

        uv.AddRange(new[] {
            new Vector2(uMin, vMax),
            new Vector2(uMax, vMax),
            new Vector2(uMin, vMin),
            new Vector2(uMax, vMin),
        });

        tris.AddRange(new[] {
            verticesCount + 0, verticesCount + 2, verticesCount + 1,
            verticesCount + 2, verticesCount + 3, verticesCount + 1,
        });
    }
}
