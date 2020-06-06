﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caringo : MonoBehaviour
{
    public float Cooldown;
    public GameObject Player;
    private NewBossSkillCD SkillCD;
    private bool SkillIsReady = false;
    private List<Skills> skills;
    private Skills Shoot;
    private Skills Teleport;
    private CrazyShoot CSScript;
    private CrazyTeleport CTScript;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;

        skills = new List<Skills>();

        SkillCD = GetComponent<NewBossSkillCD>();

        CSScript = GetComponent<CrazyShoot>();
        Shoot = new Skills(100,CSScript.Cooldown,true,CSScript.Shoot);
        skills.Add(Shoot);

        CTScript = GetComponent<CrazyTeleport>();
        Teleport = new Skills(100,CTScript.Cooldown,true,CTScript.Teleport);
        skills.Add(Teleport);

        StartCoroutine(ResetCooldown());
    }

    
    void Update()
    {
        if(SkillIsReady) {
            SkillCD.ChooseSkill(skills);
            StartCoroutine(ResetCooldown());
        }
        transform.LookAt(Player.transform.position);
        
    }

    private IEnumerator ResetCooldown() {
        SkillIsReady = false;
        yield return new WaitForSeconds(Cooldown);
        SkillIsReady = true;
    }
}
