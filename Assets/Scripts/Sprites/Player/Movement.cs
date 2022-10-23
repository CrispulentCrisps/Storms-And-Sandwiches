using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public GameObject UI;
    public GameObject Interactee;
    public Rigidbody2D rb;
    Interact npc;
    Vector2 MovementTf;
    public float Speed;
    public static bool ReadyToTeleport = true;

    public void FixedUpdate()
    {
        if (!UI.active)
        {
            MovementTf = new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed);
        }
        else
        {
            MovementTf = new Vector2(0, 0);
        }
        rb.velocity = MovementTf;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "InteractZone")
        {
            Interactee = other.gameObject;
            npc = Interactee.GetComponent<Interact>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "InteractZone")
        {
            if (Input.GetKey("space") && !npc.HasStarted)
            {
                StartInteraction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Doorway" && Doors.DoorsActive[0] && Doors.DoorsActive[1])
        {
            ReadyToTeleport = true;
            DoorwayTrigger.HasTeleported = false;
            ReadyToTeleport = true;
        }
    }

    public void StartInteraction()
    {
        UI.SetActive(true);
        npc.StartInteraction();
    }
}