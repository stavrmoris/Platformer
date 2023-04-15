using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public GameObject bloodSpot;
    public GameObject bloodSpawner;
    public string _tag;
    public AudioClip[] din;

    private int _audiocount, _audioId;

    private void PlayAudio()
    {
        _audiocount = din.Length;
        _audioId = Random.Range(0, _audiocount);
        gameObject.GetComponent<AudioSource>().PlayOneShot(din[_audioId]);
    }

    public void Reznia()
    {
        Instantiate(bloodSpot, bloodSpawner.transform.position, Quaternion.identity);
        PlayAudio();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(_tag))
        {
            Reznia();
        }
    }
}
