using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    // Start is called before the first frame update

    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed = 5f;

    


    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    } 
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
