using UnityEngine;

[AddComponentMenu("Freedom Engine/Objects/Item Box/OneUp")]
public class OneUpBox : ItemBox
{
	protected override void OnCollect(Player player)
	{
		ScoreManager.Instance.Lifes++;
		ScoreManager.Instance.Score += 50000;
	}
}
