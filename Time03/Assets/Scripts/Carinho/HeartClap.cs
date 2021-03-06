﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartClap : MonoBehaviour
{
    [Range(0, 100)]
    public float Probabilidade;

    public float CoolDown;

    public float windup;
    public float hugRadius;

    public GameObject player;

    private ThrowFeno eCol;
    private Transform _t;
    private Rigidbody _rb;
    private UnityEngine.AI.NavMeshAgent agent;

    private Animator anim;

    void Start()
    {
        _t = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        eCol = player.GetComponent<ThrowFeno>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        
    }

    private IEnumerator HHug(){

    	Collider[] hitColliders;
    	bool inBox = false;
    	
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    	

        anim.SetTrigger("Hug 1");

    	yield return new WaitForSeconds(windup); //tempo esticando os bracos

        anim.SetTrigger("Hug");

        yield return new WaitForSeconds(0.15f); //atraso da animacao

    	int i = 0;

    	hitColliders = Physics.OverlapBox(_t.position + _t.forward * hugRadius/2f + _t.up * hugRadius/2, new Vector3(hugRadius,hugRadius/2f,hugRadius/2f), _t.rotation);
    	while( i < hitColliders.Length)
    	{
    		if(hitColliders[i].tag == "Player")
    		{
    			inBox = true;
    			break;
    		}
    		i+=1;
    	}

    	if(inBox)
    	{
	    	hitColliders = Physics.OverlapSphere(_t.position, hugRadius);
	        while (i < hitColliders.Length)
	        {
	        	if(hitColliders[i].tag == "Player")
	        	{
	        		
	            	GeneralCounts.Kill = true;
	            	break;
	            }
	            i+=1;
	        }
	    }

        agent.enabled = true; //reativa agente do carinho

    }

	void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hugRadius);
    }

    public void Clap() {
        agent.enabled = false; //desativa agente do carinho
    	StartCoroutine(HHug());
        Debug.Log("Clap!");
    }

    public float getProb()
    {
    	return Probabilidade;
    }

    public float getCD()
    {
    	return CoolDown;
    }
}
