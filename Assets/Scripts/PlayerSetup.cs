using UnityEngine;
using Mirror;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.XR.Hands.Samples.VisualizerSample;
using UnityEngine.XR.Hands;

public class PlayerSetup: NetworkBehaviour {
    void Start() {
        if( !isLocalPlayer ) {
            // Disable Camera
            var camera = GetComponentInChildren<Camera>();
            if( camera != null ) {
                camera.enabled = false;
            }

            // Disable Audio Listener
            var audioListener = GetComponentInChildren<AudioListener>();
            if( audioListener != null ) {
                audioListener.enabled = false;
            }

            // Disable movement provider
            var moveProvider = GetComponentInChildren<DynamicMoveProvider>();
            if( moveProvider != null ) {
                moveProvider.enabled = false;
            }

            // Disable snap turn provider
            var snapTurnProvider = GetComponentInChildren<SnapTurnProvider>();
            if( snapTurnProvider != null ) {
                snapTurnProvider.enabled = false;
            }

            // Disable continuous turn provider
            var continuousTurnProvider = GetComponentInChildren<ContinuousTurnProvider>();
            if( continuousTurnProvider != null ) {
                continuousTurnProvider.enabled = false;
            }

            // Disable teleportation provider
            var teleportProvider = GetComponentInChildren<TeleportationProvider>();
            if( teleportProvider != null ) {
                teleportProvider.enabled = false;
            }

            // Disable CharacterController
            var characterController = GetComponent<CharacterController>();
            if( characterController != null ) {
                characterController.enabled = false;
            }

            // Disable PlayerInput
            var playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
            if( playerInput != null ) {
                playerInput.enabled = false;
            }

            // Disable XR Input Modality Manager
            var inputModalityManager = GetComponent<XRInputModalityManager>();
            if( inputModalityManager != null ) {
                inputModalityManager.enabled = false;
            }

            ////Disable input handlers
            //var inputHandlers = GetComponentsInChildren<UnityEngine.InputSystem.InputAction>();
            //foreach( var inputHandler in inputHandlers ) {
            //    inputHandler.Disable();
            //}

            // Disable TrackedPoseDriver
            var trackedPoseDrivers = GetComponentsInChildren<UnityEngine.InputSystem.XR.TrackedPoseDriver>();
            foreach( var driver in trackedPoseDrivers ) {
                driver.enabled = false;
            }

            // Disable interactors (e.g., XRDirectInteractor, XRRayInteractor)
            var interactors = GetComponentsInChildren<XRBaseInteractor>();
            foreach( var interactor in interactors ) {
                interactor.enabled = false;
            }

            // Disable interaction layers
            foreach( var interactor in interactors ) {
                interactor.enabled = false;
                interactor.gameObject.layer = LayerMask.NameToLayer( "Ignore Raycast" ); // Example layer
            }

            // Disable controller animator
            var controllerAnimator = GetComponentInChildren<ControllerAnimator>();
            if( controllerAnimator != null ) {
                controllerAnimator.enabled = false;
            }

            // Disable network animator
            var networkAnimator = GetComponent<NetworkAnimator>();
            if( networkAnimator != null ) {
                networkAnimator.enabled = false;
            }

            // Disable device simulator
            var xrDeviceSimulator = GetComponentInChildren<XRDeviceSimulator>();
            if( xrDeviceSimulator != null ) {
                xrDeviceSimulator.enabled = false;
            }

            // Disable hand visualizer
            var handVisuals = GetComponentsInChildren<HandVisualizer>();
            foreach( var visual in handVisuals ) {
                visual.enabled = false;
            }

            // Disable interaction manager
            var interactionManagers = GetComponentsInChildren<XRInteractionManager>();
            foreach( var manager in interactionManagers ) {
                manager.enabled = false;
            }

        }

    }
}
