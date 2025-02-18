using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
	public GameObject[] LockCharacters;
	public GameObject[] UnlockCharacters;
	public GameObject UiNotice;

	enum Achive { UnlockPotato, UnlockBean }	// �� ������ Notice �Ʒ��� �رݾ˸� ������ �����ؾ���.
	Achive[] achives;
	WaitForSecondsRealtime wait;

	private void Awake()
	{
		achives = (Achive[])Enum.GetValues(typeof(Achive));
		wait = new WaitForSecondsRealtime(5f);

		if (!PlayerPrefs.HasKey("MyData")) {	// ���� ����۽� �ʱ�ȭ�Ǵ� ���� ���� ����
			Init();
		}
	}

	void Init()
	{
		PlayerPrefs.SetInt("MyData", 1);

		foreach (var item in achives) {	// ��� achive 0���� �ʱ�ȭ
			PlayerPrefs.SetInt(item.ToString(), 0);	// 0: �ر� �ȵ�
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
			case Achive.UnlockPotato:   // 10ų�� ����
				isAchive = GameManager.Instance.Kill >= 10;
				break;

			case Achive.UnlockBean: // ���� ������
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
		UiNotice.SetActive(true);	// Ȱ��ȭ
		yield return wait;			// 5�ʵڿ�
		UiNotice.SetActive(false);	// ��Ȱ��ȭ
	}
}
