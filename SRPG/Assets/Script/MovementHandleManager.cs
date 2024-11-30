using UnityEngine;
using System.Collections.Generic;

public class MovementHandleManager : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라
    public LayerMask tileLayer; // 격자판 타일의 레이어
    private bool isDragging = false; // 드래그 상태
    private List<Vector3> path = new List<Vector3>(); // 경로 리스트

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            StartDrag();
        }
        if (Input.GetMouseButton(0)) // 마우스를 누르고 있을 때
        {
            Dragging();
        }
        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗄 때
        {
            EndDrag();
        }
    }

    // 드래그 시작
    void StartDrag()
    {
        // Raycast로 시작 위치 가져오기
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            // 클릭한 위치가 격자판에 닿은 경우
            Vector3 startPoint = hit.point;
            path.Clear(); // 기존 경로 초기화
            path.Add(startPoint); // 새로운 경로 시작
            isDragging = true; // 드래그 시작
            Debug.Log("Drag started at: " + startPoint);
        }
    }

    // 드래그 중
    void Dragging()
    {
        if (!isDragging) return;

        // 마우스가 이동 중일 때, 현재 위치를 계산하여 경로에 추가
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
        {
            Vector3 currentPoint = hit.point;
            if (!path.Contains(currentPoint)) // 같은 지점이 경로에 없으면 추가
            {
                path.Add(currentPoint);
                Debug.Log("Added to path: " + currentPoint);
            }
        }
    }

    // 드래그 종료
    void EndDrag()
    {
        isDragging = false;
        Debug.Log("Drag ended. Path contains " + path.Count + " points.");

        // 경로에 따라 유닛 이동 등을 처리할 수 있습니다.
        // 예시로 path의 첫 번째 지점부터 마지막 지점까지 유닛을 이동시킬 수 있습니다.
        // 예: MoveAlongPath();
    }

    // 경로를 따라 유닛을 이동시키는 예시 메서드
    void MoveAlongPath()
    {
        foreach (Vector3 point in path)
        {
            // 경로에 따라 유닛을 이동시킴
            // 예시: 유닛이 각 포인트로 이동하도록 하는 코드
            // 유닛의 위치를 업데이트하거나 이동 로직을 처리
        }
    }
}
