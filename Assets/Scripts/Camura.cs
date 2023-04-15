using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camura : MonoBehaviour
{
    [Header("—корость камеры")]
    public float damping = 1.5f;
    [Header(" оординаты камеры в спокойном состо€нии относительно игрока")]
    public Vector2 offset = new Vector2(2f, 1f);
    [Header("¬ какую сторону смотрит игрок")]
    public bool faceLeft;
    Transform _player;
    private int _lastX;

    void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(faceLeft);
    }

    public void FindPlayer(bool playerFaceLeft)
    {
        _lastX = Mathf.RoundToInt(_player.position.x);
        if(playerFaceLeft)
        {
            transform.position = new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
        }
    }

    void FixedUpdate() 
    {
        DontDestroyOnLoad(this.gameObject);
        if(_player)
        {
            int currentX = Mathf.RoundToInt(_player.position.x);
            if(currentX > _lastX) faceLeft = false; else if(currentX < _lastX) faceLeft = true;
            _lastX = Mathf.RoundToInt(_player.position.x);

            Vector3 target;
            if(faceLeft)
            {
                target = new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}