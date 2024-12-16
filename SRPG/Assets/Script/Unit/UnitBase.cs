using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public int movementRange; // 유닛의 이동 범위
    public Vector3 currentPos; // 현재 위치
    public bool isMoving; // 이동 중인지 여부

    // 유닛이 이동하는 메서드
    public abstract void Move(Vector3 destination);
}