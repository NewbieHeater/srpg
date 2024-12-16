using Unity.AI.Navigation;
using UnityEngine;

public class TileMaps : MonoSingleton<TileMaps>
{
    public NavMeshSurface nms;

    public CustomGridConfig config;
    private Tile[,] tileArr;  // 타일 배열

    #region 좌표 계산
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x / config.CellSize.x), Mathf.FloorToInt(worldPos.z / config.CellSize.y));
    }

    public Vector3 GridToWorld(Vector2Int cellPos)
    {
        return new Vector3(cellPos.x * config.CellSize.x + config.CellSize.x * 0.5f, 0, cellPos.y * config.CellSize.y + config.CellSize.y * 0.5f);
    }
    #endregion

    void Start()
    {
        InitializeTileMap();
    }

    void InitializeTileMap()
    {
        tileArr = new Tile[11, 11];

        // 타일 배열을 초기화하는 코드 (타일 생성, etc.)
    }

    // 주어진 좌표에 있는 타일의 이동 가능 여부를 반환
    public bool IsWalkable(int x, int y)
    {
        if (x < 0 || y < 0 || x > config.CellSize.x || y > config.CellSize.y) return false;
        return tileArr[x, y].isWalkable;
    }

    public void SetTile()
    {
        foreach (Transform child in transform)
        {
            int x = (int)(child.position.x - 0.5f);
            int y = (int)(child.position.z - 0.5f);

            tileArr[x, y] = child.GetComponent<Tile>();
        }
    }
}
