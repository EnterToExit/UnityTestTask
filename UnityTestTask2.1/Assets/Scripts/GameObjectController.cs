using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectController : MonoBehaviour
{
    [SerializeField] private Transform _object;
    private Camera _camera;
    private MeshRenderer _meshRenderer;
    private float _localScale;

    private void Awake()
    {
        _localScale = _object.transform.localScale.x;
        _camera = Camera.main;
        _meshRenderer = _object.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _, 100))
        {
            _meshRenderer.material.color = GetRandomColor();
        }
    }

    private void FixedUpdate()
    {
        _object.transform.Rotate(Vector3.up, 1f);
    }

    private static Color GetRandomColor()
    {
        var red = Random.Range(0f, 1f);
        var green = Random.Range(0f, 1f);
        var blue = Random.Range(0f, 1f);

        var color = new Color(red, green, blue);
        return color;
    }
}