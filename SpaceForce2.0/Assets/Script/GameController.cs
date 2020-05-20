using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public LayerMask ClickableObjects;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
           if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, ClickableObjects))
            {
                rayHit.collider.GetComponent<UnitController>().ClickMe();
            }
        }
    }
}
