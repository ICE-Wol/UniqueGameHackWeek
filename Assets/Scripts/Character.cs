using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 角色类，目前仅用于UI展示，后续也可加入角色游戏相关属性
/// </summary>
[CreateAssetMenu(fileName ="New Character",menuName ="Character Inventory/New Character")]
public class Character : ScriptableObject
{
    /// <summary>
    /// 角色编号
    /// </summary>
    [Tooltip("角色编号")]
    public int CharacterIndex;
    
    /// <summary>
    /// 角色名
    /// </summary>
    [Tooltip("角色名")]
    [TextArea]
    public string CharacterName;
    
    
    /// <summary>
    /// 角色描述，主要是背景介绍
    /// </summary>
    [Tooltip("角色描述，主要是角色在游戏中的背景介绍")]
    [TextArea]
    public string CharacterDescription;
    
    /// <summary>
    /// 角色能力，主要是游戏中的能力（如低速移动、高速移动时的不同能力）
    /// </summary>
    [Tooltip("角色能力，主要是游戏中的能力（如低速移动、高速移动时的不同能力）")]
    [TextArea]
    public string CharacterAbilitis;

    /// <summary>
    /// 将当前角色选择的页面作为一个Button，可通过左右键进行选择
    /// </summary>
    [Tooltip("将当前角色选择的页面作为一个Button，可通过左右键进行选择")]
    public GameObject CharacterButton;

    /// <summary>
    /// 角色图片,人物选择时的立绘，放入Button的Image中
    /// </summary>
    [Tooltip("角色图片,人物选择时的立绘，放入Button的Image中")]
    public Sprite CharacterImage;
}
