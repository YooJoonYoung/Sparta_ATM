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
    //public void RegisterUser(string name, int cash, int balance, string id, string ps)  // ȸ������ �Լ�
    //{
    //    Debug.Log("ȸ������ �õ�: " + id);
    //    if (IsIDDuplicate(id))// ID �ߺ� üũ
    //    {
    //        Debug.Log("�̹� �����ϴ� ID�Դϴ�.");
    //        return; // ID�� �ߺ��Ǹ� ���� ����
    //    }

    //    UserData newUser = new UserData(name, cash, balance, id, ps);// ���ο� ����� ������ �߰�
    //    userDataList.Add(newUser);

    //    dataManager.SaveUserData(userDataList); // �����͸� ���Ͽ� ����

    //    Debug.Log("ȸ�������� �Ϸ�Ǿ����ϴ�.");
    //}

    //// ID �ߺ� Ȯ�� �Լ�
    //private bool IsIDDuplicate(string id)
    //{
    //    foreach (var user in userDataList)
    //    {
    //        if (user.ID == id)
    //        {
    //            return true; // ID�� �ߺ��Ǹ� true ��ȯ
    //        }
    //    }
    //    return false; // �ߺ����� ������ false ��ȯ
    //}
}
