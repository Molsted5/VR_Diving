using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Door door;
    public GameObject dialogeUI;

    void Start() {
        if( door != null ) {
            door.DoorOpenedEvent += OnGameWon;  // Subscribe to the DoorOpenedEvent
        }
    }

    private void OnGameWon() {
        print( "Congratulations!" );
    }

    void OnDestroy() {
        if( door != null ) {
            door.DoorOpenedEvent -= OnGameWon; // Unsubscribe to avoid memory leaks
        }
    }

}
