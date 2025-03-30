using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerRoomCodeMachine : MonoBehaviour
{
    public Image image;
    public Button button;
    public TMP_InputField inputField;
    public GameObject keyPrefab;
    public Transform keySpawnTransform;

    private bool puzzleSolved;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener( CheckLockerRoomCode );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckLockerRoomCode() {
        if ( puzzleSolved ) return;

        if ( inputField.text == "code" ) {
            image.color = Color.green;
            Instantiate( keyPrefab, keySpawnTransform.position, keySpawnTransform.rotation );
            puzzleSolved = true;
        } else {
            image.color = Color.yellow;
        }
        
    }
}
