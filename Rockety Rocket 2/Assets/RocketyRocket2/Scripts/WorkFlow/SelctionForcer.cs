using UnityEngine;
using UnityEngine.EventSystems;

namespace RocketyRocket2
{
    public class SelctionForcer : MonoBehaviour
    {
        private GameObject _previusSelection;
        void Update()
        {
        var currentSelection = EventSystem.current.currentSelectedGameObject;
            if (currentSelection != null)
            {
                _previusSelection = currentSelection;
            }
            if (currentSelection == null)
            {
                EventSystem.current.SetSelectedGameObject(_previusSelection);
            }
        }
    }
}
