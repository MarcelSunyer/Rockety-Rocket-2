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
        [SerializeField] private Vector2 deadZoneSize = new Vector2(2f, 2f); // width, height
        [SerializeField] private float smoothSpeed = 0.2f;
        [SerializeField]  private GameObject boost;
        public bool startGame = false;
        public bool firstGames =false;

        private bool waitSeconds = false;
        private Vector3 velocity = Vector3.zero;
        private void Start()
        {
            
            StartCoroutine(ActiveBoost());
        }
        void LateUpdate()
        {
            if (Input.anyKeyDown || Gamepad.current != null && (
                        Gamepad.current.aButton.IsPressed() ||
                        Gamepad.current.bButton.IsPressed() ||
                        Gamepad.current.xButton.IsPressed() ||
                        Gamepad.current.yButton.IsPressed() ||
                        Gamepad.current.rightTrigger.IsPressed() ||
                        Gamepad.current.leftTrigger.IsPressed() ||
                        Gamepad.current.leftShoulder.IsPressed() ||
                        Gamepad.current.leftStick.IsPressed() ||
                        Gamepad.current.leftStickButton.IsPressed() ||
                        Gamepad.current.rightShoulder.IsPressed() ||
                        Gamepad.current.rightStick.IsPressed() ||
                        Gamepad.current.rightStickButton.IsPressed() ||
                        Gamepad.current.leftStick.IsPressed() ||
                        Gamepad.current.rightStick.IsPressed()))
            {

                StartCoroutine(StartGamePlay());
               
                
            }
            if(!startGame)
            {
                return;
            }
            if (!waitSeconds) return;
            if (ship == null) return;

            Vector3 camPos = transform.position;
            Vector3 shipPos = ship.position;

            // define dead zone around camera center
            float left = camPos.x - deadZoneSize.x * 0.5f;
            float right = camPos.x + deadZoneSize.x * 0.5f;
            float bottom = camPos.y - deadZoneSize.y * 0.5f;
            float top = camPos.y + deadZoneSize.y * 0.5f;

            Vector3 targetPos = camPos;

            if (shipPos.x < left)
            {
                targetPos.x = shipPos.x + deadZoneSize.x * 0.5f;
            }

            if (shipPos.x > right)
            {
                targetPos.x = shipPos.x - deadZoneSize.x * 0.5f;
            }

            if (shipPos.y < bottom)
            {
                targetPos.y = (shipPos.y) + (deadZoneSize.y * 0.5f);
            }

            if (shipPos.y > top)
            {
                targetPos.y = (shipPos.y) - (deadZoneSize.y * 0.5f);

            }

            targetPos.z = camPos.z;
            transform.position = Vector3.SmoothDamp(camPos, targetPos, ref velocity, smoothSpeed);
        }
        private IEnumerator StartGamePlay()
        {
            ship.GetComponent<ShipController>().StartCoroutine(
                ship.GetComponent<ShipController>().StartShip()
            );
            yield return new WaitForSeconds(0.5f);
            Tween tween = gameObject.GetComponent<Camera>().DOOrthoSize(3.25f, 2);
            tween.Play();
            startGame = true;
        }
            private IEnumerator  ActiveBoost()
        {
            if (firstGames)
                yield return new WaitForSeconds(0.5f);

            waitSeconds = true;
            yield return new WaitForSeconds(0.7f);
            boost.SetActive(true);

        }
    }
}

