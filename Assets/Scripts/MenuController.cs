using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �˵���������Ŀǰֻд��GameStart������ѡ����������Ϸ��һ��·����֮�󻹿��Բ���
/// </summary>
public class MenuController : MonoBehaviour
{
    static MenuController Controller;
        

    /// <summary>
    /// �����ѡ����棬��ѡ��GameStart��replay��
    /// </summary>
    [Tooltip("�����ѡ����棬��ѡ��GameStart��replay��")]
    public GameObject initialUI;

    /// <summary>
    /// �ؿ�ѡ�����
    /// </summary>
    [Tooltip("�ؿ�ѡ�����")]
    public GameObject levelUI;

    /// <summary>
    /// �Ѷ�ѡ�����
    /// </summary>
    [Tooltip("�Ѷ�ѡ�����")]
    public GameObject difficultyUI;

    /// <summary>
    /// ����ѡ�����
    /// </summary>
    [Tooltip("����ѡ�����")]
    public GameObject characterUI;


    /// <summary>
    /// ���ؽ���
    /// </summary>
    [Tooltip("���ؽ��棨��һ���У�")]
    public GameObject loadingUI;

    /// <summary>
    /// ������Ÿ�UI������б�
    /// </summary>
    [Tooltip("������Ÿ�UI������б�")]
    public List<GameObject> UIList=new List<GameObject>();

    
    /// <summary>
    /// UI������������������Ƶ�ǰҪ��ʾ��UI����
    /// </summary>
    private int uiIndex;

    /// <summary>
    /// �������ã��˴��������¼���������
    /// </summary>
    public int UIIndex
    {
        get{return uiIndex;}
        set
        {
            if(value>uiIndex)
            {
                WhenUIIndexIncrease();
            }
            else
            {
                WhenUIIndexReduce();
            }
            uiIndex=value;
        }

    }

    /// <summary>
    /// �¼���������,��UIIndex����ʱ����
    /// </summary>
    private void WhenUIIndexIncrease()
    {
        if(OnUIIndexIncrease!=null)
        {
            OnUIIndexIncrease(this,null);
        }
    }

    /// <summary>
    /// �¼���������,��UIIndex����ʱ����
    /// </summary>
    private void WhenUIIndexReduce()
    {
        if(OnUIIndexReduce!=null)
        {
            OnUIIndexReduce(this,null);
        }
    }

    /// <summary>
    /// �����ί��
    /// </summary>
    public delegate void UIIndexChanged(object sender,System.EventArgs e);

    /// <summary>
    /// ��ί����������¼�����UIIndex����ʱ����
    /// </summary>
    public event UIIndexChanged OnUIIndexIncrease;
    
    /// <summary>
    /// ��ί����������¼�����UIIndex����ʱ����
    /// </summary>
    public event UIIndexChanged OnUIIndexReduce;
    

    /// <summary>
    /// UIIndex���Ӻ󴥷�����ʱuiIndex��δ���ӣ�����uiIndex+1��UI������ʾ����ǰһ��UI��������
    /// </summary>
    private void AfterUIIndexIncreased(object sender,System.EventArgs e)
    {
        Debug.Log("uiIndex Increased");
        UIList[uiIndex].gameObject.SetActive(false);
        UIList[uiIndex+1].gameObject.SetActive(true);
        //��Ҫ��ʾ�Ľ���İ�ť����select״̬
        UIList[uiIndex+1].transform.GetChild(0).GetComponent<Button>().Select();
    }

    /// <summary>
    /// UIIndex���ٺ󴥷�����ʱuiIndex��δ���٣������ı�ǰ��uiIndex��ʾ��UIҳ�����أ���ǰһ��UI������ʾ
    /// </summary>
    private void AfterUIIndexReduced(object sender,System.EventArgs e)
    {
        Debug.Log("uiIndex Reduced");
        UIList[uiIndex].gameObject.SetActive(false);
        UIList[uiIndex-1].gameObject.SetActive(true);
        //��Ҫ��ʾ�Ľ���İ�ť����select״̬
        UIList[uiIndex-1].transform.GetChild(0).GetComponent<Button>().Select();
    }



    private void Start()
    {
        uiIndex=0;
        UIList[0]=initialUI;
        UIList[1]=levelUI;
        UIList[2]=difficultyUI;
        UIList[3]=characterUI;
        
        OnUIIndexIncrease+=new UIIndexChanged(AfterUIIndexIncreased);
        OnUIIndexReduce+=new UIIndexChanged(AfterUIIndexReduced);

        //������Ϸ��Ĭ��ѡ�а�ť
        transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Button>().Select();
    }
    void Awake()
    {
        //����ģʽ
        if(Controller!=null)
            Destroy(this);
        else
            Controller=this;
        
    }


    void Update()
    {
        //�������esc�����򷵻ص���һ��UI����
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiIndex>0)
            {
                UIIndex-=1;
                
                Debug.Log(uiIndex);
            }
        }
        //���س����߼������ù�
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if(uiIndex<UIList.Count-1)
        //    {
        //        UIIndex+=1;
                
        //        Debug.Log(uiIndex);
        //        Debug.Log("Enter");
        //    }
        //}

        //����ã����ù�
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("space");
        //}
    }

    /// <summary>
    /// �����Game Start��ťʱ��UIIndex�����ӣ�������һ��UI����
    /// </summary>
    public void StartButtonOnClicked()
    {
        if(uiIndex<UIList.Count-1)
            {
                UIIndex+=1;
                
                Debug.Log(uiIndex);
                Debug.Log("Enter");
            }
    }

    /// <summary>
    /// �����Levelѡ�����һ����ťʱ��UIIndex�����ӣ�������һ��UI����
    /// </summary>
    public void LevelButtonOnClicked()
    {
        if(uiIndex<UIList.Count-1)
        {
                UIIndex+=1;
                
                Debug.Log(uiIndex);
                Debug.Log("Enter");
        }
    }

    /// <summary>
    /// �����Difficultyѡ�����һ����ťʱ��UIIndex�����ӣ�������һ��UI����
    /// </summary>
    public void DifficultyButtonOnClicked()
    {
        if(uiIndex<UIList.Count-1)
        {
            UIIndex+=1;
                
            Debug.Log(uiIndex);
            Debug.Log("Enter");
        }
    }

    /// <summary>
    /// �����CharcterUI��ѡ����һ����ťʱ��������һ��UI���棨ֱ��LoadScene(buildindex+1)
    /// </summary>
    public void CharacterButtonOnClicked()
    {
        int sceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex+1);
        Debug.Log("Enter");
        
    }
}



