using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public const int GRID_SIZE = 16;
    public const int GEN_DISTANCE = 8;
    public const int UNLOAD_DISTANCE = 12;

    public Player player;
    public GameObject gridPrefab;

    public Dictionary<Vector2Int, int> heightmap = new Dictionary<Vector2Int, int>();
    public Dictionary<Vector3Int, Grid> grids = new Dictionary<Vector3Int, Grid>();
    public Queue<Vector3Int> gridUpdateQueue = new Queue<Vector3Int>();

    private void Start()
    {
        player.transform.position = new Vector3(0.5f, GetHeightAt(0, 0) + 0.5f, 0.5f);

        for (int i = -1; i < 1; i++)
        {
            for (int k = -1; k < 1; k++)
            {
                int height = GetHeightAt(0, 0);

                for (int j = (height / 16) - 1; j < (height / 16) + 1; j++)
                {
                    GenGrid(new Vector3Int(i, j, k));
                }
            }
        }

        StartCoroutine(GenGrids());
        StartCoroutine(GenNearGrids());
        StartCoroutine(UnloadFarGrids());
    }

    public int GetHeightAt(Vector2Int position)
    {
        if (heightmap.TryGetValue(position, out int height))
        {
            return height;
        }

        return heightmap[position] = (int)Math.Floor(32 + 32 * MultiOctaveNoise.Gen2(position));
    }

    public int GetHeightAt(int x, int z)
    {
        return GetHeightAt(new Vector2Int(x, z));
    }

    public void GenGridBlocks(Grid grid)
    {
        int airTypeId = BlockType.GetTypeIdByName("air");
        int dirtTypeId = BlockType.GetTypeIdByName("dirt");
        int grassTypeId = BlockType.GetTypeIdByName("grass");
        int stoneTypeId = BlockType.GetTypeIdByName("stone");

        var blocks = new Block[GRID_SIZE, GRID_SIZE, GRID_SIZE];

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int k = 0; k < blocks.GetLength(2); k++)
            {
                int x = (int)Math.Floor(grid.transform.position.x + i);
                int z = (int)Math.Floor(grid.transform.position.z + k);

                int height = GetHeightAt(x, z);

                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    int y = (int)Math.Floor(grid.transform.position.y + j);

                    if (y < height - 4)
                    {
                        blocks[i, j, k] = new Block(stoneTypeId);
                    }
                    else if (y < height)
                    {
                        blocks[i, j, k] = new Block(dirtTypeId);
                    }
                    else if (y == height)
                    {
                        blocks[i, j, k] = new Block(grassTypeId);
                    }
                    else if (y > height)
                    {
                        blocks[i, j, k] = new Block(airTypeId);
                    }
                }
            }
        }

        grid.blocks = blocks;
        grid.UpdateMesh();
    }

    public Grid GenGrid(Vector3Int gridPosition)
    {
        if (grids.TryGetValue(gridPosition, out Grid grid))
        {
            grid.UpdateMesh();
        }
        else
        {
            GameObject gridGameObject = Instantiate(gridPrefab, GRID_SIZE * gridPosition, Quaternion.identity);

            grid = gridGameObject.GetComponent<Grid>();
            GenGridBlocks(grid);

            grids[gridPosition] = grid;
        }

        return grid;
    }

    public IEnumerator GenGrids()
    {
        while (gridUpdateQueue.Count > 0)
        {
            Vector3Int gridPosition = gridUpdateQueue.Dequeue();
            GenGrid(gridPosition);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(GenGrids());
    }

    public IEnumerator GenNearGrids()
    {
        int cx = (int)Math.Floor(player.transform.position.x / 16);
        int cy = (int)Math.Floor(player.transform.position.y / 16);
        int cz = (int)Math.Floor(player.transform.position.z / 16);

        for (int x = cx - GEN_DISTANCE; x < cx + GEN_DISTANCE; x++)
        {
            int dx = x - cx;
            for (int z = cz - GEN_DISTANCE; z < cz + GEN_DISTANCE; z++)
            {
                int dz = z - cz;
                if (dx * dx + dz * dz > GEN_DISTANCE * GEN_DISTANCE)
                {
                    continue;
                }

                for (int y = cy - GEN_DISTANCE; y < cy + GEN_DISTANCE; y++)
                {
                    int dy = y - cy;
                    if (dx * dx + dy * dy + dz * dz > GEN_DISTANCE * GEN_DISTANCE)
                    {
                        continue;
                    }

                    var gridPosition = new Vector3Int(x, y, z);
                    if (grids.ContainsKey(gridPosition))
                    {
                        continue;
                    }

                    if (!gridUpdateQueue.Contains(gridPosition))
                    {
                        gridUpdateQueue.Enqueue(gridPosition);
                    }
                }
            }
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(GenNearGrids());
    }

    public IEnumerator UnloadFarGrids()
    {
        var gridsRemoveQueue = new Queue<Vector3Int>();

        int cx = (int)Math.Floor(player.transform.position.x / 16);
        int cy = (int)Math.Floor(player.transform.position.y / 16);
        int cz = (int)Math.Floor(player.transform.position.z / 16);

        foreach (Vector3Int gridPosition in grids.Keys)
        {
            int dx = gridPosition.x - cx;
            int dy = gridPosition.y - cy;
            int dz = gridPosition.z - cz;

            if (dx * dx + dy * dy + dz * dz < UNLOAD_DISTANCE * UNLOAD_DISTANCE)
            {
                continue;
            }

            gridsRemoveQueue.Enqueue(gridPosition);

        }

        foreach (Vector3Int gridPosition in gridsRemoveQueue)
        {
            Grid grid = grids[gridPosition];
            grids[gridPosition] = null;
            if (grid == null)
            {
                continue;
            }

            Destroy(grid.gameObject);
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(UnloadFarGrids());
    }
}
