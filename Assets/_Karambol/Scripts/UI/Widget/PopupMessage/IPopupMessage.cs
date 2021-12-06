using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InternshipUnity3D.Widget {
    public interface IPopupMessage {
        void Show(string message, string titleMessage, params PopupButton[] PopupButton);
        void SetSize(float widthContainer, float heightContainer);
        void SetImage(Sprite sprite);
        void Hide();
        void SetAsyncImage(string imageUrl);
        void RefreshRectTransforms();
        void AddButton(PopupButton popupButton);
    }
}