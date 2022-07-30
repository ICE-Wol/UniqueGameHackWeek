using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New LevelInventory",menuName ="Level Inventory/New LevelInventory")]
public class LevelInventory : ScriptableObject
{
    /// <summary>
    /// 关卡列表，可添加多个关卡（目前只是用于UI展示）
    /// </summary>
    public List<Level> levelList=new List<Level>();
}
