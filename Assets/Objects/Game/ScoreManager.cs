﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text;
using TMPro;

[AddComponentMenu("Freedom Engine/Game/Score Manager")]
public class ScoreManager : MonoBehaviour
{
	private static ScoreManager instance;

	public static ScoreManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<ScoreManager>();
				instance.StartSingleton();
			}

			return instance;
		}
	}

	[Header("Score UI")]
	[SerializeField] private TMP_Text timeCounter = null;
	[SerializeField] private TMP_Text ringCounter = null;
	[SerializeField] private TMP_Text lifeCounter = null;
	[SerializeField] private TMP_Text scoreCounter = null;
    [SerializeField] private TMP_Text finalScoreCounter = null;
    [SerializeField] private TMP_Text ringBonusCounter = null;
    [SerializeField] private TMP_Text timeBonusCounter = null;
    [SerializeField] private Image fader = null;

	[Header("Game Over UI")]
	[SerializeField] private float gameOverFadeTime = 1f;
	[SerializeField] private GameObject gameOverUI = null;
	[SerializeField] private AudioClip gameOverJingle = null;
	[SerializeField] private string gameOverScene = "";

	private int milliseconds;
	private int seconds;
	private int minutes;

	private int rings;
	private int lifes;
	private int score;
	private int finalScore;
	private int ringBonus;
	private int timeBonus;

	public float time { get; set; }
	public bool stopTimer { get; set; }

	private static readonly string[] digits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", "." };
	private static readonly StringBuilder timer = new StringBuilder(8);

	public int Rings
	{
		get { return rings; }
		set 
		{
			rings = value;
			ringCounter.text = rings.ToString("D3");
		}
	}

	public int Lifes
	{
		get { return lifes; }
		set
		{
			lifes = value;
			lifeCounter.text = lifes.ToString("D2");
		}
	}

	public int Score
	{
		get { return score; }
		set
		{
			score = value;
			scoreCounter.text = score.ToString();
		}
	}

	public int FinalScore
	{
		get { return finalScore; }
		set
		{
			finalScore = value;
			finalScoreCounter.text = finalScore.ToString();
		}
	}

    public int RingBonus
    {
        get { return ringBonus; }
        set
        {
            ringBonus = value;
            ringBonusCounter.text = ringBonus.ToString();
        }
    }

    public int TimeBonus
    {
        get { return timeBonus; }
        set
        {
            timeBonus = value;
            timeBonusCounter.text = timeBonus.ToString();
        }
    }
    private void StartSingleton()
	{
		Lifes = 3;
		time = 0;
		score = 0;
	}

	private void Update()
	{
		if (!stopTimer)
		{
			time += Time.deltaTime;
			var oldMillisecond = milliseconds;

			minutes = (int)(time / 60f) % 60;
			seconds = (int)(time % 60);
			milliseconds = (int)(time * 100f) % 100;

			if (milliseconds != oldMillisecond)
			{
				timer.Length = 0;
				timer.Append(digits[minutes / 10]);
				timer.Append(digits[minutes % 10]);
				timer.Append(digits[10]);
				timer.Append(digits[seconds / 10]);
				timer.Append(digits[seconds % 10]);
				timer.Append(digits[11]);
				timer.Append(digits[milliseconds / 10]);
				timer.Append(digits[milliseconds % 10]);
				timeCounter.text = timer.ToString();
			}
		}
	}

	public void ResetTimer()
	{
		time = minutes = seconds = milliseconds = 0;
		timeCounter.text = "00:00.00";
	}

	public void Die()
	{
		stopTimer = true;

		if (lifes > 0)
		{
			StageManager.Instance.Restart();
		}
		else
		{
			StartCoroutine(GameOver());
		}
	}

	private IEnumerator GameOver()
	{
		gameOverUI.SetActive(true);
		StageManager.Instance.ChangeSong(gameOverJingle, 0.5f);

		yield return new WaitForSeconds(gameOverJingle.length);

		var elapsedTime = 0f;
		var faderColor = fader.color;

		while (elapsedTime < gameOverFadeTime)
		{
			elapsedTime += Time.deltaTime;

			var alpha = elapsedTime / gameOverFadeTime;
			faderColor.a = Mathf.Lerp(0, 1, alpha);
			fader.color = faderColor;

			yield return null;
		}

		SceneManager.LoadScene(gameOverScene);
	}
}
