using Unity.AI.Navigation;
using UnityEngine;

public class TileMaps : MonoSingleton<TileMaps>
{
    public NavMeshSurface nms;

    public CustomGridConfig config;
    private Tile[,] tileArr;  // Ÿ�� �迭

    #region ��ǥ ���
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

        // Ÿ�� �迭�� �ʱ�ȭ�ϴ� �ڵ� (Ÿ�� ����, etc.)
    }

    // �־��� ��ǥ�� �ִ� Ÿ���� �̵� ���� ���θ� ��ȯ
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
