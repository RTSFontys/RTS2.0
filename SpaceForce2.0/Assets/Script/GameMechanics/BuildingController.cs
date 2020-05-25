using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public Material selected;
    public Material notSelected;

    private MeshRenderer myRend;


    public bool currentlyselected = false;
    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<GameController>().selectableUnits.Add(this.gameObject);
        ClickMe();
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
}
