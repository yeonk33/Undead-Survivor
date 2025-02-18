using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
	public GameObject[] LockCharacters;
	public GameObject[] UnlockCharacters;
	public GameObject UiNotice;

	enum Achive { UnlockPotato, UnlockBean }	// 이 순서와 Notice 아래의 해금알림 순서가 동일해야함.
	Achive[] achives;
	WaitForSecondsRealtime wait;

	private void Awake()
	{
		achives = (Achive[])Enum.GetValues(typeof(Achive));
		wait = new WaitForSecondsRealtime(5f);

		if (!PlayerPrefs.HasKey("MyData")) {	// 게임 재시작시 초기화되는 것을 막기 위해
			Init();
		}
	}

	void Init()
	{
		PlayerPrefs.SetInt("MyData", 1);

		foreach (var item in achives) {	// 모든 achive 0으로 초기화
			PlayerPrefs.SetInt(item.ToString(), 0);	// 0: 해금 안됨
		}
	}

	private void Start()
	{
		UnlockCharacter();
	}

	private void UnlockCharacter()
	{
		for (int i = 0; i < LockCharacters.Length; i++) {
			string achiveName = achives[i].ToString();
			bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
			LockCharacters[i].SetActive(!isUnlock);
			UnlockCharacters[i].SetActive(isUnlock);
		}
	}

	private void LateUpdate()
	{
		foreach (var item in achives) {
			CheckAchive(item);
		}
	}

	private void CheckAchive(Achive achive)
	{
		bool isAchive = false;

		switch (achive) {
			case Achive.UnlockPotato:   // 10킬시 해제
				isAchive = GameManager.Instance.Kill >= 10;
				break;

			case Achive.UnlockBean: // 생존 성공시
				isAchive = GameManager.Instance.GameTime == GameManager.Instance.MaxGameTime;
				break;

			default:
				break;
		}

		if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) {
			PlayerPrefs.SetInt(achive.ToString(), 1);

			for (int i = 0; i < UiNotice.transform.childCount; i++) {
				bool isActive = i == (int)achive;
				UiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
			}

			StartCoroutine(NoticeRoutine());
		}
	}

	IEnumerator NoticeRoutine()
	{
		UiNotice.SetActive(true);	// 활성화
		yield return wait;			// 5초뒤에
		UiNotice.SetActive(false);	// 비활성화
	}
}
