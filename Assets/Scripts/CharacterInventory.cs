using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New CharacterInventory",menuName ="Character Inventory/New LevelInventory")]
public class CharacterInventory : ScriptableObject
{
    /// <summary>
    /// 角色列表，可添加多名角色（目前只是用于UI展示）
    /// </summary>
    public List<Character> CharacterList=new List<Character>();
}
