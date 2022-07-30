using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡类，目前仅用于UI展示，后续也可加入关卡其他信息（如关卡弹幕流程）
/// </summary>
[CreateAssetMenu(fileName ="New Level",menuName ="Level Inventory/New Level")]
public class Level : ScriptableObject
{
    /// <summary>
    /// 关卡序列（编号）
    /// </summary>
    [Tooltip("关卡序列（编号）")]
    public int levelIndex;

    /// <summary>
    /// 关卡名
    /// </summary>
    [Tooltip("关卡名")]
    public string levelName;
    
    /// <summary>
    /// 关卡描述
    /// </summary>
    [Tooltip("关卡描述")]
    public string levelDescription;

    /// <summary>
    /// 关卡难度
    /// </summary>
    [Tooltip("关卡难度")]
    public string difficultyLevel; 

    /// <summary>
    /// 将关卡作为UI界面可选择的Button，然后进行左右移动和选择
    /// </summary>
    [Tooltip("将GameObject作为UI界面可选择的Button，然后进行左右移动和选择")]
    public GameObject levelButton;

    /// <summary>
    /// 关卡图片，放入Button所需的Image中
    /// </summary>
    [Tooltip("将关卡图片放入levelButton的Image组件中")]
    public Sprite levelImage;
}
