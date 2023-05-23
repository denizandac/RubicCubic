using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    // Start is called before the first frame update

    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _frontalSpeed = 5f;
    [SerializeField] private float _jumpHeight = 20f;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;

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
        _rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _horizontalSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _horizontalSpeed * Time.deltaTime);
        }
        transform.Translate(Vector3.forward * _frontalSpeed * Time.deltaTime);
    }
}
