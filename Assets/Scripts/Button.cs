using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door[] doors;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Button")){
            foreach(var door in doors){
                door.OpenDoor();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            foreach (var door in doors)
            {
                door.CloseDoor();
            }
        }
    }
}
