using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerAudio", menuName = "Freedom Engine/Player Audio", order = 1)]
public class PlayerAudio : ScriptableObject
{
	public AudioClip jump;
	public AudioClip brake;
	public AudioClip spin;
	public AudioClip spinDashCharge;
	public AudioClip spinDashRelease;
	public AudioClip ringLoss;
	public AudioClip dead;
	public AudioClip boostStart;
    public AudioClip stompLand;
	public AudioClip invincibilityMusic;
}