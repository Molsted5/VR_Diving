using Mirror;
using UnityEngine;

public class PlayerAuthorityHandler: NetworkBehaviour {
    // Command: Called on the client but executed on the server
    [Command]
    public void RequestAuthority( GameObject targetObject ) {
        if( !isServer ) return; // Ensure this runs only on the server

        // Get the NetworkIdentity of the target object (e.g., the cube)
        NetworkIdentity targetIdentity = targetObject.GetComponent<NetworkIdentity>();
        if( targetIdentity != null ) {
            AssignAuthority( targetIdentity ); // Delegate authority assignment to the server
        } else {
            Debug.LogError( "Target object does not have a NetworkIdentity." );
        }
    }

    [Server]
    private void AssignAuthority( NetworkIdentity targetIdentity ) {
        // Remove authority from the current owner, if any
        if( targetIdentity.connectionToClient != null ) {
            targetIdentity.RemoveClientAuthority();
        }

        // Assign authority to the player that sent the request
        targetIdentity.AssignClientAuthority( connectionToClient );
        Debug.Log( $"Authority assigned to connection {connectionToClient.connectionId}" );
    }
}
