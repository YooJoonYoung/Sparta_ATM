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

    //송금 UI 요소
    public TMP_InputField rimittanceTarger;
    public TMP_InputField rimittanceprice;
    public Button rimittancebutton;
    public Button rimittanceExitButton;

    public string saveFilePath; 

    public RegistrationManager registrationManager;

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

        //송금 기능버튼 클릭시 호출
        rimittancebutton.onClick.AddListener(OnRimittanceButtonClicked);
        rimittanceExitButton.onClick.AddListener(OnRimittanceExitButtonClicked);

        // 게임 시작 시 저장된 데이터 로드
        //  LoadUserData();
    }
    // 로그인 버튼 클릭 처리
    void OnLoginButtonClicked()
    {
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;
        UserData foundUser = registrationManager.FindUserByCredentials(inputId, inputPassword);//사용자 리스트에서 정보확인

        //if (userdata.CheckCredentials(inputId, inputPassword))
        if(foundUser !=null)
        {
            // 로그인 성공: PopupLogin 숨기고 PopupBank 표시
            userdata = foundUser; //로그인된 사용자데이터
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
      //  SaveUserData();
      registrationManager.userDataList.Add(userdata);
        registrationManager.dataManager.SaveUserData(registrationManager.userDataList);

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
  
    void OnRimittanceButtonClicked() // 송금 버튼 클릭 처리
    {
        string targetName = rimittanceTarger.text; // 송금 대상 이름
        string amountText = rimittanceprice.text;   // 송금 금액

        // 송금 대상 이름 또는 금액이 비어있으면 에러 메시지 출력
        if (string.IsNullOrEmpty(targetName) || string.IsNullOrEmpty(amountText))
        {
            Debug.Log("입력정보를 확인해주세요");
            return;
        }

        int amount;
        if (!int.TryParse(amountText, out amount) || amount <= 0)
        {
            Debug.Log("금액을 올바르게 입력해주세요");
            return;
        }

        // 잔액이 부족하면 에러 메시지 출력
        if (userdata.Balance < amount)
        {
            Debug.Log("잔액이 부족합니다");
            return;
        }

        // 송금 대상 사용자 찾기
        UserData targetUser = FindUserByName(targetName);
        if (targetUser == null)
        {
            Debug.Log("대상이 없습니다");
            return;
        }

        // 송금 처리
        userdata.SubtractMoney(amount);   // 송금자 잔액 차감
        targetUser.AddMoney(amount);      // 송금 대상자 잔액 추가

        Refresh(); // 송금 후 UI 갱신
        registrationManager.dataManager.SaveUserData(registrationManager.userDataList);//JSON 파일에 저장
        Debug.Log("송금이 완료되었습니다");
    }

    void OnRimittanceExitButtonClicked() // 송금 취소 버튼 클릭 시 처리
    {
        // 송금 팝업을 닫거나 관련 UI 처리 추가
        rimittanceTarger.text = "";
        rimittanceprice.text = "";
    }

    UserData FindUserByName(string name) // 이름으로 사용자 찾기
    {
        foreach (var user in registrationManager.userDataList)
        {
            if (user.Name == name)
            {
                return user;
            }
        }
        return null; // 사용자가 없으면 null 반환
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
      //  SaveUserData(); // 입금후 자동저장
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
    //    SaveUserData();//출금후 자동저장
    }
    
    //public void SaveUserData()// JSON 파일에 데이터 저장
    //{      
    //    string json = JsonUtility.ToJson(userdata, true);// UserData를 JSON으로 직렬화

    //    File.WriteAllText(saveFilePath, json); // JSON 파일로 저장
    //    Debug.Log("유저데이터 저장됨");
    //}

    //public void LoadUserData()// JSON 파일에서 데이터 로드
    //{
    //    if (File.Exists(saveFilePath))
    //    {  
    //        string json = File.ReadAllText(saveFilePath); // JSON 파일 읽기
    //        userdata = JsonUtility.FromJson<UserData>(json); // JSON을 UserData 객체로 변환        
          
    //        Debug.Log("유저데이터 읽음");
    //    }
    //    else
    //    {          
    //        userdata = new UserData("유준영", 100000, 50000, "defaultId", "defaultPassword"); // 파일이 없다면 기본 값으로 초기화
    //        Debug.Log("기존 데이터 없어 기본값 설정");
    //    }
    //}
    void OnErrorButtonClicked()
    {
        popUpError.SetActive(false);
    }
}