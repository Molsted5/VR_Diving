using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class Sign_puzzle : MonoBehaviour
{

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    private string Code = "UpHelpBoatOk";
    private string Input;
    private string sign;
    private int btncount;
    public List<string> btncheck = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btncheck.Clear();
        Input = "";
        Button1.onClick.AddListener(delegate { btninput("Up", Button1); });
        Button2.onClick.AddListener(delegate { btninput("Boat", Button2); });
        Button3.onClick.AddListener(delegate { btninput("Help", Button3); });
        Button4.onClick.AddListener(delegate { btninput("Ok", Button4); });
        

    }

    // Update is called once per frame
    void Update()
    {
        if(btncount == 4)
        {
            if(Input == Code)
            {
                Debug.Log("Match");
                Button1.image.color = Color.green;
                Button2.image.color = Color.green;
                Button3.image.color = Color.green;
                Button4.image.color = Color.green;
                btncount = 0;
                Input = "";
                btncheck.Clear();
            }
            else
            {
                Debug.Log("No match");
                Button1.image.color = Color.red;
                Button2.image.color = Color.red;
                Button3.image.color = Color.red;
                Button4.image.color = Color.red;
                btncount = 0;
                Input = "";
                btncheck.Clear();
            }
            
        };

    }

    void btninput(string sign, Button mybutton)
    {
        
        if (btncheck.Contains(sign))
        {
            Debug.Log("Already pressed");
        }
        else
        {
            btncheck.Add(sign);
            Input = Input + sign;
            btncount++;
            mybutton.image.color = Color.blue;
            Debug.Log(Input);
            Debug.Log(Code);
        }   
    }
}
