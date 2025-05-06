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

    private Transform keyDispenserTransform;
    private Transform fabricatorTransform;
    private Transform doorTransform;
    private Transform washerTransform;
    private Transform gomTransform;
    private Transform cameraTransform;
    private Coroutine deactivateUICoroutine;

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

        if( dialogeUI != null ) {
            dialogeUI.SetActive( false ); // Ensure the UI element is initially inactive
        }
        ActivateUIWithCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ( ( keyDispenserTransform.position - cameraTransform.position ).magnitude < 3 ) {
            if ( Mathf.Abs( Vector3.Angle( -keyDispenserTransform.forward, cameraTransform.forward ) ) < dialogeAngleDetection ) {
                ActivateUIWithCoroutine();
                lookingAtCodeMachinge = true;
            }
        }
        if ((fabricatorTransform.position - cameraTransform.position).magnitude < 3)
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
        if ((washerTransform.position - cameraTransform.position).magnitude < 3)                                    
        {
            if (Mathf.Abs(Vector3.Angle(washerTransform.forward, cameraTransform.forward)) < dialogeAngleDetection)
            {
                ActivateUIWithCoroutine();
                lookingAtWasher = true;
            }
        }

        if ((gomTransform.position - cameraTransform.position).magnitude < 3)                             
       {
           if (Mathf.Abs(Vector3.Angle(gomTransform.forward, cameraTransform.forward)) < dialogeAngleDetection)
           {
               ActivateUIWithCoroutine();
               lookingAtGOM = true;
           }
       }

    }

    private void ActivateUIWithCoroutine() {
        if( dialogeUI != null ) {
            dialogeUI.SetActive( true ); // Activate the UI
            if (lookingAtCodeMachinge)
            {
                uiText.text = "If I know Jeremy right, his password is always the service pressure in PSI followed by the capacity in cubic meters of his air tank…";
            }
            if (justSpawned)
            {
                uiText.text = "I'M LOCKED OUT!?.." +
                    "I need figure out how to get inside the base before I run out of air…" +
                    "Maybe Jeremy hid some spare keys last time he was here?..";
            }
            if (lookingAtFabricator)
            {
                uiText.text = "The Fabricator, Jeremy can never decide which size snorkel he likes so he keeps making new ones. It seems to be turned off, maybe I can power it on somehow?..";
            }
            if(lookingAtDoor)
            {
                if(door.locksUnlocked == 0)
                {
                    uiText.text = "I need to find the keys for the door...";
                }
                if (door.locksUnlocked == 1)
                {
                    uiText.text = "Thats one key down, two more to go...";
                }
                if (door.locksUnlocked == 2)
                {
                    uiText.text = "Just one more key and I will be safe!..";
                }
            }
            if (lookingAtWasher)                                                                                                             
            {
                uiText.text = "Thats weird, the cannisters for the cleaning station are missing? It is usually Jeremys job to make sure they are refilled";
            }

            if (lookingAtGOM)                                                                                     
           {
               uiText.text = "That's the Guide-o-Matic, a great tool to get some knowledge on the fly…";
           }


            // Stop any existing coroutine to prevent overlapping timers
            if (deactivateUICoroutine != null)
            {
                StopCoroutine(deactivateUICoroutine);
            }

            // Start a new coroutine to deactivate the UI
            deactivateUICoroutine = StartCoroutine( DeactivateUIAfterTimer() );
        }
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
            dialogeUI.SetActive( false );
        }

        // Clear the reference to the coroutine
        deactivateUICoroutine = null;
    }

}
