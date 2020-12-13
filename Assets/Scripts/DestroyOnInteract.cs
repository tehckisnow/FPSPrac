using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, IInteractable
{
    public string interactionLabel = "Destroy";
    public Material selectedMaterial;
    public Popup popup;

    Renderer thisRenderer;
    Material originalMaterial;

    //float IInteractable.MaxRange => throw new System.NotImplementedException();
    public float MaxRange { get { return maxRange; }}
    private const float maxRange = 5f;

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        originalMaterial = thisRenderer.material;
    }

    public void OnStartHover()
    {
        Debug.Log($"Ready to destroy {gameObject.name}");
        thisRenderer.material = selectedMaterial;
        popup.Open(interactionLabel);
    }

    public void OnInteract()
    {
        popup.Close();
        //Destroy(gameObject);//this causes errors in OnEndHover()
        gameObject.SetActive(false);
    }

    public void OnEndHover()
    {
        popup.Close();
        Debug.Log("nvm");
        if(gameObject != null)
        {
            thisRenderer.material = originalMaterial;
        }
    }
}
