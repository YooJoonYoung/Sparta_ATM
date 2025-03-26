using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string filePath;

  
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/userdata.json"; // 사용자 데이터가 저장될 경로
    }

    public List<UserData> LoadUserData()// JSON 파일에서 사용자 데이터를 읽어오는 함수
    {
        
        if (File.Exists(filePath))
        {
            Debug.Log("!!");
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<UserDataList>(json).userDataList;
        }
        return new List<UserData>(); // 파일이 없다면 빈 리스트 반환
    }
    
    public void SaveUserData(List<UserData> userDataList)// 사용자 데이터를 JSON 파일에 저장하는 함수
    {
        UserDataList data = new UserDataList { userDataList = userDataList };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}
[System.Serializable]// 여러 사용자를 담기 위한 클래스
public class UserDataList
{
    public List<UserData> userDataList;
}