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
    //public void RegisterUser(string name, int cash, int balance, string id, string ps)  // 회원가입 함수
    //{
    //    Debug.Log("회원가입 시도: " + id);
    //    if (IsIDDuplicate(id))// ID 중복 체크
    //    {
    //        Debug.Log("이미 존재하는 ID입니다.");
    //        return; // ID가 중복되면 가입 실패
    //    }

    //    UserData newUser = new UserData(name, cash, balance, id, ps);// 새로운 사용자 데이터 추가
    //    userDataList.Add(newUser);

    //    dataManager.SaveUserData(userDataList); // 데이터를 파일에 저장

    //    Debug.Log("회원가입이 완료되었습니다.");
    //}

    //// ID 중복 확인 함수
    //private bool IsIDDuplicate(string id)
    //{
    //    foreach (var user in userDataList)
    //    {
    //        if (user.ID == id)
    //        {
    //            return true; // ID가 중복되면 true 반환
    //        }
    //    }
    //    return false; // 중복되지 않으면 false 반환
    //}
}
