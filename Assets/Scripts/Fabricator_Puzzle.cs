using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class Fabricator_Puzzle : MonoBehaviour
{
    private bool Pin_activated;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public TMP_Text Textbox1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pin_activated = GameObject.Find("Pincode_box").GetComponent<Sign_puzzle>().pin_activated;
        Button1.enabled = false;
        Button2.enabled = false;
        Button3.enabled = false;
        Button4.enabled = false;
        Textbox1.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pin_activated == true)
        {
            Button1.enabled = true;
            Button2.enabled = true;
            Button3.enabled = true;
            Button4.enabled = true;
            Textbox1.enabled = false;
        }
        ;
        
    }
}
