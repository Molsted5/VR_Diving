using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Lock> locks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locks = new List<Lock>(GetComponentsInChildren<Lock>());
        foreach (Lock lockObject in locks){
            lockObject.unlockEvent += OnLockUnlocked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLockUnlocked( Lock unlockedLock ) {
        unlockedLock.unlockEvent -= OnLockUnlocked;
        Debug.Log( $"{unlockedLock.name} has been unlocked!" );

        if( locks.TrueForAll( l => !l.locked ) ) {
            Debug.Log( "All locks are unlocked! Door can now open." );
            OpenDoor(); 
        }
    }

    public void OpenDoor() {
        Debug.Log( "The door is now open!" );
        
        
    }
}
