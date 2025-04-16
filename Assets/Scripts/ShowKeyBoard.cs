using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyBoard : MonoBehaviour
{
    private TMP_InputField inputField;
    
    private Transform cameraTransform;

    public float distance = 0.5f;
    public float verticalOffset = -0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag( "MainCamera" ).transform;
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener( x => OpenKeyboard() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKeyboard() {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard( inputField.text );

        Vector3 direction = cameraTransform.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = cameraTransform.position + direction * distance + new Vector3( 0, verticalOffset, 0 );
        
        NonNativeKeyboard.Instance.RepositionKeyboard( targetPosition );

        SetCaretColorAlpha( 1 );

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    private void Instance_OnClosed( object sender, System.EventArgs e ) {
        SetCaretColorAlpha( 0 );
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCaretColorAlpha( float alpha ) {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = alpha;
        inputField.caretColor = caretColor;
    }
}
