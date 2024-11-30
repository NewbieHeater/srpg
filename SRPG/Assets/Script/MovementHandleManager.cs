using UnityEngine;
using System.Collections.Generic;

public class MovementHandleManager : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public LayerMask tileLayer; // ������ Ÿ���� ���̾�
    private bool isDragging = false; // �巡�� ����
    private List<Vector3> path = new List<Vector3>(); // ��� ����Ʈ

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            StartDrag();
        }
        if (Input.GetMouseButton(0)) // ���콺�� ������ ���� ��
        {
            Dragging();
        }
        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� �� ��
        {
            EndDrag();
        }
    }

    // �巡�� ����
    void StartDrag()
    {
        // Raycast�� ���� ��ġ ��������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            // Ŭ���� ��ġ�� �����ǿ� ���� ���
            Vector3 startPoint = hit.point;
            path.Clear(); // ���� ��� �ʱ�ȭ
            path.Add(startPoint); // ���ο� ��� ����
            isDragging = true; // �巡�� ����
            Debug.Log("Drag started at: " + startPoint);
        }
    }

    // �巡�� ��
    void Dragging()
    {
        if (!isDragging) return;

        // ���콺�� �̵� ���� ��, ���� ��ġ�� ����Ͽ� ��ο� �߰�
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            Vector3 currentPoint = hit.point;
            if (!path.Contains(currentPoint)) // ���� ������ ��ο� ������ �߰�
            {
                path.Add(currentPoint);
                Debug.Log("Added to path: " + currentPoint);
            }
        }
    }

    // �巡�� ����
    void EndDrag()
    {
        isDragging = false;
        Debug.Log("Drag ended. Path contains " + path.Count + " points.");

        // ��ο� ���� ���� �̵� ���� ó���� �� �ֽ��ϴ�.
        // ���÷� path�� ù ��° �������� ������ �������� ������ �̵���ų �� �ֽ��ϴ�.
        // ��: MoveAlongPath();
    }

    // ��θ� ���� ������ �̵���Ű�� ���� �޼���
    void MoveAlongPath()
    {
        foreach (Vector3 point in path)
        {
            // ��ο� ���� ������ �̵���Ŵ
            // ����: ������ �� ����Ʈ�� �̵��ϵ��� �ϴ� �ڵ�
            // ������ ��ġ�� ������Ʈ�ϰų� �̵� ������ ó��
        }
    }
}
