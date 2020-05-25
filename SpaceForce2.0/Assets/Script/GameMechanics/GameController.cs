using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 startPosition;

    private List<GameObject> selectedUnits;

    private List<GameObject> selectableUnits;

    private Vector3 mousePos1;
    private Vector3 mousePos2;


    public LayerMask ClickableObjects;

    public void Awake() {
        selectedUnits = new List<GameObject>();
        selectableUnits = new List<GameObject>();


    }
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        {
           ClearSelection();
        }

        if(Input.GetMouseButtonDown(0))
        {

            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            RaycastHit rayHit;
           
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, ClickableObjects))
            {
                
                UnitController unitControllerScript = rayHit.collider.GetComponent<UnitController>();
                if(Input.GetKey("left ctrl"))
                {
                    if(unitControllerScript.currentlyselected == false)
                    {
                        selectedUnits.Add(rayHit.collider.gameObject);
                        unitControllerScript.currentlyselected = true;
                        unitControllerScript.ClickMe();
                    }
                    else{
                        selectedUnits.Remove(rayHit.collider.gameObject);
                        unitControllerScript.currentlyselected = false;
                        unitControllerScript.ClickMe();
                    }
                }
                else{
                    ClearSelection();
                    selectedUnits.Add(rayHit.collider.gameObject);
                    unitControllerScript.currentlyselected = true;
                    unitControllerScript.ClickMe();
                }
            
            }
        }
    }

    void ClearSelection()
    {
        if(selectedUnits.Count > 0)
        {
            foreach(GameObject obj in selectedUnits)
            {
                obj.GetComponent<UnitController>().currentlyselected = false;
                obj.GetComponent<UnitController>().ClickMe();
            }
            selectedUnits.Clear();
        }                   
    }
}
