using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Lock> locks;
    public delegate void DoorOpenedDelegate();
    public event DoorOpenedDelegate DoorOpenedEvent;
    public int locksUnlocked;
    public float speed;
    public Transform targetPosition;
    private Coroutine currentCoroutine;

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
        //print( $"{unlockedLock.name} has been unlocked!" );
        locksUnlocked++;


        if( locks.TrueForAll( l => !l.locked ) ) {
            print( "All locks are unlocked! Door can now open." );
            OpenDoor(); 
        }
    }

    public void OpenDoor() {
        print( "The door is now open!" );
        if( currentCoroutine == null ) {
            currentCoroutine = StartCoroutine( MoveDoor( targetPosition.position ) );
        }
        DoorOpenedEvent?.Invoke();
    }

    private IEnumerator MoveDoor( Vector3 endPosition ) {
        while( Vector3.Distance( transform.position, endPosition ) > 0.01f ) {
            transform.position = Vector3.MoveTowards( transform.position, endPosition, speed * Time.deltaTime );
            yield return null;
        }
        currentCoroutine = null;
    }
}
