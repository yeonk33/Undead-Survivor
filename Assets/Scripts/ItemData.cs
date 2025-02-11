using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
	public enum ItemType
	{
		Melee,	// 근접
		Range,	// 원거리
		Glove,	// 장갑
		Shoe,	// 신발
		Heal,	// 회복
	}

	[Header("# Main Info")]
	public ItemType ItemTypeValue;
	public int ItemId;
	public string ItemName;
	public string ItemDescription;
	public Sprite ItemIcon;

	[Header("# Level Data")]
	public float BaseDamage;
	public int BaseCount;
	public float[] Damages;
	public int[] Counts;

	[Header("# Weapon")]
	public GameObject Projectile;
	public Sprite Hand;
}
