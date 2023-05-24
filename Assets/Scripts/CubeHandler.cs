using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool _isTaken = false;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && !_isTaken){
            _isTaken = true;
            GameManager.instance.collectCube();
            this.gameObject.SetActive(false);
        }
    }
}
