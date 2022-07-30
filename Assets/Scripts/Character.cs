using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ɫ�࣬Ŀǰ������UIչʾ������Ҳ�ɼ����ɫ��Ϸ�������
/// </summary>
[CreateAssetMenu(fileName ="New Character",menuName ="Character Inventory/New Character")]
public class Character : ScriptableObject
{
    /// <summary>
    /// ��ɫ���
    /// </summary>
    [Tooltip("��ɫ���")]
    public int CharacterIndex;
    
    /// <summary>
    /// ��ɫ��
    /// </summary>
    [Tooltip("��ɫ��")]
    [TextArea]
    public string CharacterName;
    
    
    /// <summary>
    /// ��ɫ��������Ҫ�Ǳ�������
    /// </summary>
    [Tooltip("��ɫ��������Ҫ�ǽ�ɫ����Ϸ�еı�������")]
    [TextArea]
    public string CharacterDescription;
    
    /// <summary>
    /// ��ɫ��������Ҫ����Ϸ�е�������������ƶ��������ƶ�ʱ�Ĳ�ͬ������
    /// </summary>
    [Tooltip("��ɫ��������Ҫ����Ϸ�е�������������ƶ��������ƶ�ʱ�Ĳ�ͬ������")]
    [TextArea]
    public string CharacterAbilitis;

    /// <summary>
    /// ����ǰ��ɫѡ���ҳ����Ϊһ��Button����ͨ�����Ҽ�����ѡ��
    /// </summary>
    [Tooltip("����ǰ��ɫѡ���ҳ����Ϊһ��Button����ͨ�����Ҽ�����ѡ��")]
    public GameObject CharacterButton;

    /// <summary>
    /// ��ɫͼƬ,����ѡ��ʱ�����棬����Button��Image��
    /// </summary>
    [Tooltip("��ɫͼƬ,����ѡ��ʱ�����棬����Button��Image��")]
    public Sprite CharacterImage;
}
