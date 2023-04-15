using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Saw : MonoBehaviour
{
    [Header("Координаты точек, между которыми перемещается пила")]
    public Vector3[] targetPoints;
    [Header("Скорость")]
    public float speed = 5;
    [Header("Номер текущей точки")]
    public int i = 0;

    void Update()
    {
        if (transform.position != targetPoints[i])
            transform.position = Vector3.MoveTowards(transform.position, targetPoints[i], speed);
        else
            i += 1;

        if (i >= targetPoints.Length)
            i = 0;
    }
}
