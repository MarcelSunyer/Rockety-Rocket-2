using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace RocketyRocket2
{
    public class Console_PC : MonoBehaviour
    {
        public GameObject Console;
        public GameObject PC;

        private enum InputType { KeyboardMouse, Gamepad }
        private InputType currentInput;

        void OnEnable()
        {
            InputSystem.onEvent += OnInputEvent;
        }

        void OnDisable()
        {
            InputSystem.onEvent -= OnInputEvent;
        }

        void Start()
        {
            SetInput(InputType.KeyboardMouse);
        }

        void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
        {
            // Solo eventos de estado (input real)
            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                return;

            if (device is Gamepad)
                SetInput(InputType.Gamepad);
            else if (device is Keyboard || device is Mouse)
                SetInput(InputType.KeyboardMouse);
        }

        void SetInput(InputType input)
        {
            if (currentInput == input) return;

            currentInput = input;

            Console.SetActive(input == InputType.Gamepad);
            PC.SetActive(input == InputType.KeyboardMouse);

            Debug.Log("Input actual: " + input);
        }
    }
}
