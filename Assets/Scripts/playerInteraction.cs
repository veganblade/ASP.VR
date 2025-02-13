using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class playerInteraction : MonoBehaviour
{
    public Camera mainCamera;
    public float interactionDistance = 10f;             //дистанция до захвата детали
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    public AddBoxColliderToChildren ABCTC;
    [SerializeField] public string ObjectName;

    void Start()
    {
        ABCTC = GameObject.Find("EXAMPLE-1").GetComponent<AddBoxColliderToChildren>();        
    }
    void Update()
    {
        InteractionRay();
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
    }
    void InteractionRay()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            ObjectName = hit.collider.gameObject.name;
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable !=null)
            {
                hitSomething = true;
                Debug.Log("это наведение на деталь");
                interactionText.text = interactable.GetDescription();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    /*ABCTC.DisplayDictionary();*/
                    interactable.Interact();
                }
            }
        }
        interactionUI.SetActive(hitSomething);
    }
}
