using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public Animator animator;

    public GameObject victoryZone;
    public float waitToShowExit;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    public enum BossPhase
    {
        intro,
        phase1,
        end
    };

    public BossPhase currentPhase = BossPhase.intro;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isRespawning)
        {
            currentPhase = BossPhase.intro;
            animator.SetBool("Phase1", false);

            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

            gameObject.SetActive(false);
            
            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(true);

            GameManager.instance.isRespawning = false;
        }
    }

    public void DamageBoss()
    {
        AudioManager.instance.PlaySFX(bossHit);

        currentPhase++;

        if(currentPhase != BossPhase.end)
        {
            animator.SetTrigger("Hurt");
        }

        switch(currentPhase)
        {
            case BossPhase.phase1:
                animator.SetBool("Phase1", true);
                break;

            case BossPhase.end:
                animator.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;
        }
    }

    IEnumerator EndBoss()
    {
        AudioManager.instance.PlaySFX(bossDeath);
        AudioManager.instance.PlaySFX(bossDeathShout);
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

        yield return new WaitForSeconds(waitToShowExit);
        victoryZone.SetActive(true);

    }
}
