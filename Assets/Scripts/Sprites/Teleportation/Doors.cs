using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform[] Points;
    public Transform PlayerTf;
    private int FoundIndex;
    private bool DEBUG = false;
    public static bool[] DoorsActive = {false, false};
    public bool Ready;

    private void Start()
    {
        PlayerTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        #region DEBUG
        if (Input.GetKey("space"))
        {
            DEBUG = true;
        }
        if (DEBUG)
        {
            Debug.DrawLine(Points[0].position, Points[1].position, Color.green);
        }
        #endregion

        if (Ready)
        {
            float FoundDist = Vector3.Distance(PlayerTf.position, Points[0].position) - Vector3.Distance(PlayerTf.position, Points[1].position);
            if (FoundDist >= 0)
            {
                FoundIndex = 1;
            }
            else
            {
                FoundIndex = 0;
            }

            if (!DoorwayTrigger.HasTeleported && Input.GetKey("space"))
            {
                if (FoundIndex == 1)
                {
                    PlayerTf.position = Points[0].position;
                }
                else
                {
                    PlayerTf.position = Points[1].position;
                }
                Ready = false;
                Movement.ReadyToTeleport = false;
                DoorwayTrigger.HasTeleported = true;
            }
        }
    }
}
