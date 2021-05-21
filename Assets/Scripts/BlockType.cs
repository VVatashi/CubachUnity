using System;
using System.Collections.Generic;

public struct BlockType
{
    public string Name;

    public int TopTextureId;
    public int BottomTextureId;
    public int SideTextureId;

    public bool Opaque;

    public BlockType(string name, int topTextureId, int bottomTextureId, int sideTextureId, bool opaque = true)
    {
        Name = name;

        TopTextureId = topTextureId;
        BottomTextureId = bottomTextureId;
        SideTextureId = sideTextureId;

        Opaque = opaque;
    }

    public BlockType(string name, int textureId, bool opaque = true) : this(name, textureId, textureId, textureId, opaque) { }

    public static Dictionary<int, BlockType> BlockTypes = new Dictionary<int, BlockType>
    {
        [0] = new BlockType("air", textureId: 0, opaque: false),
        [1] = new BlockType("dirt", textureId: 1),
        [2] = new BlockType("grass", topTextureId: 2, bottomTextureId: 1, sideTextureId: 3),
        [3] = new BlockType("stone", textureId: 4),
    };

    public static int GetTypeIdByName(string name)
    {
        foreach (KeyValuePair<int, BlockType> pair in BlockTypes)
        {
            if (pair.Value.Name == name)
            {
                return pair.Key;
            }
        }

        throw new ArgumentException($"Block type \"{name}\" not found", nameof(name));
    }

    public static BlockType GetTypeByName(string name)
    {
        foreach (KeyValuePair<int, BlockType> pair in BlockTypes)
        {
            if (pair.Value.Name == name)
            {
                return pair.Value;
            }
        }

        throw new ArgumentException($"Block type \"{name}\" not found", nameof(name));
    }
}
