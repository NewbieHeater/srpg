using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerUnit : UnitBase
{
    public GunnerUnit()
    {
        movementRange = 150;
        currentPos = transform.position;
        isMoving = false;
        health = 100;
    }

    // ������ �̵��ϴ� �޼���
    public override void Move(Vector3 destination)
    {
        List<Vector3Int> currentPath = TileMaps.Instance.MovementRoute();
    }

}
