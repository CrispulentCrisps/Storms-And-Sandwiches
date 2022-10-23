using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayTrigger : MonoBehaviour
{
    private Doors ParentScript;
    public static bool HasTeleported = false;
    public int Index;
    private void Start()
    {
        ParentScript = transform.parent.GetComponent<Doors>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !HasTeleported && Movement.ReadyToTeleport)
        {
            ParentScript.Ready = true;
            Doors.DoorsActive[Index] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HasTeleported = false;
        ParentScript.Ready = false;
        Doors.DoorsActive[Index] = false;
    }
}
