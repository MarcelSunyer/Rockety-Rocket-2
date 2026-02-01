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

        // Configuración de la línea discontinua
        [Header("Línea Discontinua")]
        [SerializeField] public float squareSize = 0.1f;
        [SerializeField] private float gapBetweenSquares = 0.15f;
        [SerializeField] public Color lineColor = Color.white;
        [SerializeField] private float lineThickness = 2f;

        private GameObject Blade;
        private bool isMoving = false;
        private int currentPositionIndex = 0;
        private Material lineMaterial;

        // Start is called before the first frame update
        void Start()
        {
            Blade = this.gameObject;


            StartCoroutine(BladeMovement());
        }

        // Crear material para dibujo con GL


        // Dibujar un segmento discontinuo usando GL
        private void DrawDottedSegment(Vector2 start, Vector2 end, System.Action<Vector2, float> drawCircle)
        {
            float distance = Vector2.Distance(start, end);
            if (distance < 0.01f) return;

            Vector2 direction = (end - start).normalized;

            float totalLength = squareSize + gapBetweenSquares;
            int numberOfDots = Mathf.FloorToInt(distance / totalLength);

            if (numberOfDots < 1 && distance > squareSize)
                numberOfDots = 1;

            float actualGap = (distance - (numberOfDots * squareSize)) / Mathf.Max(1, numberOfDots - 1);
            float currentPos = squareSize / 2f;

            for (int i = 0; i < numberOfDots; i++)
            {
                Vector2 center = start + direction * currentPos;
                drawCircle(center, squareSize / 2f);
                currentPos += squareSize + actualGap;

                if (currentPos > distance - squareSize / 2f) break;
            }
        }


        private void DrawCircleGL(Vector2 center, float radius)
        {
            int segments = 12;
            GL.Begin(GL.TRIANGLES);
            for (int i = 0; i < segments; i++)
            {
                float a1 = Mathf.Deg2Rad * 360f / segments * i;
                float a2 = Mathf.Deg2Rad * 360f / segments * (i + 1);

                Vector2 p1 = center;
                Vector2 p2 = center + new Vector2(Mathf.Cos(a1), Mathf.Sin(a1)) * radius;
                Vector2 p3 = center + new Vector2(Mathf.Cos(a2), Mathf.Sin(a2)) * radius;

                GL.Vertex3(p1.x, p1.y, 0);
                GL.Vertex3(p2.x, p2.y, 0);
                GL.Vertex3(p3.x, p3.y, 0);
            }
            GL.End();
        }

        private void DrawDottedSegmentGL(Vector2 start, Vector2 end)
        {
            DrawDottedSegment(start, end, DrawCircleGL);
        }


        // Alternativa: Usar GUI para dibujar (funciona en espacio de pantalla)
        void OnGUI()
        {
            if (positionsToMove.Count < 2) return;

            // Crear un estilo para los cuadrados
            GUIStyle squareStyle = new GUIStyle();
            squareStyle.normal.background = MakeCircleTex(32, lineColor);

            // Dibujar cada segmento
            for (int i = 0; i < positionsToMove.Count; i++)
            {
                Vector2 startPoint = positionsToMove[i];
                Vector2 endPoint = positionsToMove[(i + 1) % positionsToMove.Count];

                DrawDottedSegmentGUI(startPoint, endPoint, squareStyle);
            }
        }

        // Dibujar segmento usando GUI (alternativa)
        private void DrawDottedSegmentGUI(Vector2 start, Vector2 end, GUIStyle style)
        {
            DrawDottedSegment(start, end, (center, radius) =>
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(center);
                screenPos.y = Screen.height - screenPos.y;

                float size = radius * 2f * 100f; // Escala para GUI pixels
                Rect rect = new Rect(screenPos.x - size / 2f, screenPos.y - size / 2f, size, size);
                GUI.Box(rect, GUIContent.none, style);
            });
        }


        // Crear textura para GUI
        private Texture2D MakeCircleTex(int size, Color col)
        {
            Texture2D tex = new Texture2D(size, size);
            Color[] pix = new Color[size * size];
            Vector2 center = new Vector2(size / 2f, size / 2f);
            float r = size / 2f;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dist = Vector2.Distance(new Vector2(x, y), center);
                    pix[y * size + x] = dist <= r ? col : Color.clear;
                }
            }

            tex.SetPixels(pix);
            tex.Apply();
            return tex;
        }


        // Para visualización en el editor (Gizmos)
        void OnDrawGizmos()
        {
            if (positionsToMove.Count < 2) return;

            Gizmos.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.7f);

            // Dibujar puntos de control
            for (int i = 0; i < positionsToMove.Count; i++)
            {
                Gizmos.DrawSphere(positionsToMove[i], 0.05f);

                Vector2 start = positionsToMove[i];
                Vector2 end = positionsToMove[(i + 1) % positionsToMove.Count];

                // Dibujar la línea discontinua
                DrawDottedSegmentGizmos(start, end);
            }
        }

        // Dibujar segmento con Gizmos
        private void DrawDottedSegmentGizmos(Vector2 start, Vector2 end)
        {
            float distance = Vector2.Distance(start, end);
            if (distance < 0.01f) return;

            Vector2 direction = (end - start).normalized;

            float totalLength = squareSize + gapBetweenSquares;
            int numberOfDots = Mathf.FloorToInt(distance / totalLength);

            if (numberOfDots < 1 && distance > squareSize)
                numberOfDots = 1;

            float actualGap = (distance - (numberOfDots * squareSize)) / Mathf.Max(1, numberOfDots - 1);
            float currentPos = squareSize / 2f;
            float radius = squareSize / 2f;

            for (int i = 0; i < numberOfDots; i++)
            {
                Vector2 center = start + direction * currentPos;
                Gizmos.DrawSphere(center, radius);

                currentPos += squareSize + actualGap;
                if (currentPos > distance - radius) break;
            }
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (Blade != null)
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
                while (!BladeCanMove)
                    yield return null;

                Tween tween = Blade.transform.DOMove(positionsToMove[currentPositionIndex], MovementSpeed);
                tween.Play();

                yield return tween.WaitForCompletion();
                currentPositionIndex++;
            }

            currentPositionIndex = 0;
            isMoving = false;
            StartCoroutine(BladeMovement());
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
            if (!isMoving)
                StartCoroutine(BladeMovement());
        }

        // Actualizar cuando cambian valores en el inspector
        private void OnValidate()
        {
            // Forzar repintado en el editor
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
#endif
        }

        // Limpiar al destruir
        private void OnDestroy()
        {
            if (lineMaterial != null)
                Destroy(lineMaterial);
        }
    }
}