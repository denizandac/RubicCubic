using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    // Start is called before the first frame update

    [Header("Movement")]
    [SerializeField] private float _horizontalSpeed = 25f;
    [SerializeField] private float _frontalSpeed = 5f;
    [SerializeField] private float _gravityForce = 9.81f;
    [SerializeField] private float _roadWidth = 10f;
    [SerializeField] private float _playerWidth = 1f;
    //[SerializeField] private float _jumpHeight = 10f;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;

    [Header("Ground Check")]
    [SerializeField] private float _groundCheckRadius = 1.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded;
    //[SerializeField] private bool _willJump;

    [Header("Mouse Input")]
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private bool _isDragging = false;
    [SerializeField] private float _deltaX;
    [SerializeField] private float _dragStartX;
    [SerializeField] private float _targetX;
    [SerializeField] private float _LerpSpeed = 10f;
    //[SerializeField] private float _touchSensitivity = 100f;
    //[SerializeField] private float _touchThreshold = 0.7f;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        _initialPosition.y = transform.position.y;
        _initialPosition.z = transform.position.z;
        if(Physics.CheckSphere(new Vector3(0f,transform.position.y, 0f), _groundCheckRadius, _groundLayer)){
            _isGrounded = true;
        }
        else{
            _isGrounded = false;
        }
        if(Input.GetMouseButtonDown(0)){
            _initialPosition.x = transform.position.x;
            _isDragging = true;
            _dragStartX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButtonUp(0)){
            _isDragging = false;
        }
        if(Input.GetMouseButton(0)){
            _isDragging = true;
        }
        if (_isDragging){   
            float _deltaX = (Input.mousePosition.x - _dragStartX) * _horizontalSpeed / Screen.width;
            _targetX = _initialPosition.x + _deltaX;
            _targetX = Mathf.Clamp(_targetX, -_roadWidth/2 + _playerWidth/2, _roadWidth/2 - _playerWidth/2);
        }
        // if(Input.GetKeyDown(KeyCode.Space) && _isGrounded){
        //     _willJump = true;
        // }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        // Mouse Input for Mobile
        if (_isDragging)
        {
            Vector3 targetPosition = new Vector3(_targetX, _initialPosition.y, _initialPosition.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime*_LerpSpeed);
        }

        transform.Translate(Vector3.forward * _frontalSpeed * Time.deltaTime);

        if(!_isGrounded){
            transform.Translate(Vector3.down * _gravityForce * Time.deltaTime);
        }
        #region Jumping       
        // Jumping (if needed)
        // if(_willJump && _isGrounded)
        // {
        //     transform.Translate(Vector3.up * _jumpHeight * Time.deltaTime);
        //     _isGrounded = false;
        //     _willJump = false;
        // }
        #endregion
        #region Keyboard Input for Mobile
        // Keyboard Input for PC
        // if(Input.GetKey(KeyCode.A))
        // {
        //     transform.Translate(Vector3.left * _horizontalSpeed * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.D))
        // {
        //     transform.Translate(Vector3.right * _horizontalSpeed * Time.deltaTime);
        // }
        #endregion
    }   
}
