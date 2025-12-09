using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform ship;
        [SerializeField] private Vector2 deadZoneSize = new Vector2(2f, 2f); // width, height
        [SerializeField] private float smoothSpeed = 0.2f;

        private Vector3 velocity = Vector3.zero;

        void LateUpdate()
        {
            if (ship == null) return;

            Vector3 camPos = transform.position;
            Vector3 shipPos = ship.position;

            // define dead zone around camera center
            float left = camPos.x - deadZoneSize.x * 2;
            float right = camPos.x + deadZoneSize.x * 2;
            float bottom = camPos.y + deadZoneSize.y * 2;
            float top = camPos.y - deadZoneSize.y * 2;

            Vector3 targetPos = camPos;
            //Debug.Log(camPos.x);
            //Debug.Log(deadZoneSize.x);

            if (shipPos.x < left)
            {
                targetPos.x = shipPos.x + deadZoneSize.x * 2;
            }                                              
                                                           
            if (shipPos.x > right)                         
            {                                              
                targetPos.x = shipPos.x - deadZoneSize.x * 2;
            }                                              
                                                           
            if (shipPos.y < bottom)                        
            {                                              
                targetPos.y = shipPos.y + deadZoneSize.y * 2; 
            }                                              
                                                           
            if (shipPos.y > top)                           
            {                                              
                targetPos.y = shipPos.y - deadZoneSize.y * 2; 
            }

            targetPos.z = camPos.z; 
            transform.position = Vector3.SmoothDamp(camPos, targetPos, ref velocity, smoothSpeed);
        }
    }
}

