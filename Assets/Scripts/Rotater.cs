using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public Blood _blood;
    public string _tag = "Player";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(_tag))
        {
            _blood.Reznia();
        }
    }
}
