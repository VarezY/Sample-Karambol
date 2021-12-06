using UnityEngine;

namespace InternshipUnity3D.Widget {
    public class WidgetProxyHolder : MonoBehaviour {

        GameObject _widgetRootObj;

        // widget root
        public WidgetRoot widgetRoot;

        
        void Awake() {
            DontDestroyOnLoad(this.gameObject);
            _widgetRootObj = Instantiate(Resources.Load("Prefabs/UI/WidgetRoot") as GameObject);
            _widgetRootObj.name = "WidgetRoot";
            widgetRoot = _widgetRootObj.GetComponent<WidgetRoot>();
        }

        void OnDestroy() {
            Destroy(_widgetRootObj);
        }
    }
}