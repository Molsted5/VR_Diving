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
    private float sizefloat = 0.2f;
    private Vector3 scale = new Vector3(0.2f, 0.2f, 0.2f);

    public GameObject fabricatorInterface;
    public TMP_Text ActivationText;
    public TMP_Text SizeText;
    public TMP_Text desciption;
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
        }   
    }
    void add()
    {
        if(sizeint >= 45)
        {
            desciption.text = "Størrelse skal være mellem 15cm og 45cm";
        }
        else
        {
            desciption.text = "Indstil størrelse og print";
            sizeint++;
            sizefloat = (sizeint / 100f);
            scale = new Vector3(sizefloat, sizefloat, sizefloat);
        }
        SizeText.text = sizeint.ToString() + " cm";
        Debug.Log("add pressed");
    }
    void subtract()
    {
        if (sizeint <= 15)
        {
            desciption.text = "Størrelse skal være mellem 15cm og 45cm";
        }
        else
        {
            desciption.text = "Indstil størrelse og print";
            sizeint--;
            sizefloat = (sizeint / 100f);
            scale = new Vector3(sizefloat, sizefloat, sizefloat);
        }
        SizeText.text = sizeint.ToString();
        Debug.Log("subtract pressed");
    }
    void print()
    {
        GameObject printedObject = Instantiate(snorkelprefab, snorkelspawn.position, snorkelspawn.rotation);
        printedObject.transform.localScale = scale;
        Debug.Log("print pressed");
    }
}
