using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UserData userdata;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

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
    }

    void Start()
    {
        userdata = new UserData("���ؿ�", 100000, 50000);
        Refresh(); //�ʱ� UI ������Ʈ
    }

    public void Refresh()
    {
        // UI�� �̸�, ����, �ܾ��� ������Ʈ
        nameText.text = userdata.Name;
        cashText.text = string.Format("{0:N0}", userdata.Cash);  // õ ���� �޸�
        balanceText.text = string.Format("{0:N0}", userdata.Balance);  // õ ���� �޸�
    }

    // �����͸� ������ �޼��� (���÷� ����ڰ� ������ ������ ��)
    public void UpdateUserData(string name, int cash, int balance)
    {
        userdata.Name = name;
        userdata.Cash = cash;
        userdata.Balance = balance;
        Refresh();  // �����Ͱ� ����Ǿ����� UI ����
    }

    public void Add(int amount)
    {
        userdata.AddMoney(amount);
        Refresh();
    }
    public void Sub(int amount)
    {
        userdata.SubtractMoney(amount);
        Refresh();
    }
}