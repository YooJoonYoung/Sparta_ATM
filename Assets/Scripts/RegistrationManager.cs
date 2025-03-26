using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationManager : MonoBehaviour
{
    public DataManager dataManager;
    public List<UserData> userDataList;

    private void Start()
    {
        dataManager = GetComponent<DataManager>();
        userDataList = dataManager.LoadUserData(); // ���� ����� ������ �ε�
    }
    public UserData FindUserByCredentials(string id, string password)//�Էµ� ������ ����� ã�� �Լ�
    {
        
        foreach (var user in userDataList)// userDataList���� ID�� ��й�ȣ�� ��ġ�ϴ� ����� ã��
        {
            if (user.ID == id && user.PS == password)
            {
                return user;
            }
        }
        return null; // ��ġ�ϴ� ����ڰ� ������ null ��ȯ
    }
  
}
