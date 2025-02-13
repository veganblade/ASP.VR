using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private Material asfalt;
    public BoxCollider exampleBC;
    public BoxCollider groundBC;
    public float groundSize;
    public float groundSizeX;
    public float groundSizeY;
    void Start()
    {
        ground = GameObject.Find("Ground");
        asfalt = GameObject.Find("Ground").GetComponent<Material>();
        exampleBC = GameObject.Find("Example-1").GetComponent<BoxCollider>();
        groundBC = GameObject.Find("Ground").GetComponent<BoxCollider>();
        groundSizeX = exampleBC.size.x;
        groundSizeY = exampleBC.size.y;
        Instantiate(ground, transform.position, ground.transform.rotation);
    }

    void Update()
    {
        if(exampleBC.size.x > groundBC.size.x && exampleBC.size.y > groundBC.size.y)
        {
            groundSizeX = exampleBC.size.x + 500;
            groundSizeY = exampleBC.size.y + 500;
        }
    }
}

 