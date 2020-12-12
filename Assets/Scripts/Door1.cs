using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour, IInteractable
{
    public GameObject door;
    public bool open = false;
    public float slideDistance = 3.5f;

    public Popup popup;

    public float MaxRange { get { return maxRange; }}
    private const float maxRange = 5f;

    public void Toggle()
    {
        Debug.Log("Toggling");
        if(open)
        {
            open = false;
            door.transform.position -= Vector3.up * slideDistance;
        }
        else
        {
            open = true;
            door.transform.position -= Vector3.down * slideDistance;
        }
    }

    public void OnStartHover()
    {
        Debug.Log("door");
        popup.Open("Open");
    }

    public void OnInteract()
    {
        popup.Close();
        Debug.Log("interacting");
        Toggle();
    }

    public void OnEndHover()
    {
        popup.Close();
        Debug.Log("end hover");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
