using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class HandInStation_controller : MonoBehaviour
{
    public Transform KeySpawn;
    public GameObject KeyObject;
    public Animator DoorAnimation;
    public Button Activate;
    public Image Indicator;
    public TMP_Text InformText;

    private bool KeyRecieved;
    private Collider CurrentItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Activate.onClick.AddListener(OnActivationClick);
        InformText.text = "Insert snorkel of of correct size";
        Indicator.color = Color.grey;
        KeyRecieved = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentItem = other;
    }

    private void OnActivationClick()
    {
        if (KeyRecieved == false)
        {
            if (CurrentItem != null)
            {
                StartCoroutine(CheckScale(CurrentItem));
            }
            else
            {
                StartCoroutine(EmptyBox());
            }
        }
        else
        {
            StartCoroutine(KeyIsRecieved());
        }
        
    }

    IEnumerator KeyIsRecieved()
    {
        InformText.text = "Puzzle already solved";
        yield return new WaitForSeconds(3f);
        InformText.text = "Insert snorkel of of correct size";
    }
    //Coroutine for if the box is empty. Start animation, wait 2 seconds, update text and color, wait 3 seconds then reset
    IEnumerator EmptyBox()
    {
        DoorAnimation.SetBool("Unlocked", true);
        yield return new WaitForSeconds(2f);
        InformText.text = "No item in the box";
        Indicator.color = Color.red;

        yield return new WaitForSeconds(3f);

        InformText.text = "Insert snorkel of of correct size";
        Indicator.color = Color.grey;
        DoorAnimation.SetBool("Unlocked", false);
    }

    IEnumerator CheckScale(Collider other)
    {
        //Get the rootscale of entered object and start animation, then wait 2 seconds.
        float rootscale = other.transform.root.localScale.x;
        DoorAnimation.SetBool("Unlocked", true);

        yield return new WaitForSeconds(2f);
       
        //Check scale of object
        if (other.CompareTag("Snorkel"))
        {
            if (rootscale >= 0.30f && rootscale <= 0.35f)
            {
                Destroy(other.transform.root.gameObject);
                InformText.text = "Snorkel is within the recommended size";
                Indicator.color = Color.green;
                Instantiate(KeyObject, KeySpawn.position, KeySpawn.rotation);
                KeyRecieved = true;
                DoorAnimation.SetBool("Unlocked", false);
            }
            else
            {
                Destroy(other.transform.root.gameObject);
                InformText.text = "Snorkel is not correct size";
                Indicator.color = Color.red;
                DoorAnimation.SetBool("Unlocked", false);
            }
        }
        else
        {
            InformText.text = "Machine only accepts snorkels";
        }


            //wait and reset text and button color
            yield return new WaitForSeconds(4f);
        Indicator.color = Color.grey;
        InformText.text = "Insert snorkel of of correct size";
    }
}
