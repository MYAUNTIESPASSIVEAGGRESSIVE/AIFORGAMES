using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeeingSense : MonoBehaviour
{
    // colliders for the each sections of the peripharel
    private Collider SightA;
    private Collider SightB;

    private void Start()
    {
        SightA = GetComponentInChildren<Collider>();
        SightB = GetComponentInChildren<Collider>();
    }


}
