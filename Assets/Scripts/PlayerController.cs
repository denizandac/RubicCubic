using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    // Start is called before the first frame update

    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _frontalSpeed = 5f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private float _gravityForce = 9.81f;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;

    [Header("Ground Check")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _willJump;
    [SerializeField] private float _groundCheckRadius = 1f;
    [SerializeField] private LayerMask _groundLayer;

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
        _anim = GetComponent<Animator>();
        _groundLayer = LayerMask.GetMask("Ground");
    }

    void Update(){
        if(!_isGrounded){
            if(Physics.CheckSphere(new Vector3(0f,transform.position.y, 0f), _groundCheckRadius, _groundLayer)){
                _isGrounded = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded){
            _willJump = true;
        }
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
        if(_willJump)
        {
            transform.DOJump(transform.position, _jumpHeight, 1, 1f).SetEase(Ease.InOutSine);
            _isGrounded = false;
            _willJump = false;
        }
        if(!_isGrounded){
            transform.Translate(Vector3.down * _gravityForce * Time.deltaTime);
        }        
    }   
}
