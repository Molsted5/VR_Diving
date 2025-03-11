using Mirror;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableCube: NetworkBehaviour {
    private XRGrabInteractable grabInteractable;

    private void Awake() {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable() {
        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener( OnGrabbed );
        grabInteractable.selectExited.AddListener( OnReleased );
    }

    private void OnDisable() {
        // Unsubscribe from grab events
        grabInteractable.selectEntered.RemoveListener( OnGrabbed );
        grabInteractable.selectExited.RemoveListener( OnReleased );
    }

    private void OnGrabbed( SelectEnterEventArgs args ) {
        // Find the NetworkIdentity of the player who interacted
        var interactorObject = args.interactorObject.transform.gameObject;
        var playerIdentity = interactorObject.GetComponentInParent<NetworkIdentity>();
        if( playerIdentity != null ) {
            // Notify the player to request authority from the server
            PlayerAuthorityHandler authorityHandler = playerIdentity.GetComponent<PlayerAuthorityHandler>();
            if( authorityHandler != null ) {
                authorityHandler.RequestAuthority( gameObject );
            } else {
                Debug.LogError( "Player does not have a PlayerAuthorityHandler component." );
            }
        } else {
            Debug.LogError( "No NetworkIdentity found in parent hierarchy of the grabbing controller." );
        }
    }

    private void OnReleased( SelectExitEventArgs args ) {
        Debug.Log( "Cube released" );
        // Optionally handle release logic here
    }
}
