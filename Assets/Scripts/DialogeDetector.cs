using System.Collections;
using UnityEngine;

public class DialogeDetector : MonoBehaviour
{
    public float dialogeAngleDetection = 75f;
    public GameObject dialogeUI;
    public float uiActiveDuration = 3f;

    private Transform keyDispenserTransform;
    private Transform cameraTransform;
    private Coroutine deactivateUICoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyDispenserTransform = GameObject.Find( "KeyDispenser" ).transform;
        cameraTransform = GameObject.Find( "Main Camera" ).transform;

        if( dialogeUI != null ) {
            dialogeUI.SetActive( false ); // Ensure the UI element is initially inactive
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( ( keyDispenserTransform.position - cameraTransform.position ).magnitude < 2 ) {
            if ( Mathf.Abs( Vector3.Angle( -keyDispenserTransform.forward, cameraTransform.forward ) ) < dialogeAngleDetection ) {
                ActivateUIWithCoroutine();
            }
        }
    }

    private void ActivateUIWithCoroutine() {
        if( dialogeUI != null ) {
            dialogeUI.SetActive( true ); // Activate the UI

            // Stop any existing coroutine to prevent overlapping timers
            if( deactivateUICoroutine != null ) {
                StopCoroutine( deactivateUICoroutine );
            }

            // Start a new coroutine to deactivate the UI
            deactivateUICoroutine = StartCoroutine( DeactivateUIAfterTimer() );
        }
    }

    private IEnumerator DeactivateUIAfterTimer() {
        yield return new WaitForSeconds( uiActiveDuration );

        // Deactivate the UI element
        if( dialogeUI != null ) {
            dialogeUI.SetActive( false );
        }

        // Clear the reference to the coroutine
        deactivateUICoroutine = null;
    }

}
