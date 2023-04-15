using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Trap : MonoBehaviour
{
    public bool isNotAtacked = true;
    private Animator _anim;
    Transform _player;
    public float distanceTOplayer;
    public GameObject trapSprite;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        distanceTOplayer = Vector2.Distance(_player.transform.position, transform.position);
        var distanceTOplayer2 = distanceTOplayer*4;
        if (distanceTOplayer <= 7 && isNotAtacked)
        {
            trapSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1/ distanceTOplayer2);
        }else if(distanceTOplayer > 7 && isNotAtacked)
        {
            trapSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        else
        {
            trapSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isNotAtacked){
                //Destroy(col.gameObject);
                isNotAtacked = false;
                _anim.SetTrigger("Attack");
            }
        }
    }
}
