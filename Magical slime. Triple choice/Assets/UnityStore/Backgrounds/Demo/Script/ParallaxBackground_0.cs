using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxBackground0 : MonoBehaviour
{
    [FormerlySerializedAs("Camera_Move")] public bool cameraMove;

    [FormerlySerializedAs("Camera_MoveSpeed")]
    public float cameraMoveSpeed = 1.5f;

    [FormerlySerializedAs("Layer_Speed")] [Header("Layer Setting")]
    public float[] layerSpeed = new float[7];

    [FormerlySerializedAs("Layer_Objects")]
    public GameObject[] layerObjects = new GameObject[7];

    private Transform _camera;
    private float[] _startPos = new float[7];
    private float _boundSizeX;
    private float _sizeX;
    private GameObject _layer0;

    void Start()
    {
        _camera = Camera.main.transform;
        _sizeX = layerObjects[0].transform.localScale.x;
        _boundSizeX = layerObjects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i = 0; i < 5; i++)
        {
            _startPos[i] = _camera.position.x;
        }
    }

    void Update()
    {
        //Moving camera
        if (cameraMove)
        {
            _camera.position += Vector3.right * Time.deltaTime * cameraMoveSpeed;
        }

        for (int i = 0; i < 5; i++)
        {
            float temp = (_camera.position.x * (1 - layerSpeed[i]));
            float distance = _camera.position.x * layerSpeed[i];
            layerObjects[i].transform.position = new Vector2(_startPos[i] + distance, _camera.position.y);
            if (temp > _startPos[i] + _boundSizeX * _sizeX)
            {
                _startPos[i] += _boundSizeX * _sizeX;
            }
            else if (temp < _startPos[i] - _boundSizeX * _sizeX)
            {
                _startPos[i] -= _boundSizeX * _sizeX;
            }
        }
    }
}