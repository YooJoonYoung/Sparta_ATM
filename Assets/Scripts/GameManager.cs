using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //JSON작업을 위한 네임스페이스(저장등 파일관리를 위해 추가)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userdata;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;
    public GameObject popUpError;
    public Button errorButton;

    private string saveFilePath; 

    // Start is called before the first frame update

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        saveFilePath = Application.persistentDataPath + "/userdata.json"; //JSON 파일 저장 경로 설정하기
    }

    void Start()
    {

        //userdata = new UserData("유준영", 100000, 50000);
        LoadUserData();
       // Refresh(); //초기 UI 업데이트
        popUpError.SetActive(false);
        errorButton.onClick.AddListener(OnErrorButtonClicked);
    }

    public void Refresh()
    {
        // UI에 이름, 현금, 잔액을 업데이트
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // 천 단위 콤마
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // 천 단위 콤마
    }

    // 데이터를 갱신할 메서드 (예시로 사용자가 현금을 변경할 때)
    //public void UpdateUserData(string name, int cash, int balance)
    //{
    //    userdata.Name = name;
    //    userdata.Cash = cash;
    //    userdata.Balance = balance;
    //    Refresh();  // 데이터가 변경되었으면 UI 갱신
    //}

    public void Add(int amount)
    {
        userdata.AddMoney(amount);
        Refresh();
        SaveUserData(); // 입금후 자동저장
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
        SaveUserData();//출금후 자동저장
    }
    
    public void SaveUserData()// JSON 파일에 데이터 저장
    {      
        string json = JsonUtility.ToJson(userdata, true);// UserData를 JSON으로 직렬화

        File.WriteAllText(saveFilePath, json); // JSON 파일로 저장
        Debug.Log("유저데이터 저장됨");
    }

    public void LoadUserData()// JSON 파일에서 데이터 로드
    {
        if (File.Exists(saveFilePath))
        {  
            string json = File.ReadAllText(saveFilePath); // JSON 파일 읽기
            userdata = JsonUtility.FromJson<UserData>(json); // JSON을 UserData 객체로 변환        
            Refresh(); // UI 갱신
            Debug.Log("유저데이터 읽음");
        }
        else
        {          
            userdata = new UserData("유준영", 100000, 50000); // 파일이 없다면 기본 값으로 초기화
            Debug.Log("기존 데이터 없어 기본값 설정");
        }
    }
    void OnErrorButtonClicked()
    {
        popUpError.SetActive(false);
    }
}