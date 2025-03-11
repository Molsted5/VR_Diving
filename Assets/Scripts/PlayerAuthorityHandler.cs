using Mirror;
using UnityEngine;

public class PlayerAuthorityHandler: NetworkBehaviour {
    [Command]
    public void RequestAuthority( GameObject targetObject ) {
        if( !isServer ) return;

        NetworkIdentity targetIdentity = targetObject.GetComponent<NetworkIdentity>();
        if( targetIdentity != null ) {
            AssignAuthority( targetIdentity );
        } else {
            Debug.LogError( "Target object does not have a NetworkIdentity." );
        }
    }

    [Server]
    private void AssignAuthority( NetworkIdentity targetIdentity ) {
        if( targetIdentity.connectionToClient != null ) {
            // Remove authority from the current owner
            targetIdentity.RemoveClientAuthority();
        }

        // Assign authority to the player requesting it
        targetIdentity.AssignClientAuthority( connectionToClient );
        Debug.Log( $"Authority assigned to connection {connectionToClient.connectionId}" );
    }
}
