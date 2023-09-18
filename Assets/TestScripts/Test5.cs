using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    // 地图尺寸
    public int mapWidth = 50;
    public int mapHeight = 10;

    // 地形类型
    public TileBase[] terrainTiles;
    public float[] terrainHeights;

    // Tilemap组件
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // 初始化地图数组为平原
        int[,] terrainMap = new int[mapHeight, mapWidth];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                terrainMap[y, x] = 0;
            }
        }

        // 随机生成草地、水面和山丘
        for (int y = 1; y < mapHeight - 1; y++)
        {
            for (int x = 1; x < mapWidth - 1; x++)
            {
                int terrainType = Random.Range(1, terrainTiles.Length);
                terrainMap[y, x] = terrainType;

                if (terrainType == 4)
                {
                    // 山丘需要在周围一圈加上平原以避免出现不可通过的情况
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            if (terrainMap[y + dy, x + dx] == 0)
                            {
                                terrainMap[y + dy, x + dx] = 2;
                            }
                        }
                    }
                }
            }
        }

        // 平地和山丘之间随机生成草地
        for (int y = 1; y < mapHeight - 1; y++)
        {
            for (int x = 1; x < mapWidth - 1; x++)
            {
                if (terrainMap[y, x] == 0 && terrainMap[y, x + 1] == 4)
                {
                    for (int dx = 0; dx < Random.Range(2, 5); dx++)
                    {
                        if (x + dx < mapWidth)
                        {
                            terrainMap[y, x + dx] = 2;
                        }
                    }
                }
                else if (terrainMap[y, x] == 4 && terrainMap[y, x - 1] == 0)
                {
                    for (int dx = -Random.Range(2, 5); dx <= 0; dx++)
                    {
                        if (x + dx >= 0)
                        {
                            terrainMap[y, x + dx] = 2;
                        }
                    }
                }
            }
        }

        // 绘制地形
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int terrainType = terrainMap[y, x];
                TileBase tile = terrainTiles[terrainType];
                float heightRangeMin = terrainHeights[terrainType * 2];
                float heightRangeMax = terrainHeights[terrainType * 2 + 1];
                float height = Random.Range(heightRangeMin, heightRangeMax);
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tile);
                for (int i = 1; i < height; i++)
                {
                    Vector3Int heightPosition = new Vector3Int(x, y + i, 0);
                    tilemap.SetTile(heightPosition, tile);
                }
            }
        }
    }
}