﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleManager : MonoBehaviour
{
    public GameObject tentaclePrefab;
    public GameObject paintingTentaclePrefab;
    private GameObject[] allTentacles;
    private GameObject paintingTentacle;
    private int currentlyPainting;
    private Transform currentlyPaintingTransform;
    private Transform paintingTentacleTransform;
    private Vector3 hiddenLocation;
    private Vector3 myPosition;

    void Start()
    {
        Transform _T = GetComponent<Transform>();
        myPosition = _T.position;
        hiddenLocation = new Vector3(-20,-20,-20);
        allTentacles = new GameObject[8];
        for(int i=0;i<8;i++)
        {
           GameObject tent = Instantiate(tentaclePrefab, //gameObject a ser instanciado
           myPosition,            //posicao
           Quaternion.LookRotation(new Vector3(Mathf.Sin(i*Mathf.PI*0.25f+Mathf.PI/8),0,Mathf.Cos(i*Mathf.PI*0.25f+Mathf.PI/8)),_T.up)  //rotacao
           ,_T);                 //objeto pai

            allTentacles[i]=tent;
        }
        HitTentacle.tentacleHit += SwapTentacles; //registra a funcao ao evento

        
        currentlyPainting = (int)Random.Range(0.0f,7.9999f); //escolhe o tentaculo que vai pintar
        currentlyPaintingTransform = allTentacles[currentlyPainting].GetComponent<Transform>();
        paintingTentacle = Instantiate(paintingTentaclePrefab,currentlyPaintingTransform.position,currentlyPaintingTransform.rotation,_T); //spawna o tentaculo pintando
        paintingTentacleTransform = paintingTentacle.GetComponent<Transform>();
        currentlyPaintingTransform.position = hiddenLocation; //esconde o tentaculo original
    }
    void OnDestroy() 
    {
        HitTentacle.tentacleHit -= SwapTentacles; //remove a funcao do evento
    }

    private void SwapTentacles()
    {
        paintingTentacleTransform.position = hiddenLocation;
        StartCoroutine(ChangeTentacles());
    }

    private IEnumerator ChangeTentacles()
    {
        yield return new WaitForSeconds(1);
        currentlyPaintingTransform.position = myPosition;                                       //traz o tentaculo escondido de volta
        currentlyPainting = (currentlyPainting + (int)Random.Range(1.0f,6.9999f))%8;            //sorteia novo tentaculo 
        currentlyPaintingTransform = allTentacles[currentlyPainting].GetComponent<Transform>(); //pega seu Transform
        paintingTentacleTransform.rotation = (currentlyPaintingTransform.rotation);             //traz o tentaculo pintando para sua posicao e rotacao
        paintingTentacleTransform.position = myPosition;
        currentlyPaintingTransform.position = hiddenLocation;                                   //esconde o tentaculo sorteado
    }

    public void StopAllTents()
    {
        for(int i=0; i<8; i++)
        {
            allTentacles[i].SetActive(false);
        }
        paintingTentacle.SetActive(false);
    }

}
