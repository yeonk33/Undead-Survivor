using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	public ItemData Data;
	public int Level;
	public Weapon Weapon;
	public Gear Gear;

	private Image _icon;
	private Text _textLevel;
	private Text _textName;
	private Text _textDescription;

	private void Awake()
	{
		_icon = GetComponentsInChildren<Image>()[1];
		_icon.sprite = Data.ItemIcon;

		Text[] texts = GetComponentsInChildren<Text>();
		_textLevel = texts[0];
		_textName = texts[1];
		_textDescription = texts[2];
		_textName.text = Data.ItemName;
	}

	private void OnEnable()
	{
		_textLevel.text = "Lv." + (Level + 1);

		switch (Data.ItemTypeValue) {
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:
				_textDescription.text = string.Format(Data.ItemDescription, Data.Damages[Level] * 100, Data.Counts[Level]);
				break;

			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoe:
				_textDescription.text = string.Format(Data.ItemDescription, Data.Damages[Level] * 100);
				break;

			default:
				_textDescription.text = string.Format(Data.ItemDescription);
				break;
		}
	}

	public void OnClick()
	{
		switch (Data.ItemTypeValue) {
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Range:
				if (Level == 0) {
					GameObject newWeapon = new GameObject();
					Weapon = newWeapon.AddComponent<Weapon>();
					Weapon.Init(Data);

				} else {
					float nextDamage = Data.BaseDamage;
					int nextCount = 0;

					nextDamage += Data.BaseDamage * Data.Damages[Level];
					nextCount += Data.Counts[Level];

					Weapon.LevelUp(nextDamage, nextCount);
				}
				Level++;
				break;

			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoe:
				if (Level == 0) {
					GameObject newGear = new GameObject();
					Gear = newGear.AddComponent<Gear>();
					Gear.Init(Data);
				} else {
					float nextRate = Data.Damages[Level];
					Gear.LevelUp(nextRate);
				}
				Level++;
				break;

			case ItemData.ItemType.Heal:
				GameManager.Instance.Health = GameManager.Instance.MaxHealth;
				break;

			default:
				break;
		}
		
		if (Level == Data.Damages.Length) { // 최대 레벨 도달
			GetComponent<Button>().interactable = false;
		}

	}
}
