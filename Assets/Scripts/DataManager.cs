using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string filePath;

  
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/userdata.json"; // ����� �����Ͱ� ����� ���
    }

    public List<UserData> LoadUserData()// JSON ���Ͽ��� ����� �����͸� �о���� �Լ�
    {
        
        if (File.Exists(filePath))
        {
            Debug.Log("!!");
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<UserDataList>(json).userDataList;
        }
        return new List<UserData>(); // ������ ���ٸ� �� ����Ʈ ��ȯ
    }
    
    public void SaveUserData(List<UserData> userDataList)// ����� �����͸� JSON ���Ͽ� �����ϴ� �Լ�
    {
        UserDataList data = new UserDataList { userDataList = userDataList };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}
[System.Serializable]// ���� ����ڸ� ��� ���� Ŭ����
public class UserDataList
{
    public List<UserData> userDataList;
}