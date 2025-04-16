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
    private int sizeint = 20;

    public GameObject fabricatorInterface;
    public TMP_Text ActivationText;
    public TMP_Text SizeText;
    public Button add_btn;
    public Button subtract_btn;
    public Button print_btn;

    public Transform snorkelspawn;
    public GameObject snorkelprefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fabricatorInterface.SetActive(false);
        ActivationText.enabled = true;
        //SizeText.outlineWidth = 1f;
        //SizeText.outlineColor = new Color(255, 255, 255, 255);
        
        add_btn.onClick.AddListener(add);
        subtract_btn.onClick.AddListener(subtract);
        print_btn.onClick.AddListener(print);
    }

    // Update is called once per frame
    void Update()
    {
        
        Pin_activated = GameObject.Find("Pincode_box").GetComponent<Sign_puzzle>().pin_activated;

        if (Pin_activated == true)
        {
            ActivationText.enabled = false;
            fabricatorInterface.SetActive(true);
        };

        
        //Debug.Log(Pin_activated);
        
    }
    void add()
    {
        sizeint++;
        SizeText.text = sizeint.ToString();
        Debug.Log("add pressed");
    }
    void subtract()
    {
       sizeint--;
       SizeText.text = sizeint.ToString();
        Debug.Log("subtract pressed");
    }
    void print()
    {
        Instantiate(snorkelprefab, snorkelspawn.position, snorkelspawn.rotation);
        Debug.Log("print pressed");
    }
}
