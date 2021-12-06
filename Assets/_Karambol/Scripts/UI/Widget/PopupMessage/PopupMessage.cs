using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace InternshipUnity3D.Widget {
    public class PopupMessage : MonoBehaviour, IPopupMessage {
        // enum ButtonLayouts
        public enum ButtonLayouts {
            Horizontal,
            Vertical
        }

        public RectTransform containerPopup;

        [SerializeField] TextMeshProUGUI _titleMessageText;
        [SerializeField] TextMeshProUGUI _messageText;

        [SerializeField] Transform _buttonContainer;

        [SerializeField] Button _buttonTemplate;

        [SerializeField] Image _topImage;

        [SerializeField] Image _imageLoadingProgress;

        [Header("Settings")]
        public TMP_FontAsset[] _textAssets;

        List<Button> _buttons = new List<Button>();
        Vector2 _initSize, _initAnchorMin, _initAnchorMax;

        protected RectTransform _rectTransform;
        Coroutine _downloadImageRoutine;

        string _previousDownloadedImageUrl;

        void Awake() {
            this._rectTransform = GetComponent<RectTransform>();

            RectTransform messageRT = _messageText.GetComponent<RectTransform>();
            _initSize = messageRT.sizeDelta;
            _initAnchorMin = messageRT.anchorMin;
            _initAnchorMax = messageRT.anchorMax;
        }

        virtual public void Start() {
            _buttonTemplate.gameObject.SetActive(false);
        }

        public void Hide() {
            this.gameObject.SetActive(false);
        }

        virtual public void Show(string message, string titleMessage = null, params PopupButton[] popupButtons) {
            if(string.IsNullOrEmpty(titleMessage)){
                _titleMessageText.gameObject.SetActive(false);
            }else{
                _titleMessageText.text = titleMessage;
            }

            _messageText.text = message;

            this.gameObject.SetActive(true);

            // destroy previous buttons
            for (int itButton = 0; itButton < _buttons.Count; itButton++) {
                Destroy(_buttons[itButton].gameObject);
            }
            _buttons.Clear();

            // add buttons
            if (popupButtons != null && popupButtons.Length > 0) {
                _buttonContainer.gameObject.SetActive(true);
                RectTransform messageRT = _messageText.GetComponent<RectTransform>();
                messageRT.anchorMin = _initAnchorMin;
                messageRT.anchorMax = _initAnchorMax;
                messageRT.sizeDelta = _initSize;

                for (int itPopupButton = 0; itPopupButton < popupButtons.Length; itPopupButton++) {
                    PopupButton popupButton = popupButtons[itPopupButton];
                    AddButton(popupButton);
                }
            } else {
                _buttonContainer.gameObject.SetActive(false);
                RectTransform messageRT = _messageText.GetComponent<RectTransform>();
                messageRT.anchorMin = Vector2.zero;
                messageRT.anchorMax = Vector2.one;

                messageRT.offsetMin = Vector2.zero;
                messageRT.offsetMax = Vector2.zero;
                messageRT.sizeDelta = _initSize;

            }
            RefreshRectTransforms();
        }

        public void AddButton(PopupButton popupButton) {

            popupButton.Init();

            GameObject buttonObject = Instantiate(_buttonTemplate.gameObject, _buttonContainer) as GameObject;
            buttonObject.SetActive(true);

            Button button = buttonObject.GetComponent<Button>();

            if (button != null) {
                button.targetGraphic.color = popupButton.buttonColor;

                TextMeshProUGUI buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null) {
                    buttonText.text = popupButton.label;
                    buttonText.color = popupButton.labelColor;
                } else {
                    Debug.LogError("Button doesn't have text component");
                }

                button.onClick.AddListener(() => {
                    popupButton.Click();
                    if (popupButton.buttonAction != null) {
                        popupButton.buttonAction();
                    }
                    // HIDE POPUP by default
                    if (popupButton.isHidePopup) {
                        Hide();
                    }
                });

                _buttons.Add(button);
            } else {
                Debug.LogError("Button object doesn't have button component");
            }
        }

        public void SetSize(float widthContainer, float heightContainer) {
            containerPopup.sizeDelta = new Vector2(widthContainer, heightContainer);
        }

        // Set image on Popup widget
        public void SetImage(Sprite sprite) {
            if (sprite != null) {
                this._topImage.gameObject.SetActive(true);
                this._topImage.color = new Color(1, 1, 1, 1);
                this._topImage.sprite = sprite;
                this._topImage.preserveAspect = true;
                this._topImage.gameObject.SetActive(true);
            } else {
                this._topImage.gameObject.SetActive(false);
                this._topImage.color = new Color(1, 1, 1, 0);
                this._topImage.gameObject.SetActive(false);
            }
        }

        public void SetAsyncImage(string imageUrl) {
            if (_downloadImageRoutine != null) {
                StopCoroutine(_downloadImageRoutine);
            }
            _downloadImageRoutine = StartCoroutine(IEAsyncImage(imageUrl));
        }

        IEnumerator IEAsyncImage(string imageUrl) {

            if (this._previousDownloadedImageUrl != imageUrl) {
                // download new image
                this._imageLoadingProgress.fillAmount = 0;
                this._topImage.gameObject.SetActive(true);
                this._topImage.sprite = null;

                var uwr = new UnityWebRequest(imageUrl);
                uwr.method = UnityWebRequest.kHttpVerbGET;
                var dh = new DownloadHandlerTexture();
                uwr.downloadHandler = dh;

                uwr.SendWebRequest();
                while (!uwr.isDone) {
                    this._imageLoadingProgress.fillAmount = uwr.downloadProgress;
                    yield return null;
                }

                var sprite = ProjectUtility.ConvertTexture2DToSprite(dh.texture);
                this._topImage.sprite = sprite;

                this._imageLoadingProgress.fillAmount = 0;
                this._topImage.color = Color.white;

                this._previousDownloadedImageUrl = imageUrl;
            } else {
                // we have downloaded image before this
                // immediately show it to user
                this._topImage.gameObject.SetActive(true);
                this._imageLoadingProgress.fillAmount = 0;
            }

        }

        protected TMP_FontAsset GetFont(string textAssetName) {
            for (int i = 0; i < _textAssets.Length; i++) {
                if (_textAssets[i].name == textAssetName) {
                    return _textAssets[i];
                }
            }
            return null;
        }

        public void SetMessageFont(string textAssetName) {
            var font = GetFont(textAssetName);
            if (font != null) {
                this._messageText.font = font;
            } else {
                Debug.Log("Text asset " + textAssetName + " not found");
            }
        }

        public void SetMessageColor(Color color) {
            this._messageText.color = color;
        }

        public void RefreshRectTransforms() {
            var rectTransforms = this.transform.GetComponentsInChildren<RectTransform>();
            for (int i = 0; i < rectTransforms.Length; i++) {
                rectTransforms[i].ForceUpdateRectTransforms();
            }
        }
    }
}