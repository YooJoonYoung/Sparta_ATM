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

    //UI 텍스트
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    //에러 메시지 UI
    public GameObject popUpError;
    public Button errorButton;
    public GameObject popSignUpError;
    public Button popSignUpErrorButton;
    public GameObject iDErrorMSG;
    public GameObject pSErrorMSG;
    // 로그인 팝업
    public GameObject popupLogin;
    public GameObject popupBank;
    public GameObject popupSignUp; //회원가입 팝업

    // 로그인 UI 요소
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button signupButton;

    // 회원가입 UI 요소
    public TMP_InputField signUpIdInputField;
    public TMP_InputField signUpNameInputField;
    public TMP_InputField signUpPasswordInputField;
    public TMP_InputField signUpPasswordConfirmInputField;
    public Button signUpButton;
    public Button signUpCancelButton;

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
        // 로그인 팝업 먼저 표시
        popupLogin.SetActive(true);
        popupBank.SetActive(false);
        popupSignUp.SetActive(false);
        popSignUpError.SetActive(false);

        // 로그인 버튼 클릭 시 호출
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        signupButton.onClick.AddListener(OnSignupButtonClicked);
       

        // 오류 버튼 클릭 시 팝업 닫기
        popUpError.SetActive(false);
        errorButton.onClick.AddListener(OnErrorButtonClicked);
        popSignUpErrorButton.onClick.AddListener(OnpopSignupButtonClicked);

        //회원가입 버튼 클릭시 호출
        signUpButton.onClick.AddListener(OnSignUpConfirmClicked);
        signUpCancelButton.onClick.AddListener(OnSignUpCancelClicked);

        // 게임 시작 시 저장된 데이터 로드
        LoadUserData();
    }
    // 로그인 버튼 클릭 처리
    void OnLoginButtonClicked()
    {
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;

        if (userdata.CheckCredentials(inputId, inputPassword))
        {
            // 로그인 성공: PopupLogin 숨기고 PopupBank 표시
            popupLogin.SetActive(false);
            popupBank.SetActive(true);
            Refresh();
        }
        else
        {
            // 로그인 실패: 에러 메시지 표시
            popUpError.SetActive(true);
             Debug.Log("로그인 실패");
        }
    }

    // 회원가입 버튼 클릭 처리
    void OnSignupButtonClicked()
    {
        popupSignUp.SetActive(true);
    }
    void OnSignUpConfirmClicked()
    {
        string id = signUpIdInputField.text;
        string name = signUpNameInputField.text;
        string password = signUpPasswordInputField.text;
        string passwordConfirm = signUpPasswordConfirmInputField.text;

        // 빈칸 체크
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            popSignUpError.SetActive(true);
            iDErrorMSG.SetActive(true);
            return;
        }

        // 비밀번호 확인
        if (password != passwordConfirm)
        {
            popSignUpError.SetActive(true);
            pSErrorMSG.SetActive(true);
            return;
        }

        // UserData 객체 생성
        userdata = new UserData(name, 100000, 50000, id, password);

        // 사용자 데이터 저장
        SaveUserData();

        // 회원가입 완료 후 로그인 화면으로 돌아가기
        popupSignUp.SetActive(false);
        popupLogin.SetActive(true);
    }
    
    // 회원가입 취소 버튼 클릭 시
    void OnSignUpCancelClicked()
    {
        popupSignUp.SetActive(false);  
        popupLogin.SetActive(true);   
    }

    void OnpopSignupButtonClicked()
    {
        popSignUpError.SetActive(false);
        iDErrorMSG.SetActive(false);
        pSErrorMSG.SetActive(false);
    }


    public void Refresh()
    {
        // UI에 이름, 현금, 잔액을 업데이트
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // 천 단위 콤마
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // 천 단위 콤마
    }

   
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
          
            Debug.Log("유저데이터 읽음");
        }
        else
        {          
            userdata = new UserData("유준영", 100000, 50000, "defaultId", "defaultPassword"); // 파일이 없다면 기본 값으로 초기화
            Debug.Log("기존 데이터 없어 기본값 설정");
        }
    }
    void OnErrorButtonClicked()
    {
        popUpError.SetActive(false);
    }
}