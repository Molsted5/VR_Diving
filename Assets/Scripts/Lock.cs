using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool locked;
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
        if( triggerCollider.transform.parent.CompareTag("Key") ) {
            Destroy( triggerCollider.transform.parent.gameObject );
            Unlock();
        }
    }
}
