using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    // Start is called before the first frame update

    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _frontalSpeed = 5f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private float _gravityForce = 9.81f;
    [SerializeField] private float _roadWidth = 10f;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;

    [Header("Ground Check")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _willJump;
    [SerializeField] private float _groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("Mouse Input")]
    [SerializeField] private Vector2 _startTouchPosition;
    [SerializeField] private Vector2 _currentTouchPosition;
    [SerializeField] private Vector2 _touchDelta;
    [SerializeField] private float _touchThreshold = 0.7f;
    [SerializeField] private float _touchSensitivity = 75f;


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
        if(Physics.CheckSphere(new Vector3(0f,transform.position.y, 0f), _groundCheckRadius, _groundLayer)){
            _isGrounded = true;
        }
        else{
            _isGrounded = false;
        }
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded){
            _willJump = true;
        }
        if(Input.GetMouseButtonDown(0)){
            _startTouchPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(0)){
            _currentTouchPosition = Input.mousePosition;
            _touchDelta = _currentTouchPosition-_startTouchPosition;
            _horizontalSpeed = (_touchDelta.x * _roadWidth * _touchSensitivity)/ Screen.width;
            _startTouchPosition = _currentTouchPosition;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Keyboard Input for PC
        // if(Input.GetKey(KeyCode.A))
        // {
        //     transform.Translate(Vector3.left * _horizontalSpeed * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.D))
        // {
        //     transform.Translate(Vector3.right * _horizontalSpeed * Time.deltaTime);
        // }

        // Mouse Input for Mobile

        if(Input.GetMouseButton(0)){
            if(_horizontalSpeed > _touchThreshold || _horizontalSpeed < -_touchThreshold){
                transform.Translate(Vector3.right * _horizontalSpeed * Time.deltaTime);
            }
        }

        transform.Translate(Vector3.forward * _frontalSpeed * Time.deltaTime);
        if(_willJump && _isGrounded)
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
