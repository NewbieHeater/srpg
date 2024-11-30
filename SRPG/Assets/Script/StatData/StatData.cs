using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatData
{
    public string weaponName { get; set; }

    [field: Header("�ʱ�ȭ �� �ִ� ü��")]
    [field: SerializeField] public float mMaxHp { private set; get; }
    public float maxHp
    {
        get
        {
            return (mMaxHp); //+ (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Hp : 0f)));
        }
    }
    [SerializeField][HideInInspector] private float mHpCurrent;
    public float HpCurrent
    {
        get
        {
            return mHpCurrent;
        }
    }


    [field: Header("�ʱ�ȭ �� �⺻ ���ݷ�")]
    [field: SerializeField] public float baseAttack = 10;
    public float AttackCurrent
    {
        get
        {
            return ((baseAttack));// + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("�ʱ�ȭ �� �⺻ ���ݼӵ�")]
    [field: SerializeField] public float Speed { private set; get; }
    public float speed
    {
        get
        {
            return ((Speed));// + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("�ʱ�ȭ �� �⺻ �̵��ӵ�")]
    [field: SerializeField] public float baseMovementPower { private set; get; }
    public float MovementPowerCurrent
    {
        get
        {
            return baseMovementPower;// + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ ����")]
    [field: SerializeField] public float baseDefense { private set; get; }
    public float DefenseCurrent
    {
        get
        {
            return baseDefense;// + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }

    #region �ܺ� Ŭ����

    //public Character buffController; // ���� ��Ʈ�ѷ� (��ο��� ����)

    [Space(30)]
    [Header("�ܺ� Ŭ������ �����Ͽ� ���ȿ� �߰� ȿ��")]
    [Header("�ش� ��ü�� ����κ��丮, ������� null ����")]
    public int give;
    //[SerializeField] public EquipmentInventory? equipmentInventory = null; // ��� �κ��丮 (���������� �ε��Ͽ� ��� ����)

    #endregion



    /// <summary>
    /// ���� ������ �ʱⰪ���� �ʱ�ȭ
    /// </summary>
    public void InitStatData()
    {
        mHpCurrent = mHpCurrent == 0 ? maxHp : mHpCurrent;
    }

    /// <summary>
    /// ���� ü���� ����
    /// </summary>
    /// <param name="amount">������ �� (����ϰ�� ���� ü�� ����)</param>
    /// <returns>ü���� 0 �̸��ΰ�?</returns>
    public bool ModifyCurrentHp(float amount)
    {
        mHpCurrent += amount;
        mHpCurrent = Mathf.Clamp(mHpCurrent, float.MinValue, maxHp);

        return mHpCurrent < 0f;
    }


}