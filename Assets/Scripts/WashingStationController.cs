using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WashingStationController : MonoBehaviour
{
    public Animator DoorAnimation;
    public GameObject KeyObject;
    public Transform KeySpawn;
    public Button Activate;
    public Image Indicator;
    public TMP_Text InformText;

    private bool KeyRecieved;
    private Collider CurrentItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Activate.onClick.AddListener(OnActivationClick);
        InformText.text = "Insert cleaning liquid for diving gear";
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
        if(KeyRecieved == false)
        {
            if (CurrentItem != null)
            {
                StartCoroutine(CheckLiquid(CurrentItem));
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
        InformText.text = "Insert cleaning liquid for diving gear";
    }
    IEnumerator EmptyBox()
    {
        DoorAnimation.SetBool("Unlocked", true);
        yield return new WaitForSeconds(2f);
        InformText.text = "No item in the box";
        Indicator.color = Color.red;

        yield return new WaitForSeconds(3f);

        InformText.text = "Insert cleaning liquid for diving gear";
        Indicator.color = Color.grey;
    }
    IEnumerator CheckLiquid(Collider other)
    {
        DoorAnimation.SetBool("Unlocked", true);
        yield return new WaitForSeconds(2f);

        if (other.CompareTag("Water"))
        {
            Instantiate(KeyObject, KeySpawn.position, KeySpawn.rotation);
            DoorAnimation.SetBool("Unlocked", false);
            InformText.text = "Correct Liquid";
            Indicator.color = Color.green;
            KeyRecieved = true;
        }
        else
        {
            DoorAnimation.SetBool("Unlocked", false);
            InformText.text = "Wrong Liquid";
            Indicator.color = Color.red;
        }

        yield return new WaitForSeconds(4f);
        InformText.text = "Insert cleaning liquid for diving gear";
        Indicator.color = Color.grey;
    }    
   
    
}
