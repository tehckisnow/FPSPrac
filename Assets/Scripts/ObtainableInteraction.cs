using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableInteraction : MonoBehaviour, IInteractable
{
    public string interactionLabel = "Get";
    public Material selectedMaterial;
    public Popup popup;
    public AudioSource soundEffect;
    public PlayerCoins coinManager;

    Renderer thisRenderer;
    Material originalMaterial;

    public float MaxRange { get { return maxRange; }}
    private const float maxRange = 5f;

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        originalMaterial = thisRenderer.material;
    }

    public void OnStartHover()
    {
        thisRenderer.material = selectedMaterial;
        popup.Open(interactionLabel);
    }

    public void OnInteract()
    {
        popup.Close();
        if(soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect.clip, new Vector3(0,0,0));
        }
        gameObject.SetActive(false);
        //Do thing;
        coinManager.AddCoin();
    }

    public void OnEndHover()
    {
        popup.Close();
        thisRenderer.material = originalMaterial;
    }
}
