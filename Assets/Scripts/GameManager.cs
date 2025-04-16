using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Door door;
    public GameObject dialogeUI;

    void Start() {
        if( door != null ) {
            door.DoorOpenedEvent += OnGameWon;  // Subscribe to the DoorOpenedEvent
        }

        if( dialogeUI != null ) {
            dialogeUI.SetActive( false ); // Ensure the UI element is initially inactive
        }
    }

    private void OnGameWon() {
        print( "Congratulations!" );
        if( dialogeUI != null ) {
            dialogeUI.SetActive( true );
        }
    }

    void OnDestroy() {
        if( door != null ) {
            door.DoorOpenedEvent -= OnGameWon; // Unsubscribe to avoid memory leaks
        }
    }

}
