using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //JSON�۾��� ���� ���ӽ����̽�(����� ���ϰ����� ���� �߰�)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userdata;

    //UI �ؽ�Ʈ
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    //���� �޽��� UI
    public GameObject popUpError;
    public Button errorButton;
    public GameObject popSignUpError;
    public Button popSignUpErrorButton;
    public GameObject iDErrorMSG;
    public GameObject pSErrorMSG;
    // �α��� �˾�
    public GameObject popupLogin;
    public GameObject popupBank;
    public GameObject popupSignUp; //ȸ������ �˾�

    // �α��� UI ���
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button signupButton;

    // ȸ������ UI ���
    public TMP_InputField signUpIdInputField;
    public TMP_InputField signUpNameInputField;
    public TMP_InputField signUpPasswordInputField;
    public TMP_InputField signUpPasswordConfirmInputField;
    public Button signUpButton;
    public Button signUpCancelButton;

    //�۱� UI ���
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
        saveFilePath = Application.persistentDataPath + "/userdata.json"; //JSON ���� ���� ��� �����ϱ�
    }

    void Start()
    {
        // �α��� �˾� ���� ǥ��
        popupLogin.SetActive(true);
        popupBank.SetActive(false);
        popupSignUp.SetActive(false);
        popSignUpError.SetActive(false);

        // �α��� ��ư Ŭ�� �� ȣ��
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        signupButton.onClick.AddListener(OnSignupButtonClicked);
       

        // ���� ��ư Ŭ�� �� �˾� �ݱ�
        popUpError.SetActive(false);
        errorButton.onClick.AddListener(OnErrorButtonClicked);
        popSignUpErrorButton.onClick.AddListener(OnpopSignupButtonClicked);

        //ȸ������ ��ư Ŭ���� ȣ��
        signUpButton.onClick.AddListener(OnSignUpConfirmClicked);
        signUpCancelButton.onClick.AddListener(OnSignUpCancelClicked);

        //�۱� ��ɹ�ư Ŭ���� ȣ��
        rimittancebutton.onClick.AddListener(OnRimittanceButtonClicked);
        rimittanceExitButton.onClick.AddListener(OnRimittanceExitButtonClicked);

        // ���� ���� �� ����� ������ �ε�
        //  LoadUserData();
    }
    // �α��� ��ư Ŭ�� ó��
    void OnLoginButtonClicked()
    {
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;
        UserData foundUser = registrationManager.FindUserByCredentials(inputId, inputPassword);//����� ����Ʈ���� ����Ȯ��

        //if (userdata.CheckCredentials(inputId, inputPassword))
        if(foundUser !=null)
        {
            // �α��� ����: PopupLogin ����� PopupBank ǥ��
            userdata = foundUser; //�α��ε� ����ڵ�����
            popupLogin.SetActive(false);
            popupBank.SetActive(true);
            Refresh();
        }
        else
        {
            // �α��� ����: ���� �޽��� ǥ��
            popUpError.SetActive(true);
             Debug.Log("�α��� ����");
        }
    }

    // ȸ������ ��ư Ŭ�� ó��
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

        // ��ĭ üũ
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            popSignUpError.SetActive(true);
            iDErrorMSG.SetActive(true);
            return;
        }

        // ��й�ȣ Ȯ��
        if (password != passwordConfirm)
        {
            popSignUpError.SetActive(true);
            pSErrorMSG.SetActive(true);
            return;
        }

        // UserData ��ü ����
        userdata = new UserData(name, 100000, 50000, id, password);

        // ����� ������ ����
      //  SaveUserData();
      registrationManager.userDataList.Add(userdata);
        registrationManager.dataManager.SaveUserData(registrationManager.userDataList);

        // ȸ������ �Ϸ� �� �α��� ȭ������ ���ư���
        popupSignUp.SetActive(false);
        popupLogin.SetActive(true);
    }
    
    // ȸ������ ��� ��ư Ŭ�� ��
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
  
    void OnRimittanceButtonClicked() // �۱� ��ư Ŭ�� ó��
    {
        string targetName = rimittanceTarger.text; // �۱� ��� �̸�
        string amountText = rimittanceprice.text;   // �۱� �ݾ�

        // �۱� ��� �̸� �Ǵ� �ݾ��� ��������� ���� �޽��� ���
        if (string.IsNullOrEmpty(targetName) || string.IsNullOrEmpty(amountText))
        {
            Debug.Log("�Է������� Ȯ�����ּ���");
            return;
        }

        int amount;
        if (!int.TryParse(amountText, out amount) || amount <= 0)
        {
            Debug.Log("�ݾ��� �ùٸ��� �Է����ּ���");
            return;
        }

        // �ܾ��� �����ϸ� ���� �޽��� ���
        if (userdata.Balance < amount)
        {
            Debug.Log("�ܾ��� �����մϴ�");
            return;
        }

        // �۱� ��� ����� ã��
        UserData targetUser = FindUserByName(targetName);
        if (targetUser == null)
        {
            Debug.Log("����� �����ϴ�");
            return;
        }

        // �۱� ó��
        userdata.SubtractMoney(amount);   // �۱��� �ܾ� ����
        targetUser.AddMoney(amount);      // �۱� ����� �ܾ� �߰�

        Refresh(); // �۱� �� UI ����
        registrationManager.dataManager.SaveUserData(registrationManager.userDataList);//JSON ���Ͽ� ����
        Debug.Log("�۱��� �Ϸ�Ǿ����ϴ�");
    }

    void OnRimittanceExitButtonClicked() // �۱� ��� ��ư Ŭ�� �� ó��
    {
        // �۱� �˾��� �ݰų� ���� UI ó�� �߰�
        rimittanceTarger.text = "";
        rimittanceprice.text = "";
    }

    UserData FindUserByName(string name) // �̸����� ����� ã��
    {
        foreach (var user in registrationManager.userDataList)
        {
            if (user.Name == name)
            {
                return user;
            }
        }
        return null; // ����ڰ� ������ null ��ȯ
    }

    public void Refresh()
    {
        // UI�� �̸�, ����, �ܾ��� ������Ʈ
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // õ ���� �޸�
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // õ ���� �޸�
    }

   
    public void Add(int amount)
    {
        userdata.AddMoney(amount);
        Refresh();
      //  SaveUserData(); // �Ա��� �ڵ�����
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
    //    SaveUserData();//����� �ڵ�����
    }
    
    //public void SaveUserData()// JSON ���Ͽ� ������ ����
    //{      
    //    string json = JsonUtility.ToJson(userdata, true);// UserData�� JSON���� ����ȭ

    //    File.WriteAllText(saveFilePath, json); // JSON ���Ϸ� ����
    //    Debug.Log("���������� �����");
    //}

    //public void LoadUserData()// JSON ���Ͽ��� ������ �ε�
    //{
    //    if (File.Exists(saveFilePath))
    //    {  
    //        string json = File.ReadAllText(saveFilePath); // JSON ���� �б�
    //        userdata = JsonUtility.FromJson<UserData>(json); // JSON�� UserData ��ü�� ��ȯ        
          
    //        Debug.Log("���������� ����");
    //    }
    //    else
    //    {          
    //        userdata = new UserData("���ؿ�", 100000, 50000, "defaultId", "defaultPassword"); // ������ ���ٸ� �⺻ ������ �ʱ�ȭ
    //        Debug.Log("���� ������ ���� �⺻�� ����");
    //    }
    //}
    void OnErrorButtonClicked()
    {
        popUpError.SetActive(false);
    }
}