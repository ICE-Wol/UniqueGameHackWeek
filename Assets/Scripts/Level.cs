using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ؿ��࣬Ŀǰ������UIչʾ������Ҳ�ɼ���ؿ�������Ϣ����ؿ���Ļ���̣�
/// </summary>
[CreateAssetMenu(fileName ="New Level",menuName ="Level Inventory/New Level")]
public class Level : ScriptableObject
{
    /// <summary>
    /// �ؿ����У���ţ�
    /// </summary>
    [Tooltip("�ؿ����У���ţ�")]
    public int levelIndex;

    /// <summary>
    /// �ؿ���
    /// </summary>
    [Tooltip("�ؿ���")]
    public string levelName;
    
    /// <summary>
    /// �ؿ�����
    /// </summary>
    [Tooltip("�ؿ�����")]
    public string levelDescription;

    /// <summary>
    /// �ؿ��Ѷ�
    /// </summary>
    [Tooltip("�ؿ��Ѷ�")]
    public string difficultyLevel; 

    /// <summary>
    /// ���ؿ���ΪUI�����ѡ���Button��Ȼ����������ƶ���ѡ��
    /// </summary>
    [Tooltip("��GameObject��ΪUI�����ѡ���Button��Ȼ����������ƶ���ѡ��")]
    public GameObject levelButton;

    /// <summary>
    /// �ؿ�ͼƬ������Button�����Image��
    /// </summary>
    [Tooltip("���ؿ�ͼƬ����levelButton��Image�����")]
    public Sprite levelImage;
}
