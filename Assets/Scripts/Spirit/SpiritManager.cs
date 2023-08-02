using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
	public static SpiritManager instance;

	[SerializeField] private GameObject spiritPrefab;
	[SerializeField] private List<Trait> traits;
	public List<Transform> movementPoints;

	[HideInInspector] public Spirit activeSpirit;

	private void Start()
	{
		instance = this;
	}

	public IEnumerator GenerateSpirit(int day, int index)
	{
		// `day` starts from 1.
		Trait trait = traits[(day - 1) * GameManager.instance.limit + index];
		GameObject spiritObject = Instantiate(spiritPrefab);
		Spirit spirit = spiritObject.GetComponent<Spirit>();
		TransferTransform(spirit, movementPoints[0]); // Move to spawn point.
		spirit.GetComponent<SpriteRenderer>().sprite = trait.sprite;
		spirit.realName = trait.realName;
		spirit.description = trait.description;
		spirit.demise = trait.demise;
		spirit.dialog = trait.dialog;
		yield return spirit.GreetingSequence();
	}

	public void TransferTransform(Spirit s, Transform t)
    {
		s.transform.SetPositionAndRotation(t.position, t.rotation);
		s.transform.localScale = t.localScale;
    }

	[System.Serializable] private struct Trait
	{
		public Sprite sprite;
		public string realName, description, demise;
		public List<string> dialog;
	}
}
