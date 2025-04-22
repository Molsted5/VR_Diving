using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
public class HandInStation_controller : MonoBehaviour
{
    public Transform KeySpawn;
    public GameObject KeyObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CheckScale(other));
    }

    IEnumerator CheckScale(Collider other)
    {
        float rootscale = other.transform.root.localScale.x;

        yield return new WaitForSeconds(2f);

        if (rootscale >= 0.30f && rootscale <= 0.35)
        {
            Destroy(other.transform.root.gameObject);
            Debug.Log("Scale is correct");
            Instantiate(KeyObject, KeySpawn.position, KeySpawn.rotation);

        }
        else
        {
            Destroy(other.transform.root.gameObject);
            Debug.Log("Scale not correct");
        }
    }
}
