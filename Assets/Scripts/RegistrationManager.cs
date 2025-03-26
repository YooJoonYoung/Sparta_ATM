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
        userDataList = dataManager.LoadUserData(); // 기존 사용자 데이터 로드
    }
    public UserData FindUserByCredentials(string id, string password)//입력된 정보로 사용자 찾는 함수
    {
        
        foreach (var user in userDataList)// userDataList에서 ID와 비밀번호가 일치하는 사용자 찾기
        {
            if (user.ID == id && user.PS == password)
            {
                return user;
            }
        }
        return null; // 일치하는 사용자가 없으면 null 반환
    }
  
}
