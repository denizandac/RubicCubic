using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnout : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private _TurnoutType _turnoutType;
    [SerializeField] private GameObject _player;
    public bool isEntered = false;

    void Start()
    {
        _player = PlayerController.instance.gameObject;
    }
    void FixedUpdate(){ 
        if(isEntered){
            // yolun neresindeyse movement speed ona göre ayarlanmalı
            if(_turnoutType == _TurnoutType.Left){
                _player.transform.parent.rotation = Quaternion.Lerp(_player.transform.rotation, Quaternion.Euler(0, -90f, 0), Time.deltaTime);
            }
            else if(_turnoutType == _TurnoutType.Right){
                _player.transform.parent.rotation = Quaternion.Lerp(_player.transform.rotation, Quaternion.Euler(0, 90f, 0), Time.deltaTime);
            }
        }
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        isEntered = true;
    }
    enum _TurnoutType{
        Left,
        Right
    }
}
