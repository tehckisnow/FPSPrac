using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionInteraction : MonoBehaviour, IInteractable
{
    public string description;
    public GameObject textbox;
    public Material selectedMaterial;

    public Popup popup;

    Renderer thisRenderer;
    Material originalMaterial;

    public float MaxRange { get { return maxRange; }}
    private const float maxRange = 10f;

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        originalMaterial = thisRenderer.material;
    }

    public void OnStartHover()
    {
        thisRenderer.material = selectedMaterial;
        popup.Open("Look");
    }

    public void OnInteract()
    {
        popup.Close();
        textbox.GetComponent<Textbox>().Open(description);
    }

    public void OnEndHover()
    {
        popup.Close();
        if(gameObject != null)
        {
            thisRenderer.material = originalMaterial;
        }
    }
}
