using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D _rb;
    [Header("Время до падения в секундах")]
    public float timer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Collapse(col));
        }
    }


    private IEnumerator Collapse(Collision2D col)
    {
        yield return new WaitForSeconds(timer);
        FallDown();
    }

    private void FallDown()
    {
        _rb.isKinematic = false;
    }
}