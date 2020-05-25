﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public Material selected;
    public Material notSelected;

    private MeshRenderer myRend;

    public Vector3 startpos;

    private NavMeshAgent navAgent;


    public bool currentlyselected = false;
    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<GameController>().selectableUnits.Add(this.gameObject);
        ClickMe();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame

    public void ClickMe()
    {
        if(currentlyselected == false)
        {
            myRend.material = notSelected;
        }
        else{
            myRend.material = selected;
        }
    }

    public void moveUnit(Vector3 dest)
    {
        navAgent.Warp(dest);
        navAgent.destination = dest;
    }

    
}
