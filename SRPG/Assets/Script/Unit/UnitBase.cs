using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public int movementRange; // ������ �̵� ����
    public Vector3 currentPos; // ���� ��ġ
    public bool isMoving; // �̵� ������ ����

    // ������ �̵��ϴ� �޼���
    public abstract void Move(Vector3 destination);
}