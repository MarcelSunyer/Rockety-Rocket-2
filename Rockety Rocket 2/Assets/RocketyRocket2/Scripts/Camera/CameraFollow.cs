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
                targetPos.y = shipPos.y + deadZoneSize.y * 0.5f; 
            }

            if (shipPos.y > top) 
            { 
                targetPos.y = shipPos.y - deadZoneSize.y * 0.5f; 
            }

            targetPos.z = camPos.z;  
            transform.position = Vector3.SmoothDamp(camPos, targetPos, ref velocity, smoothSpeed) ;


        }
    }
}

