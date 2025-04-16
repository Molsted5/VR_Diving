using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Lock> locks;
    public delegate void DoorOpenedDelegate();
    public event DoorOpenedDelegate DoorOpenedEvent;

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
        print( $"{unlockedLock.name} has been unlocked!" );

        if( locks.TrueForAll( l => !l.locked ) ) {
            print( "All locks are unlocked! Door can now open." );
            OpenDoor(); 
        }
    }

    public void OpenDoor() {
        print( "The door is now open!" );
        DoorOpenedEvent?.Invoke();
    }
}
