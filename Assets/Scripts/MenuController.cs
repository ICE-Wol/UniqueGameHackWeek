using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 菜单控制器，目前只写了GameStart到人物选择最后进入游戏这一条路径，之后还可以补充
/// </summary>
public class MenuController : MonoBehaviour
{
    static MenuController Controller;
        

    /// <summary>
    /// 最初的选择界面，可选择GameStart，replay等
    /// </summary>
    [Tooltip("最初的选择界面，可选择GameStart，replay等")]
    public GameObject initialUI;

    /// <summary>
    /// 关卡选择界面
    /// </summary>
    [Tooltip("关卡选择界面")]
    public GameObject levelUI;

    /// <summary>
    /// 难度选择界面
    /// </summary>
    [Tooltip("难度选择界面")]
    public GameObject difficultyUI;

    /// <summary>
    /// 人物选择界面
    /// </summary>
    [Tooltip("人物选择界面")]
    public GameObject characterUI;


    /// <summary>
    /// 加载界面
    /// </summary>
    [Tooltip("加载界面（不一定有）")]
    public GameObject loadingUI;

    /// <summary>
    /// 用来存放各UI界面的列表
    /// </summary>
    [Tooltip("用来存放各UI界面的列表")]
    public List<GameObject> UIList=new List<GameObject>();

    
    /// <summary>
    /// UI界面的索引，用来控制当前要显示的UI界面
    /// </summary>
    private int uiIndex;

    /// <summary>
    /// 属性设置，此处调用了事件触发函数
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
    /// 事件触发函数,当UIIndex增加时触发
    /// </summary>
    private void WhenUIIndexIncrease()
    {
        if(OnUIIndexIncrease!=null)
        {
            OnUIIndexIncrease(this,null);
        }
    }

    /// <summary>
    /// 事件触发函数,当UIIndex减少时触发
    /// </summary>
    private void WhenUIIndexReduce()
    {
        if(OnUIIndexReduce!=null)
        {
            OnUIIndexReduce(this,null);
        }
    }

    /// <summary>
    /// 定义的委托
    /// </summary>
    public delegate void UIIndexChanged(object sender,System.EventArgs e);

    /// <summary>
    /// 与委托相关联的事件，当UIIndex增加时触发
    /// </summary>
    public event UIIndexChanged OnUIIndexIncrease;
    
    /// <summary>
    /// 与委托相关联的事件，当UIIndex减少时触发
    /// </summary>
    public event UIIndexChanged OnUIIndexReduce;
    

    /// <summary>
    /// UIIndex增加后触发（此时uiIndex还未增加），将uiIndex+1的UI界面显示，让前一个UI界面隐藏
    /// </summary>
    private void AfterUIIndexIncreased(object sender,System.EventArgs e)
    {
        Debug.Log("uiIndex Increased");
        UIList[uiIndex].gameObject.SetActive(false);
        UIList[uiIndex+1].gameObject.SetActive(true);
        //让要显示的界面的按钮处于select状态
        UIList[uiIndex+1].transform.GetChild(0).GetComponent<Button>().Select();
    }

    /// <summary>
    /// UIIndex减少后触发（此时uiIndex还未减少），将改变前的uiIndex显示的UI页面隐藏，让前一个UI界面显示
    /// </summary>
    private void AfterUIIndexReduced(object sender,System.EventArgs e)
    {
        Debug.Log("uiIndex Reduced");
        UIList[uiIndex].gameObject.SetActive(false);
        UIList[uiIndex-1].gameObject.SetActive(true);
        //让要显示的界面的按钮处于select状态
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

        //进入游戏后默认选中按钮
        transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Button>().Select();
    }
    void Awake()
    {
        //单例模式
        if(Controller!=null)
            Destroy(this);
        else
            Controller=this;
        
    }


    void Update()
    {
        //如果按下esc键，则返回到上一个UI界面
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiIndex>0)
            {
                UIIndex-=1;
                
                Debug.Log(uiIndex);
            }
        }
        //检测回车和逻辑，不用管
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if(uiIndex<UIList.Count-1)
        //    {
        //        UIIndex+=1;
                
        //        Debug.Log(uiIndex);
        //        Debug.Log("Enter");
        //    }
        //}

        //检测用，不用管
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("space");
        //}
    }

    /// <summary>
    /// 当点击Game Start按钮时，UIIndex会增加，进入下一个UI界面
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
    /// 当点击Level选择的任一个按钮时，UIIndex会增加，进入下一个UI界面
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
    /// 当点击Difficulty选择的任一个按钮时，UIIndex会增加，进入下一个UI界面
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
    /// 当点击CharcterUI并选择任一个按钮时，进入下一个UI界面（直接LoadScene(buildindex+1)
    /// </summary>
    public void CharacterButtonOnClicked()
    {
        int sceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex+1);
        Debug.Log("Enter");
        
    }
}



