using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPower : MonoBehaviour
{
    public bool TriggerGameFlow = true;

    public Renderer Weapon;

    public Material TargetMaterial;

    private void OnTriggerExit(Collider other)
    {
        Weapon.material = TargetMaterial;

        TriggerGameFlow = false;
    }
}