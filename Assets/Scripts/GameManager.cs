using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    [SerializeField] private int _score;
    [SerializeField] private int _cubeCount;
    [SerializeField] private int _cubeCountMax;
    [SerializeField] private int _lastCubeCount;
    [SerializeField] private Vector2 _lastPosition;
    [SerializeField] private Vector3 _lastRotation;
    //private GameManagerState _gameManagerState;

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

    void Start(){
        _score = 0;
        _cubeCount = 0;
        _cubeCountMax = 5;
        _lastCubeCount = 0;
        _lastPosition = PlayerController.instance.transform.position;
        _lastRotation = PlayerController.instance.transform.rotation.eulerAngles;
        //_gameManagerState = GameManagerState.Start;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void collectCube(){
        if(_cubeCount < _cubeCountMax){
            _cubeCount++;
        }
        _score+=100;
        //extract from pool
    }

    private void saveFromCheckpoint(){
        _lastCubeCount = _cubeCount;
        _lastPosition = PlayerController.instance.transform.position;
        _lastRotation = PlayerController.instance.transform.rotation.eulerAngles;
    }

    enum GameManagerState { 
        Start,
        GameOver
    }
}
