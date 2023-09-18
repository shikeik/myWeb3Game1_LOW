using UnityEngine;
using UnityEngine.Tilemaps;

public class SmoothTerrainGenerator : MonoBehaviour
{
    public Tilemap tilemap; // Tilemap组件，用于渲染地形瓦片
    public TileBase[] tiles; // 存储不同地形瓦片的数组

    public int width = 50; // 地图宽度
    public int height = 50; // 地图高度

    [Range(0, 1)]
    public float threshold = 0.5f; // 阈值，控制生成地形的平滑程度

    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noise = Mathf.PerlinNoise(x * 0.2f, y * 0.2f); // 使用Perlin噪声生成随机数值
                if (noise > threshold)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[1]); // 设置当前位置的地形瓦片为数组中索引为1的瓦片
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[0]); // 设置当前位置的地形瓦片为数组中索引为0的瓦片
                }
            }
        }

        tilemap.RefreshAllTiles(); // 刷新所有地形瓦片，使其在场景中显示出来
    }
}

