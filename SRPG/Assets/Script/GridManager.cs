using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public CustomGrid grid;          // 그리드 스크립트 참조
    public CustomGridPalette palette; // 팔레트 참조

    public CustomGridPaletteItem selectedItem; // 현재 선택된 아이템

    void Start()
    {
        // 초기화
        grid.RetreiveAll();
        grid.RefreshPoints();
    }

    void Update()
    {
        // 마우스 클릭 시, 선택된 아이템을 그리드에 추가
        if (Input.GetMouseButtonDown(0) && selectedItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("BuilderGround")))
            {
                Debug.Log(hit.point);
                Vector2Int cellPos = grid.GetCellPos(hit.point);
                if (grid.Contains(cellPos) && !grid.IsItemExist(cellPos))
                {
                    Debug.Log("아이템 추가");
                    // 아이템 추가
                    grid.AddItem(cellPos, selectedItem);
                    //SaveData(); // 데이터를 저장
                }
                else
                    Debug.Log("아이템 추가실패");
            }
        }
    }

    void OnGUI()
    {
        // 기본지형 버튼
        if (GUI.Button(new Rect(20, 20, 400, 60), "기본지형"))
        {
            selectedItem = palette.GetItem(0); // ID가 0인 팔레트 아이템 가져오기
            Debug.Log("Selected item: 기본지형");
        }
        
        // 공허 버튼
        if (GUI.Button(new Rect(500, 20, 400, 60), "공허"))
        {
            selectedItem = palette.GetItem(1); // ID가 1인 팔레트 아이템 가져오기
            Debug.Log("Selected item: 공허");
        }

        // 저장 버튼
        if (GUI.Button(new Rect(980, 20, 400, 60), "공허"))
        {
            SaveData(); // 데이터를 저장
            grid.RetreiveAll();
            Debug.Log("저장");
        }

        // 불러오기 버튼
        if (GUI.Button(new Rect(1580, 20, 400, 60), "공허"))
        {
            LoadData(); // 데이터를 저장
            grid.RetreiveAll();
            Debug.Log("불러오기");
        }
    }

    // 데이터 저장
    void SaveData()
    {
        byte[] data = grid.Serialize();
        string path = Path.Combine(Application.persistentDataPath, "MapData.dat");
        File.WriteAllBytes(path, data);
        Debug.Log("Data saved to: " + path);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "MapData.dat");
        if (File.Exists(path))
        {
            byte[] loadedData = File.ReadAllBytes(path);
            grid.Import(loadedData, palette);
            Debug.Log("Data loaded from: " + path);
        }
        else
        {
            Debug.LogError("File not found at: " + path);
        }
    }
}
