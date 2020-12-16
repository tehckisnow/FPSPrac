using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftInteraction : MonoBehaviour, IInteractable
{
    public string interactionLabel = "Lift";
    public Material selectedMaterial;
    public Popup popup;
    public GameObject player;

    Renderer thisRenderer;
    Material originalMaterial;

    public float MaxRange { get { return maxRange; }}
    private const float maxRange = 5f;
    private bool taken = false;
    private Rigidbody rb;

    private void Pickup()
    {
        popup.Close();
        popup.Open("Drop");
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if(rb != null)
        {
            rb.isKinematic = true;
        }
        gameObject.transform.SetParent(player.transform);
        taken = true;
    }

    private void Drop()
    {
        gameObject.transform.parent = null;
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if(rb != null)
        {
            rb.isKinematic = false;
        }
        taken = false;
    }

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        originalMaterial = thisRenderer.material;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnStartHover()
    {
        thisRenderer.material = selectedMaterial;
        popup.Open(interactionLabel);
    }

    public void OnInteract()
    {
        popup.Close();
        if(taken)
        {
            Drop();
        }
        else
        {
            Pickup();
        }
    }

    public void OnEndHover()
    {
        popup.Close();
        thisRenderer.material = originalMaterial;
    }
}
