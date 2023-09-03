using System.Collections;
using UnityEngine;

[AddComponentMenu("Freedom Engine/Objects/Goal Plate")]
public class GoalPlate : FreedomObject
{
    public AudioClip plateClip;
    public Animator animator;
    private bool activated;
    [SerializeField] private int delay;

    private new AudioSource audio;
    [SerializeField] private AudioClip endTheme;

    private IEnumerator endscene;


    private void Start()
    {
        if (!TryGetComponent(out audio))
        {
            audio = gameObject.AddComponent<AudioSource>();
        }
    }

    public override void OnRespawn()
    {
        activated = false;
        animator.SetBool("Activated", false);
    }

    private IEnumerator endscreen()
    {
        yield return new WaitForSeconds(delay);

        StageManager.Instance.FinishStage();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            animator.SetBool("Activated", true);
            audio.PlayOneShot(plateClip, 0.5f);

            ScoreManager.Instance.stopTimer = true;
            StageManager.Instance.ChangeSong(endTheme, 1, false);

            StageManager.Instance.scoreCard.SetActive(false);
            StageManager.Instance.stageClearCard.SetActive(true);

            ScoreManager.Instance.RingBonus += ScoreManager.Instance.Rings * 100;

            //score bonus Sonic 1 style
            if(ScoreManager.Instance.time < 29)
            {
                ScoreManager.Instance.TimeBonus += 50000;
            } 
            else if(ScoreManager.Instance.time > 30 ||  ScoreManager.Instance.time < 44)
            {
                ScoreManager.Instance.TimeBonus += 10000;
            }
            else if (ScoreManager.Instance.time > 45 || ScoreManager.Instance.time < 59)
            {
                ScoreManager.Instance.TimeBonus += 5000;
            }
            else if (ScoreManager.Instance.time > 60 || ScoreManager.Instance.time < 89)
            {
                ScoreManager.Instance.TimeBonus += 4000;
            }
            else if (ScoreManager.Instance.time > 90 || ScoreManager.Instance.time < 119)
            {
                ScoreManager.Instance.TimeBonus += 3000;
            }
            else if (ScoreManager.Instance.time > 120 || ScoreManager.Instance.time < 179)
            {
                ScoreManager.Instance.TimeBonus += 2000;
            }
            else if (ScoreManager.Instance.time > 180 || ScoreManager.Instance.time < 239)
            {
                ScoreManager.Instance.TimeBonus += 1000;
            }
            else if (ScoreManager.Instance.time > 240 || ScoreManager.Instance.time < 299)
            {
                ScoreManager.Instance.TimeBonus += 500;
            }
            else if (ScoreManager.Instance.time > 300)
            {
                ScoreManager.Instance.TimeBonus += 0;
            }

            ScoreManager.Instance.FinalScore = ScoreManager.Instance.Score + ScoreManager.Instance.RingBonus + ScoreManager.Instance.TimeBonus;

            endscene = endscreen();
            StartCoroutine(endscene);
        }
    }
}