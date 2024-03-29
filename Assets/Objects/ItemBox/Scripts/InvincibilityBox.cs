using UnityEngine;
using System.Collections;

[AddComponentMenu("Freedom Engine/Objects/Item Box/Invincibility Box")]
public class InvincibilityBox : ItemBox
{
    public AudioSource stageMusicSource;
    public AudioSource invincibilityMusicSource;
    public float time;

    private void Awake()
    {
        stageMusicSource = GameObject.Find("_STAGE_MANAGER_").GetComponent<AudioSource>();
    }

    protected override void OnCollect(Player player)
    {

        // Play the invincibility music
        invincibilityMusicSource.Play();
        player.SetShield(PlayerShields.Invincibility);

        // Lower the volume of the stage music
        stageMusicSource.mute = true;

        StartCoroutine(timer(player));
    }

    public IEnumerator timer(Player player)
    {
        yield return new WaitForSecondsRealtime(time);
        player.SetShield(PlayerShields.None);
        invincibilityMusicSource.Stop();
    }
}
