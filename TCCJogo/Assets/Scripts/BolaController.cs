
using System;
using UnityEngine;


public class BolaController : MonoBehaviour
{
    SpriteRenderer sr;
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        sr.color = Color.blue;
    }   
}