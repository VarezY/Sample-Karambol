using System;
using UnityEngine;

namespace InternshipUnity3D.Widget {
    public class PopupButton {

        public string label;
        public Action buttonAction;
        public bool isHidePopup = true;
        public Color buttonColor = new Color(0, 0.7f, 1f);
        public Color labelColor = Color.white;

        public bool clicked {
            get;
            private set;
        }

        public PopupButton(string label, Action buttonAction = null, bool isHidePopup = true) {
            this.label = label;
            this.buttonAction = buttonAction;
            this.isHidePopup = isHidePopup;
        }

        public PopupButton(string label, Color buttonColor, Color labelColor, Action buttonAction = null, bool isHidePopup = true) {
            this.label = label;
            this.buttonAction = buttonAction;
            this.isHidePopup = isHidePopup;
            this.buttonColor = buttonColor;
            this.labelColor = labelColor;
        }

        public void Init() {
            clicked = false;
        }

        public void Click() {
            clicked = true;
        }

    }
}