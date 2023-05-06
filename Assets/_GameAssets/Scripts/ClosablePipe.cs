using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClosablePipe : MonoBehaviour
{
    public bool isOpen;

    public bool canBeOpened = true;
    public bool canBeClosed = true;

    public Sprite openPipeGraphic;
    public Sprite closePipeGraphic;
    public string interactKey = "f";

    public UnityEvent onOpen;
    public UnityEvent onClose;

    private bool canPlayerInteract;
    private PickupSystem pickupSystem;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        pickupSystem = PickupSystem.GetInstance();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateGraphics();
    }

    void Update()
    {
        if(canPlayerInteract && pickupSystem.hasWrench && ((canBeClosed && isOpen) || (canBeOpened && !isOpen))) {
            spriteRenderer.color = new Color32(0xCC, 0xFF, 0xDD, 0xFF);;
        } else {
            spriteRenderer.color = Color.white;
        }

        if(Input.GetKeyDown(interactKey) && canPlayerInteract && pickupSystem.hasWrench && ((canBeClosed && isOpen) || (canBeOpened && !isOpen))) {
            // Debug.Log("Closing Pipe");
            SetOpen(!isOpen);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPlayerInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canPlayerInteract = false;
        }
    }

    void SetOpen(bool newState) {
        if (newState == this.isOpen)
        {
            return;
        }

        this.isOpen = newState;
        UpdateGraphics();

        if(this.isOpen && onOpen != null) {
            onOpen.Invoke();
        } else if (!this.isOpen && onClose != null) {
            onClose.Invoke();
        }
    }

    void UpdateGraphics() {
        if (isOpen)
        {
            spriteRenderer.sprite = openPipeGraphic;
        } else {
            spriteRenderer.sprite = closePipeGraphic;
        }
    }
}
