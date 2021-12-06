using System;
using UnityEngine;
using UnityEngine.UI;

namespace InternshipUnity3D.Widget
{
    public class WidgetRoot : MonoBehaviour
    {

        // --- Object references
        public IPopupMessage popupMessage
        {
            get;
            private set;
        }
        public PopupSpinner popupSpinner
        {
            get;
            private set;
        }

        // --- Game Object references
        public GameObject PopupGameObject
        {
            get;
            private set;
        }
        public GameObject PopupSpinnerGameObject
        {
            get;
            private set;
        }


        Transform widgetParent;
        private CanvasScaler canvasScaler;

        // Use this for initialization
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            this.widgetParent = this.transform;
            this.canvasScaler = GetComponent<CanvasScaler>();
        }

        void OnDestroy()
        {
            Destroy(this.gameObject);
        }

        void Start()
        {
            // if (this.transform.childCount > 0 && this.transform.GetChild(0).name == "SafeArea")
            // {
            //     this.widgetParent = this.transform.GetChild(0);
            // }
        }


        public void SetCanvasScalerMatchWidthOrHeight(float value)
        {
            this.canvasScaler.matchWidthOrHeight = value;
        }

        public void SetScaleMode(CanvasScaler.ScaleMode scaleMode)
        {
            this.canvasScaler.uiScaleMode = scaleMode;
        }

        public static GameObject CreateInstance(GameObject origin)
        {
            return Instantiate(origin, WidgetProxy.Root.transform);
        }

        public void ShowPopup(string message)
        {
            ShowPopup(message, null);
        }

        public IPopupMessage ShowPopup(string message, string titleMessage, params PopupButton[] popupButton)
        {
            // using proxy pattern to instantiate popup
            if (this.popupMessage == null)
            {
                this.PopupGameObject = Instantiate(Resources.Load("Prefabs/UI/Popup"), this.widgetParent) as GameObject;
                this.popupMessage = PopupGameObject.GetComponent<PopupMessage>();
            }
            this.popupMessage.Show(message, titleMessage, popupButton);
            this.popupMessage.SetImage(null);
            this.PopupGameObject.transform.SetAsLastSibling();

            return this.popupMessage;
        }

        public IPopupMessage ShowPopup(string message, string titleMessage, float widthContainer, float heightContainer, params PopupButton[] popupButton)
        {
            ShowPopup(message, titleMessage, popupButton);
            this.popupMessage.SetSize(widthContainer, heightContainer);
            this.popupMessage.SetImage(null);

            return this.popupMessage;
        }
        float sizeScalerDefault = 0.0f;
        public IPopupMessage ShowPopup(string message, string titleMessage, float sizeCanvasScaler, params PopupButton[] popupButton)
        {
            if (this.popupMessage == null)
            {
                this.PopupGameObject = Instantiate(Resources.Load("Prefabs/UI/Popup"), this.widgetParent) as GameObject;
                this.popupMessage = PopupGameObject.GetComponent<PopupMessage>();
            }
            this.popupMessage.Show(message, titleMessage, popupButton);
            this.popupMessage.SetImage(null);
            this.PopupGameObject.transform.SetAsLastSibling();
            UnityEngine.UI.CanvasScaler canvasScaler = GetComponent<UnityEngine.UI.CanvasScaler>();
            sizeScalerDefault = canvasScaler.matchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = sizeCanvasScaler;

            return this.popupMessage;
        }

        public IPopupMessage ResizeDefaultSizeWidgetRoot()
        {
            UnityEngine.UI.CanvasScaler canvasScaler = GetComponent<UnityEngine.UI.CanvasScaler>();
            canvasScaler.matchWidthOrHeight = sizeScalerDefault;
            return this.popupMessage;
        }

        public IPopupMessage ShowPopup(string message, string titleMessage, float widthContainer, float heightContainer, Sprite topSprite, params PopupButton[] popupButton)
        {
            ShowPopup(message, titleMessage, popupButton);
            this.popupMessage.SetSize(widthContainer, heightContainer);
            this.popupMessage.SetImage(topSprite);

            return this.popupMessage;
        }

        public void HidePopup()
        {
            if (this.popupMessage != null)
            {
                this.popupMessage.Hide();
            }
        }

        public void ShowSpinner(float posX = 0, float posY = 0, string text = "", bool isBlocking = false)
        {
            if (this.popupSpinner == null)
            {
                this.PopupSpinnerGameObject = Instantiate(Resources.Load("Widgets/UI/Loading"), this.widgetParent) as GameObject;
                this.popupSpinner = PopupSpinnerGameObject.GetComponent<PopupSpinner>();
            }
            this.popupSpinner.Show(posX, posY, text, isBlocking);
            this.PopupSpinnerGameObject.transform.SetAsLastSibling();
        }

        public void HideSpinner()
        {
            if (this.popupSpinner != null)
            {
                this.popupSpinner.Hide();
            }
        }

         public void HideSpinnerWithDelay(float delayTime)
        {
            if (this.popupSpinner != null)
            {
                this.popupSpinner.Hide(delayTime);
            }
        }

    }
}