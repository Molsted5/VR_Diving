using UnityEngine;
using Mirror;

public class AudioListenerManager: NetworkBehaviour {
    void Start() {
        // Check if this is the local player
        if( !isLocalPlayer ) {
            // Disable the Audio Listener for non-local players
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            if( audioListener != null ) {
                audioListener.enabled = false;
            }
        }
    }
}
