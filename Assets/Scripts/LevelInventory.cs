using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New LevelInventory",menuName ="Level Inventory/New LevelInventory")]
public class LevelInventory : ScriptableObject
{
    /// <summary>
    /// �ؿ��б�����Ӷ���ؿ���Ŀǰֻ������UIչʾ��
    /// </summary>
    public List<Level> levelList=new List<Level>();
}
