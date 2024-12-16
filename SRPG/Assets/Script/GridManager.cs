using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public CustomGrid grid;          // �׸��� ��ũ��Ʈ ����
    public CustomGridPalette palette; // �ȷ�Ʈ ����

    public CustomGridPaletteItem selectedItem; // ���� ���õ� ������

    void Start()
    {
        // �ʱ�ȭ
        grid.RetreiveAll();
        grid.RefreshPoints();
    }

    void Update()
    {
        // ���콺 Ŭ�� ��, ���õ� �������� �׸��忡 �߰�
        if (Input.GetMouseButtonDown(0) && selectedItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("BuilderGround")))
            {
                Debug.Log(hit.point);
                Vector2Int cellPos = grid.GetCellPos(hit.point);
                if (grid.Contains(cellPos) && !grid.IsItemExist(cellPos))
                {
                    Debug.Log("������ �߰�");
                    // ������ �߰�
                    grid.AddItem(cellPos, selectedItem);
                    //SaveData(); // �����͸� ����
                }
                else
                    Debug.Log("������ �߰�����");
            }
        }
    }

    void OnGUI()
    {
        // �⺻���� ��ư
        if (GUI.Button(new Rect(20, 20, 400, 60), "�⺻����"))
        {
            selectedItem = palette.GetItem(0); // ID�� 0�� �ȷ�Ʈ ������ ��������
            Debug.Log("Selected item: �⺻����");
        }
        
        // ���� ��ư
        if (GUI.Button(new Rect(500, 20, 400, 60), "����"))
        {
            selectedItem = palette.GetItem(1); // ID�� 1�� �ȷ�Ʈ ������ ��������
            Debug.Log("Selected item: ����");
        }

        // ���� ��ư
        if (GUI.Button(new Rect(980, 20, 400, 60), "����"))
        {
            SaveData(); // �����͸� ����
            grid.RetreiveAll();
            Debug.Log("����");
        }

        // �ҷ����� ��ư
        if (GUI.Button(new Rect(1580, 20, 400, 60), "����"))
        {
            LoadData(); // �����͸� ����
            grid.RetreiveAll();
            Debug.Log("�ҷ�����");
        }
    }

    // ������ ����
    void SaveData()
    {
        byte[] data = grid.Serialize();
        string path = Path.Combine(Application.persistentDataPath, "MapData.dat");
        File.WriteAllBytes(path, data);
        Debug.Log("Data saved to: " + path);
    }

    // ������ �ҷ�����
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
