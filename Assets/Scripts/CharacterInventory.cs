using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New CharacterInventory",menuName ="Character Inventory/New LevelInventory")]
public class CharacterInventory : ScriptableObject
{
    /// <summary>
    /// ��ɫ�б�����Ӷ�����ɫ��Ŀǰֻ������UIչʾ��
    /// </summary>
    public List<Character> CharacterList=new List<Character>();
}
