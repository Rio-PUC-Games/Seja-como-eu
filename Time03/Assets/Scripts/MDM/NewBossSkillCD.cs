﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossSkillCD : MonoBehaviour
{
    
    public void ChooseSkill(List<Skills> skills) {
        int ReturnCount = 0;
        foreach(Skills s in skills) {
            if(!s.IsSkillReady())
                ReturnCount++;
        }
        if(ReturnCount == skills.Count)
            return;

        while(true) {
            int id = Random.Range(0,skills.Count);
            if(skills[id].IsSkillReady()) {
                skills[id].ActivateSkill();
                StartCoroutine(ActiveCoolDown(skills[id]));
                return; 
            }
        }
    }

    private IEnumerator ActiveCoolDown(Skills s) {
        s.SwitchReady();
        yield return new WaitForSeconds(s.GetCoolDown());
        s.SwitchReady();
    }
}
