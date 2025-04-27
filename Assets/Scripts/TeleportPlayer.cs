using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class TeleportPlayer: MonoBehaviour {
    public TeleportationAnchor teleportationAnchor;
    public TeleportationProvider teleportationProvider;

    public void Teleport() {
        if( teleportationAnchor != null && teleportationProvider != null ) {
            TeleportRequest request = new TeleportRequest {
                destinationPosition = teleportationAnchor.transform.position // Teleport to the Anchor's position
            };

            teleportationProvider.QueueTeleportRequest( request ); // Trigger the teleportation
        }
        else {
            Debug.LogWarning( "Teleportation Anchor or Provider is not assigned!" );
        }
    }
}
