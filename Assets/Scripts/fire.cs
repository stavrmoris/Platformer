using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Light2D.Examples{public class fire : MonoBehaviour{
[Header("Источник света")]
public LightSprite coloredLight;
[Header("Минимальный огонь")]
public float min_fire = 0.6f;
[Header("Максимальный огонь")]
public float maxFire = 0.8f;
private float _randomINT;
private bool _minus;

void Update() {
    _randomINT = Random.Range(0.002f, 0.01f);
    if(coloredLight.Color.a < min_fire) {_minus = false;}else if(coloredLight.Color.a > maxFire){_minus = true;}
    if(_minus){coloredLight.Color.a -= _randomINT;}else{coloredLight.Color.a += _randomINT;}
}}}