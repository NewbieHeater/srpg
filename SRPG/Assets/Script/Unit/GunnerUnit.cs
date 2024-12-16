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

    // 유닛이 이동하는 메서드
    public override void Move(Vector3 destination)
    {
        List<Vector3Int> currentPath = TileMaps.Instance.MovementRoute();
    }

}
