using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public enum InfoType
	{
		EXP,
		Level,
		Kill,
		Time,
		Health
	}

	public InfoType type;
	private Text myText;
	private Slider mySlider;

	private void Awake()
	{
		myText = GetComponent<Text>();
		mySlider = GetComponent<Slider>();
	}

	private void LateUpdate() // Update�� ������ ���� �� UI ������Ʈ�ϱ� ���� lateUpdate
	{
		switch (type) {
			case InfoType.EXP:
				float currentEXP = GameManager.Instance.EXP;
				float maxEXP = GameManager.Instance.NextEXP[GameManager.Instance.Level];
				mySlider.value = currentEXP / maxEXP;
				break;

			case InfoType.Level:
				myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.Level); // F0 : �Ҽ��� �ڸ��� 0��
				break;

			case InfoType.Kill:
				myText.text = string.Format("{0:F0}", GameManager.Instance.Kill); // F0 : �Ҽ��� �ڸ��� 0��
				break;

			case InfoType.Time:
				float remainTime = GameManager.Instance.MaxGameTime - GameManager.Instance.GameTime;
				int min = Mathf.FloorToInt(remainTime / 60);
				int sec = Mathf.FloorToInt(remainTime % 60);
				myText.text = string.Format("{0:D2}:{1:D2}", min, sec); // D2 : �� �ڸ����� ǥ��
				break;

			case InfoType.Health:
				float currentHealth = GameManager.Instance.Health;
				float maxHealth = GameManager.Instance.MaxHealth;
				mySlider.value = currentHealth / maxHealth;
				break;
			default:
				break;
		}
	}
}
