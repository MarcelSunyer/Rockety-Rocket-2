using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RocketyRocket2
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform ship;
        [SerializeField] private Vector2 deadZoneSize = new Vector2(2f, 2f); 
        [SerializeField] private float smoothSpeed = 0.2f;
        [SerializeField] private float lookAheadFactor = 1.5f; 
        [SerializeField] private float maxLookAheadDistance = 3f; 
        [SerializeField] private float lookAheadSmoothTime = 0.3f;
        [SerializeField] private GameObject boost;
        public bool startGame = false;
        public bool firstGames = false;

        private bool waitSeconds = false;
        private Vector3 velocity = Vector3.zero;
        private Vector3 lookAheadVelocity = Vector3.zero;
        private ShipController shipController;
        private Rigidbody2D shipRigidbody;
        private Vector3 targetLookAheadPosition;

        public bool pressanykey = true;

        private void Start()
        {
            if (ship != null)
            {
                shipController = ship.GetComponent<ShipController>();
                shipRigidbody = ship.GetComponent<Rigidbody2D>();
            }

            StartCoroutine(ActiveBoost());
            targetLookAheadPosition = transform.position;
        }

        void LateUpdate()
        {
            if (ship == null || shipController == null || shipRigidbody == null)
                return;

            if (!startGame)
            {
                if (shipController.started)
                {
                    StartCoroutine(StartGamePlay());
                }
                return;
            }

            if (!waitSeconds) return;
            if (ship == null) return;

            Vector3 camPos = transform.position;
            Vector3 shipPos = ship.position;

            // Calculate look ahead position based on ship's velocity
            Vector2 shipVelocity = shipRigidbody.linearVelocity;

            // Normalize velocity to get direction
            float velocityMagnitude = shipVelocity.magnitude;
            Vector2 velocityDirection = velocityMagnitude > 0.1f ? shipVelocity.normalized : Vector2.zero;

            // Calculate look ahead offset based on velocity direction and magnitude
            Vector2 lookAheadOffset = Vector2.zero;

            if (velocityMagnitude > 0.1f)
            {
                // Calculate base look ahead offset
                float lookAheadAmount = Mathf.Min(velocityMagnitude * lookAheadFactor, maxLookAheadDistance);
                lookAheadOffset = velocityDirection * lookAheadAmount;
            }

            // Smoothly interpolate the look ahead position
            Vector3 desiredLookAheadPosition = shipPos + (Vector3)lookAheadOffset;
            targetLookAheadPosition = Vector3.SmoothDamp(
                targetLookAheadPosition,
                desiredLookAheadPosition,
                ref lookAheadVelocity,
                lookAheadSmoothTime
            );

            // Use the look ahead position for dead zone calculations
            Vector3 effectiveShipPos = targetLookAheadPosition;

            // Define dead zone around camera center
            float left = camPos.x - deadZoneSize.x * 0.5f;
            float right = camPos.x + deadZoneSize.x * 0.5f;
            float bottom = camPos.y - deadZoneSize.y * 0.5f;
            float top = camPos.y + deadZoneSize.y * 0.5f;

            Vector3 targetPos = camPos;

            // Check if ship's look ahead position is outside dead zone
            if (effectiveShipPos.x < left)
            {
                targetPos.x = effectiveShipPos.x + deadZoneSize.x * 0.5f;
            }
            else if (effectiveShipPos.x > right)
            {
                targetPos.x = effectiveShipPos.x - deadZoneSize.x * 0.5f;
            }

            if (effectiveShipPos.y < bottom)
            {
                targetPos.y = effectiveShipPos.y + deadZoneSize.y * 0.5f;
            }
            else if (effectiveShipPos.y > top)
            {
                targetPos.y = effectiveShipPos.y - deadZoneSize.y * 0.5f;
            }

            targetPos.z = camPos.z;
            transform.position = Vector3.SmoothDamp(camPos, targetPos, ref velocity, smoothSpeed);
        }

        private IEnumerator StartGamePlay()
        {
            if (pressanykey)
            {
                ship.GetComponent<ShipController>().StartCoroutine(
                    ship.GetComponent<ShipController>().StartShip()
                );

                yield return new WaitForSeconds(0.5f);
                Tween tween = gameObject.GetComponent<Camera>().DOOrthoSize(3.75f, 2);
                tween.Play();
            }
            startGame = true;
            
        }

        private IEnumerator ActiveBoost()
        {
            if (firstGames)
                yield return new WaitForSeconds(0.5f);

            waitSeconds = true;
            yield return new WaitForSeconds(0.7f);
            boost.SetActive(true);
        }
    }
}