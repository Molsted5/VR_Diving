
using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogeDetector : MonoBehaviour
{
    public float dialogeAngleDetection = 75f;
    public GameObject dialogeUI;
    public Door door;
    public TextMeshProUGUI uiText;
    public float uiActiveDuration = 6f;
    public int locksUnlocked;

    public GameObject dialogFabricatorOff;
    public GameObject dialogFabricatorOn;
    public GameObject dialogDoor0;
    public GameObject dialogDoor1;
    public GameObject dialogDoor2;
    public GameObject dialogGuide;
    public GameObject dialogWash;
    public GameObject dialogSpawn;
    public GameObject dialogCanisterCode;

    private Transform keyDispenserTransform;
    private Transform fabricatorTransform;
    private Transform doorTransform;
    private Transform washerTransform;
    private Transform gomTransform;
    private Transform cameraTransform;
    private Coroutine deactivateUICoroutine;

    bool fabricatorActive;
    bool justSpawned = true;
    bool lookingAtCodeMachinge = false;
    bool lookingAtFabricator = false;
    bool lookingAtDoor = false;
    bool lookingAtWasher = false;
    bool lookingAtGOM = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fabricatorTransform = GameObject.Find("Fabricator").transform;
        keyDispenserTransform = GameObject.Find( "KeyDispenser" ).transform;
        doorTransform = GameObject.Find("Door").transform;
        washerTransform = GameObject.Find("WashingStation").transform;
        gomTransform = GameObject.Find("computer").transform;
        cameraTransform = GameObject.Find( "Main Camera" ).transform;

        dialogFabricatorOff.SetActive(false);
        dialogFabricatorOn.SetActive(false);
        dialogDoor0.SetActive(false);
        dialogDoor1.SetActive(false);
        dialogDoor2.SetActive(false);
        dialogGuide.SetActive(false);
        dialogWash.SetActive(false);
        dialogSpawn.SetActive(false);
        dialogCanisterCode.SetActive(false);

        /*if( dialogeUI != null ) {
            dialogeUI.SetActive( false ); // Ensure the UI element is initially inactive
        }*/
        ActivateUIWithCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        locksUnlocked = GameObject.Find("Door").GetComponent<Door>().locksUnlocked;
        fabricatorActive = GameObject.Find("Pincode_box").GetComponent<Sign_puzzle>().pin_activated;
        Debug.Log(lookingAtDoor);

        if ( ( keyDispenserTransform.position - cameraTransform.position ).magnitude < 4 ) {
            if ( Mathf.Abs( Vector3.Angle( -keyDispenserTransform.forward, cameraTransform.forward ) ) < dialogeAngleDetection ) {
                ActivateUIWithCoroutine();
                lookingAtCodeMachinge = true;
            }
        }
        if ((fabricatorTransform.position - cameraTransform.position).magnitude < 4)
        {
            if (Mathf.Abs(Vector3.Angle(fabricatorTransform.right, cameraTransform.forward)) < dialogeAngleDetection)
            {
                ActivateUIWithCoroutine();
                lookingAtFabricator = true;
            }
        }
        if ((doorTransform.position - cameraTransform.position).magnitude < 3)
        {
            if (Mathf.Abs(Vector3.Angle(doorTransform.forward, cameraTransform.forward)) < dialogeAngleDetection)
            {
                ActivateUIWithCoroutine();
                lookingAtDoor = true;
            }
        }
        if ((washerTransform.position - cameraTransform.position).magnitude < 4)                                    
        {
            if (Mathf.Abs(Vector3.Angle(washerTransform.forward, cameraTransform.forward)) < dialogeAngleDetection)
            {
                ActivateUIWithCoroutine();
                lookingAtWasher = true;
            }
        }

        if ((gomTransform.position - cameraTransform.position).magnitude < 6)                             
       {
           if (Mathf.Abs(Vector3.Angle(gomTransform.forward, cameraTransform.forward)) < dialogeAngleDetection)
           {
               ActivateUIWithCoroutine();
               lookingAtGOM = true;
           }
       }

    }

    private void ActivateUIWithCoroutine() {
        //if( dialogeUI != null ) {
            //dialogeUI.SetActive( true ); // Activate the UI
            if (lookingAtCodeMachinge)
            {
                /*uiText.text = "If I know Jeremy right, his password is always the service pressure in PSI followed by the capacity in cubic meters of his air tank�";
                dialogeUI.transform.position = new Vector3(41f, 2.40f, 26.5f);
                dialogeUI.transform.rotation = Quaternion.Euler(0f, -58.814f, 0f);*/
                dialogCanisterCode.SetActive(true);
            }
            if (justSpawned)
            {
                /*uiText.text = "I'M LOCKED OUT!?.." +
                    "I need figure out how to get inside the base before I run out of air�" +
                    "Maybe Jeremy hid some spare keys last time he was here?..";
                dialogeUI.transform.position = new Vector3(50.7f, 1.9f, 33f);
                dialogeUI.transform.rotation = Quaternion.Euler(0f, -90f, 0f);*/
                dialogSpawn.SetActive(true);
            }
            if (lookingAtFabricator)
            {
                if (!fabricatorActive)
                {
                    dialogFabricatorOff.SetActive(true);
                }
                else if (fabricatorActive)
                {
                    dialogFabricatorOn.SetActive(true);
                }
                /*uiText.text = "The Fabricator, Jeremy can never decide which size snorkel he likes so he keeps making new ones. It seems to be turned off, maybe I can power it on somehow?..";
                dialogeUI.transform.position = new Vector3(44.15f, 2.29f, 39.72f);
                dialogeUI.transform.rotation = Quaternion.Euler(0f, -138.691f, 0f);*/
                
            }
            if(lookingAtDoor)
            {
                if(locksUnlocked == 0)
                {
                    /*uiText.text = "I need to find the keys for the door...";
                    dialogeUI.transform.position = new Vector3(44.05f, 2.511f, 36.77f);
                    dialogeUI.transform.rotation = Quaternion.Euler(0f, -10.256f, 0f);*/
                    dialogDoor0.SetActive(true);
                }
                if (locksUnlocked == 1)
                {
                    if(dialogDoor0 == true)
                    {
                        dialogDoor0.SetActive(false);
                        dialogDoor1.SetActive(true);
                    }
                    else
                    {
                        dialogDoor0.SetActive(true);
                    }
                    /*uiText.text = "Thats one key down, two more to go...";
                    dialogeUI.transform.position = new Vector3(44.05f, 2.29f, 33f);
                    dialogeUI.transform.rotation = Quaternion.Euler(0f, 64.635f, 0f);*/
                }
                if (locksUnlocked == 2)
                {
                    /*uiText.text = "Just one more key and I will be safe!..";
                    dialogeUI.transform.position = new Vector3(44.05f, 2.29f, 33f);
                    dialogeUI.transform.rotation = Quaternion.Euler(0f, 64.635f, 0f);*/
                    if(dialogDoor0 == true || dialogDoor1 == true)
                    {
                        dialogDoor0.SetActive(false);
                        dialogDoor1.SetActive(false);
                        dialogDoor2.SetActive(true);
                    }
                    else
                    {
                        dialogDoor2.SetActive(true);
                    }
                }
            }
            if (lookingAtWasher)                                                                                                             
            {
                /*uiText.text = "Thats weird, the cannisters for the cleaning station are missing? It is usually Jeremys job to make sure they are refilled";
                dialogeUI.transform.position = new Vector3(41.137f, 2.511f, 40.057f);
                dialogeUI.transform.LookAt(cameraTransform);*/
                dialogWash.SetActive(true);
            }

            if (lookingAtGOM)                                                                                     
           {
                /*uiText.text = "That's the Guide-o-Matic, a great tool to get some knowledge on the fly�";
                dialogeUI.transform.position = new Vector3(44.16f, 2.29f, 31.14f);
                dialogeUI.transform.rotation = Quaternion.Euler(0f, 64.635f, 0f);*/
                dialogGuide.SetActive(true);
            }


            // Stop any existing coroutine to prevent overlapping timers
            if (deactivateUICoroutine != null)
            {
                StopCoroutine(deactivateUICoroutine);
            }

            // Start a new coroutine to deactivate the UI
            deactivateUICoroutine = StartCoroutine( DeactivateUIAfterTimer() );
        //}
    }

    private IEnumerator DeactivateUIAfterTimer() {
        yield return new WaitForSeconds( uiActiveDuration );

        // Deactivate the UI element
        if( dialogeUI != null ) {
            justSpawned = false;
            lookingAtCodeMachinge = false;
            lookingAtFabricator = false;
            lookingAtDoor = false;
            lookingAtWasher = false;
            lookingAtGOM = false;
            //dialogeUI.SetActive( false );

            dialogFabricatorOff.SetActive(false);
            dialogFabricatorOn.SetActive(false);
            dialogDoor0.SetActive(false);
            dialogDoor1.SetActive(false);
            dialogDoor2.SetActive(false);
            dialogGuide.SetActive(false);
            dialogWash.SetActive(false);
            dialogSpawn.SetActive(false);
            dialogCanisterCode.SetActive(false);
        }

        // Clear the reference to the coroutine
        deactivateUICoroutine = null;
    }

}
