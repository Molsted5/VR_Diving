using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool locked;
    public bool keyUsed;
    public delegate void UnlockDelegate( Lock lockObject );
    public event UnlockDelegate unlockEvent;
    public Material material;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locked = true;
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock() {
        locked = false;
        material.color = Color.green;
        if( unlockEvent != null ) {
            unlockEvent( this );
        }
    }

    private void OnTriggerEnter( Collider triggerCollider ) {
        Transform key = triggerCollider.transform.parent;
        Key keyScript = key.GetComponent<Key>();
        if( key.CompareTag( "Key" ) && key.GetComponent<Key>().free ) {
            keyScript.free = false;
            Destroy( key.gameObject );
            Unlock();
        }
    }

}
