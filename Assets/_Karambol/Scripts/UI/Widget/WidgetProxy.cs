using UnityEngine;

namespace InternshipUnity3D.Widget {
    public class WidgetProxy {
        public static WidgetProxy Instance {
            get {
                if (instance == null) {
                    instance = new WidgetProxy();
                }

                return instance;
            }
        }
        private static WidgetProxy instance = null;

        public static WidgetRoot Root {
            get {
                if (instance == null) {
                    instance = new WidgetProxy();
                }
                return instance.GetRoot();
            }
        }

        GameObject _widgetProxyObj;
        WidgetProxyHolder _widgetProxyHolder;

        /// <summary>
        /// Add widget proxy holder component to widget proxy holder
        /// </summary>
        public WidgetProxy() {
            _widgetProxyObj = new GameObject();
            // _widgetProxyObj.transform.SetParent(GameObject.Find("_Dynamic").transform);
            _widgetProxyObj.name = "_widgetProxyHolder";
            _widgetProxyHolder = _widgetProxyObj.AddComponent<WidgetProxyHolder>();
        }

        /// <summary>
        /// Get root widget
        /// </summary>
        public WidgetRoot GetRoot() {
            return _widgetProxyHolder.widgetRoot;
        }
        
    }
}