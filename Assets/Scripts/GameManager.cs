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
        saveFilePath = Application.persistentDataPath + "/userdata.json"; //JSON ���� ���� ��� �����ϱ�
    }

    void Start()
    {

        //userdata = new UserData("���ؿ�", 100000, 50000);
        LoadUserData();
       // Refresh(); //�ʱ� UI ������Ʈ
        popUpError.SetActive(false);
        errorButton.onClick.AddListener(OnErrorButtonClicked);
    }

    public void Refresh()
    {
        // UI�� �̸�, ����, �ܾ��� ������Ʈ
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // õ ���� �޸�
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // õ ���� �޸�
    }

    // �����͸� ������ �޼��� (���÷� ����ڰ� ������ ������ ��)
    //public void UpdateUserData(string name, int cash, int balance)
    //{
    //    userdata.Name = name;
    //    userdata.Cash = cash;
    //    userdata.Balance = balance;
    //    Refresh();  // �����Ͱ� ����Ǿ����� UI ����
    //}

    public void Add(int amount)
    {
        userdata.AddMoney(amount);
        Refresh();
        SaveUserData(); // �Ա��� �ڵ�����
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
        SaveUserData();//����� �ڵ�����
    }
    
    public void SaveUserData()// JSON ���Ͽ� ������ ����
    {      
        string json = JsonUtility.ToJson(userdata, true);// UserData�� JSON���� ����ȭ

        File.WriteAllText(saveFilePath, json); // JSON ���Ϸ� ����
        Debug.Log("���������� �����");
    }

    public void LoadUserData()// JSON ���Ͽ��� ������ �ε�
    {
        if (File.Exists(saveFilePath))
        {  
            string json = File.ReadAllText(saveFilePath); // JSON ���� �б�
            userdata = JsonUtility.FromJson<UserData>(json); // JSON�� UserData ��ü�� ��ȯ        
            Refresh(); // UI ����
            Debug.Log("���������� ����");
        }
        else
        {          
            userdata = new UserData("���ؿ�", 100000, 50000); // ������ ���ٸ� �⺻ ������ �ʱ�ȭ
            Debug.Log("���� ������ ���� �⺻�� ����");
        }
    }
    void OnErrorButtonClicked()
    {
        popUpError.SetActive(false);
    }
}