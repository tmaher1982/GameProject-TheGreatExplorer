using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    // How to Create Floating Text with Unity: A step-by-step tutorial
    // https://www.youtube.com/watch?v=ysg9oaZEgwc
    Transform mainCamera;
    public Transform canvas;
    Transform position;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
        position = transform.parent;
        transform.SetParent(canvas);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        transform.position = position.position + offset;
    }
}
