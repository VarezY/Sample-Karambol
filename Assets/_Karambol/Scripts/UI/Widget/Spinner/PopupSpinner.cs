using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InternshipUnity3D.Widget
{
    [RequireComponent(typeof(RectTransform))]
    public class PopupSpinner : MonoBehaviour
    {

        Vector2 initSize;
        [SerializeField] TMPro.TextMeshProUGUI labelText;
        [SerializeField] Image overlay;
        [SerializeField] RectTransform rectTransform;
        void Awake()
        {
            initSize = rectTransform.sizeDelta;
        }

        public void Show(float posX, float posY, string text, bool isBlocking)
        {
            labelText.text = text;

            Vector2 additionalSize = labelText.GetPreferredValues(text);
            additionalSize.y = 0;
            if (isBlocking)
            {
                this.overlay.gameObject.SetActive(true);
            }
            this.rectTransform.anchoredPosition = new Vector2(posX, posY);
            this.rectTransform.sizeDelta = initSize + additionalSize;
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
            this.overlay.gameObject.SetActive(false);
        }
        public void Hide(float delayTime)
        {
            StartCoroutine(IEHide(delayTime));
        }

        IEnumerator IEHide(float delayTime){
            yield return new WaitForSeconds(delayTime);
            this.gameObject.SetActive(false);
            this.overlay.gameObject.SetActive(false);
        }
    }

}