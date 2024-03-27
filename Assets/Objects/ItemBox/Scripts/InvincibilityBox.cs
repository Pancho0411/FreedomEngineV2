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
        player.attacking = true;

        // Lower the volume of the stage music
        stageMusicSource.mute = true;

        StartCoroutine(timer());
    }

    public IEnumerator timer()
    {
        yield return new WaitForSecondsRealtime(time);
        stageMusicSource.mute = false;
        invincibilityMusicSource.mute = true;
    }
}
