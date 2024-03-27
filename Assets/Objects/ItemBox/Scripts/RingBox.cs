using UnityEngine;

[AddComponentMenu("Freedom Engine/Objects/Item Box/RingBox")]
public class RingBox : ItemBox
{
	public int ringAmount;

	protected override void OnCollect(Player player)
	{
		ScoreManager.Instance.Rings += ringAmount;
        if (ScoreManager.Instance.Rings % 100 == 0 && ScoreManager.Instance.Rings > 0)
        {
            ScoreManager.Instance.Lifes++;
        }

        player.stats.boostMeter += 10f;
        player.stats.boostMeter = Mathf.Clamp(player.stats.boostMeter, 0f, 100f);
    }
}
