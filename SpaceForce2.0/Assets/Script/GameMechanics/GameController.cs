using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 startPosition;

    private List<GameObject> selectedUnits;

    public List<GameObject> selectableUnits;

    public List<GameObject> selectedBuildings;

    private Vector3 mousePos1;
    private Vector3 mousePos2;

    private Vector3 mousePos3;


    public LayerMask ClickableUnits;

    public LayerMask ClickableBuildings;

    public void Awake() {
        selectedUnits = new List<GameObject>();
        selectedBuildings = new List<GameObject>();
        selectableUnits = new List<GameObject>();


    }
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        {
            if(selectedUnits.Count > 0)
            {
                foreach(GameObject obj in selectedUnits)
                {
                    obj.GetComponent<UnitController>().moveUnit(new Vector3(Input.mousePosition.x - obj.GetComponent<UnitController>().transform.position.x, obj.GetComponent<UnitController>().transform.position.y, Input.mousePosition.y - obj.GetComponent<UnitController>().transform.position.z));
                }
            }
           //ClearSelection();

        }

        if(Input.GetMouseButtonDown(0))
        {

            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, ClickableUnits))
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
            else if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, ClickableBuildings))
            {
                BuildingController BuildingControllerScript = rayHit.collider.GetComponent<BuildingController>();
                if(Input.GetKey("left ctrl"))
                {
                    if(BuildingControllerScript.currentlyselected == false)
                    {
                        selectedBuildings.Add(rayHit.collider.gameObject);
                        BuildingControllerScript.currentlyselected = true;
                        BuildingControllerScript.ClickMe();
                    }
                    else{
                        selectedBuildings.Remove(rayHit.collider.gameObject);
                        BuildingControllerScript.currentlyselected = false;
                        BuildingControllerScript.ClickMe();
                    }
                }
                else{
                    ClearSelection();
                    selectedBuildings.Add(rayHit.collider.gameObject);
                    BuildingControllerScript.currentlyselected = true;
                    BuildingControllerScript.ClickMe();
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }
    }

    void SelectObjects()
    {
        List<GameObject> remObjects =  new List<GameObject>();
        if(Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach(GameObject selectObject in selectableUnits)
        {
            if(selectObject != null)
            {
                if(selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedUnits.Add(selectObject);
                    selectObject.GetComponent<UnitController>().currentlyselected = true;
                    selectObject.GetComponent<UnitController>().ClickMe();
                }
            }
            else{
                remObjects.Add(selectObject);
            }
        }

        if(remObjects.Count > 0)
        {
            foreach(GameObject rem in remObjects)
            {
                selectableUnits.Remove(rem);
            }
            remObjects.Clear();
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
        else if(selectedBuildings.Count > 0)
        {
            foreach(GameObject obj in selectedBuildings)
            {
                obj.GetComponent<BuildingController>().currentlyselected = false;
                obj.GetComponent<BuildingController>().ClickMe();
            }
            selectedBuildings.Clear();
        }                   
    }
}
