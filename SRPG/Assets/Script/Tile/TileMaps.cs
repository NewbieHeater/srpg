using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMaps : MonoSingleton<TileMaps>
{
    public CustomGridConfig config;
    private Tile[,] tileArr;  // 타일 배열
    private List<Vector3Int> currentPath;  // 현재 설정된 경로
    public bool DragOn = false;
    private bool isDrawingPath = false;
    #region 좌표 계산
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPos.x / config.CellSize.x), Mathf.FloorToInt(worldPos.z / config.CellSize.y));
    }

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        return new Vector3Int(Mathf.FloorToInt(worldPos.x / config.CellSize.x), 0, Mathf.FloorToInt(worldPos.z / config.CellSize.y));
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
    private void Update()
    {
        if (DragOn)
        {
            MovementRoute();
        }
        
    }

    public List<Vector3Int> MovementRoute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 시 경로 그리기 시작
            currentPath = new List<Vector3Int>(); // 경로 초기화
            isDrawingPath = true;
        }

        if (isDrawingPath && Input.GetMouseButton(0))
        {
            // 마우스 드래그 중일 때
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitWorldPos = hit.point;

                Vector3Int cellPos = WorldToCell(hitWorldPos);

                if (!PathContains(cellPos))
                {
                    currentPath.Add(cellPos);
                    //tilemap.SetTile(cellPos, pathTile);
                }
                else if (PathContains(cellPos))
                {
                    currentPath.Remove(cellPos);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // 마우스 버튼을 떼면 경로 그리기 종료
            Debug.Log("경로 설정 완료");
            isDrawingPath = false;
            DragOn = false;
            // 드래그가 끝나면 플레이어가 경로를 따를 수 있도록 이동 시작
            if (currentPath.Count > 0)
            {
                return currentPath;
            }
        }
        return currentPath;
    }
    private bool PathContains(Vector3Int cellPos)
    {
        if (cellPos.x < 0 || cellPos.y < 0 || cellPos.x > config.CellSize.x || cellPos.y > config.CellSize.y)
        {
            return true;
        }
        else
        {
            foreach (Vector3Int point in currentPath)
            {
                if (point == cellPos)
                    return true;
            }
            return false;
        }
        
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
