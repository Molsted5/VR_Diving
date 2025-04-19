using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Sign_puzzle : MonoBehaviour
{

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    private string _Code = "UpHelpBoatOk";
    private string _Input;
    private int btncount;
    private bool checkingcode;
    public List<string> _btncheck = new List<string>();
    public Boolean pin_activated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _btncheck.Clear();
        _Input = "";
        Button1.onClick.AddListener(delegate { btninput("Up", Button1); });
        Button2.onClick.AddListener(delegate { btninput("Boat", Button2); });
        Button3.onClick.AddListener(delegate { btninput("Help", Button3); });
        Button4.onClick.AddListener(delegate { btninput("Ok", Button4); });
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown("space"))
        {
            pin_activated = true;
            Debug.Log("Space pressed");
        }

        if(btncount == 4 && !checkingcode)
        {
            checkingcode = true;
            StartCoroutine(CheckCode());
        };

        //Debug.Log(pin_activated);

    }

    IEnumerator CheckCode()
    {
        if (_Input == _Code)
        {
            Debug.Log("Match");
            Button1.image.color = Color.green;
            Button2.image.color = Color.green;
            Button3.image.color = Color.green;
            Button4.image.color = Color.green;
            pin_activated = true;
            btncount = 0;
            _Input = "";
            _btncheck.Clear();
        }
        else
        {
            Debug.Log("No match");
            Button1.image.color = Color.red;
            Button2.image.color = Color.red;
            Button3.image.color = Color.red;
            Button4.image.color = Color.red;
            btncount = 0;
            _Input = "";
            _btncheck.Clear();
        }

        Debug.Log("code checked");

        yield return new WaitForSeconds(2);

        Debug.Log("waited");

        Button1.image.color = Color.white;
        Button2.image.color = Color.white;
        Button3.image.color = Color.white;
        Button4.image.color = Color.white;
        checkingcode = false;
    }

    void btninput(string sign, Button mybutton)
    {
        
        if (_btncheck.Contains(sign))
        {
            Debug.Log("Already pressed");
        }
        else
        {
            _btncheck.Add(sign);
            _Input = _Input + sign;
            btncount++;
            mybutton.image.color = Color.blue;
            Debug.Log(_Input);
            Debug.Log(_Code);
        }   
    }
}
