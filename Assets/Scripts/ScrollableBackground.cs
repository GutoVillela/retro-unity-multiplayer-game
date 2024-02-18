using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableBackground : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float _xSpeed = 0.005f, _ySpeed = 0.005f;

    private void Update()
    {
        Rect newRect = new Rect(_image.uvRect.position + new Vector2(_xSpeed, _ySpeed) * Time.deltaTime, _image.uvRect.size);
        _image.uvRect = newRect;
    }
}
