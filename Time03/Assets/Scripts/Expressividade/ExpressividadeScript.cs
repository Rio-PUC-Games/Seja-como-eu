using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressividadeScript : MonoBehaviour
{

    public int health = 6;
    private Skills Missile;
    private Skills Pillar;
    private List<Skills> skills;
    private BossSkillsCD skillsCD;
    public float cooldown;
    private bool skillIsReady = false;
    private GeneralCounts counts;
    private ExMissile exmScript;
    private ExPillar expScript;
    public GameObject portalExit;
    private DisplayFrase df;
    private Animator headAnimator;

    // Start is called before the first frame update
    void Start()
    {
        df = GetComponent<DisplayFrase>();
        
        headAnimator = GetComponentInChildren<Animator>();

        counts = SaveSystem.GetInstance().generalCounts;

        skills = new List<Skills>();

        skillsCD = GetComponent<BossSkillsCD>();

        exmScript = GetComponent<ExMissile>();
        Missile = new Skills(exmScript.getProb(), exmScript.getCD(), false, exmScript.Shoot);
        skills.Add(Missile);

        expScript = GetComponent<ExPillar>();
        Pillar = new Skills(expScript.getProb(), expScript.getCD(), false, expScript.InkPillar);
        skills.Add(Pillar);

        StartCoroutine(ResetCooldown());


        HitTentacle.tentacleHit += TakeDamage; //registra a funcao TakeDamage ao evento tentacleHit
    }
    void OnDestroy()
    {
        HitTentacle.tentacleHit -= TakeDamage; //remove a funcao TakeDamage do evento tentacleHit
    }

    // Update is called once per frame
    void Update()
    {
        if (!counts.ExpressividadeIsMorto) {
            counts.ExpressividadeCompleteTimer += Time.deltaTime;
        }

        if(skillIsReady && health > 0)
        {
            skillsCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
        #endif
    }

    private IEnumerator ResetCooldown()
    {
        skillIsReady = false;
        yield return new WaitForSeconds(cooldown);
        skillIsReady = true;
    }

    private void TakeDamage()
    {
        health -=1;
        if (health <= 0)
        {
            headAnimator.SetTrigger("death");
            counts.ExpressividadeIsMorto = true;
            //Destroy(this.gameObject);
            portalExit.SetActive(true);
            expScript.StopAllCoroutines();
            df.Trigger.TriggerConversation(0,"ExpressividadeMorre");
            GetComponent<TentacleManager>().StopAllTents();
        }
        else
        {
            headAnimator.SetTrigger("damage");
            if(health == 4)
            {
                Missile.SwitchReady();
            }
            if(health == 2)
            {
                Pillar.SwitchReady();
            }
        }
    }
}
