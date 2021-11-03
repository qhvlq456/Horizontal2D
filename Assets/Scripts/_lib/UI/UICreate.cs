using UnityEngine;

namespace HorizontalGame
{
    public class UICreate : MonoBehaviour
    {
        private Canvas canvas;
        private GameObject prefeb;
        public GameObject CreateUI(string objectName, string prefebName)
        {
            canvas = GameObject.Find(objectName).GetComponent<Canvas>();
            prefeb = Resources.Load(prefebName) as GameObject;

            return Instantiate(prefeb, canvas.transform);
        }
    }
}
