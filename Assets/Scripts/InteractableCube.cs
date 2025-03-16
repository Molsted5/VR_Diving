using Mirror;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableCube : NetworkBehaviour {
    private XRGrabInteractable grabInteractable; // Reference to the XR Interaction Toolkit component that handles interactions

    private void Awake() {
        // Get the XRGrabInteractable component attached to this object
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable() {
        // Subscribe to grab (select) events
        grabInteractable.selectEntered.AddListener( OnGrabbed );
        grabInteractable.selectExited.AddListener( OnReleased );
    }

    private void OnDisable() {
        // Unsubscribe from grab (select) events
        grabInteractable.selectEntered.RemoveListener( OnGrabbed );
        grabInteractable.selectExited.RemoveListener( OnReleased );
    }

    private void OnGrabbed( SelectEnterEventArgs args ) {
        // Get the game object of the interactor (e.g., the controller grabbing the cube)
        var interactorObject = args.interactorObject.transform.gameObject;

        // Find the NetworkIdentity on the interactor's parent (e.g., XR Origin/Player)
        var playerIdentity = interactorObject.GetComponentInParent<NetworkIdentity>();
        if( playerIdentity != null ) {
            // Use the player's PlayerAuthorityHandler script to request authority over the cube
            var authorityHandler = playerIdentity.GetComponent<PlayerAuthorityHandler>();
            if( authorityHandler != null ) {
                authorityHandler.RequestAuthority( gameObject ); // Request authority for this cube
            } else {
                Debug.LogError( "Player does not have a PlayerAuthorityHandler component." );
            }
        } else {
            Debug.LogError( "No NetworkIdentity found in parent hierarchy of the grabbing controller." );
        }
    }

    private void OnReleased( SelectExitEventArgs args ) {
        // Logic to handle when the cube is released
        Debug.Log( "Cube released" );
    }
}
