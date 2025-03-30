using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerRoomCodeMachine : MonoBehaviour
{
    public Image image;
    public Button button;
    public TMP_InputField inputField;

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
            puzzleSolved = true;
        } else {
            image.color = Color.yellow;
        }
        
    }
}
