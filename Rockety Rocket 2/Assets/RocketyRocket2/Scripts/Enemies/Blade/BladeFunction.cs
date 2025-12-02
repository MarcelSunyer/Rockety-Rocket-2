using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class BladeFunction : MonoBehaviour
    {
        public float RotationSpeed;
        public float MovementSpeed;

        public bool BladeCanMove;

        public List<Vector2> positionsToMove = new List<Vector2>();

        [SerializeField] private ShipController shipController;

        private GameObject Blade;

        private bool isMoving = false;
        private int currentPositionIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            Blade = this.gameObject;

            StartCoroutine(BladeMovement());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Blade.transform.Rotate(0, 0, RotationSpeed * 1.5f);

        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Ship"))
            {
                shipController.ShipDestroyed();
            }
        }

        private IEnumerator BladeMovement()
        {
            isMoving = true;
            while (currentPositionIndex < positionsToMove.Count)
            {
                // Si no se puede mover, espera hasta que BladeCanMove sea true
                while (!BladeCanMove)
                    yield return null;

                Tween tween = Blade.transform.DOMove(positionsToMove[currentPositionIndex], MovementSpeed);
                tween.Play();

                // Espera a que termine o se pause
                yield return tween.WaitForCompletion();

                currentPositionIndex++;
            }

            // Cuando llega al final, resetea al inicio
            currentPositionIndex = 0;
            isMoving = false;
            StartCoroutine(BladeMovement()); // reinicia el loop
        }

        // Llamar para pausar
        private void PauseBlade()
        {
            BladeCanMove = false;
        }

        // Llamar para reanudar
        private void ResumeBlade()
        {
            BladeCanMove = true;
            // Si la coroutine no está corriendo, iníciala
            if (!isMoving)
                StartCoroutine(BladeMovement());
        }
    }
}
