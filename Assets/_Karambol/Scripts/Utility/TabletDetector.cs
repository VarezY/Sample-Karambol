using UnityEngine;

namespace InternshipUnity3D
{
    public class TabletDetector : MonoBehaviour
    {
        /// <summary>  
        /// Tablet device detection
        /// </summary>
        public static bool TabletDevice()
        {
            bool tablet = false;

            if (Application.platform == RuntimePlatform.Android)
            {
                float size = DeviceDiagonalSizeInInches2();
                if (size > 6)
                {
                    tablet = true;
                }
                else
                {
                    tablet = false;
                }
            }
#if UNITY_IOS
        if (SystemInfo.deviceModel.Contains("iPad")) {
            tablet = true;
        } else {
            tablet = false;
        }
#endif

            return tablet;
        }

        /// <summary>  
        /// calculate physical inches with pythagoras theorem
        /// </summary>
        public static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            Debug.Log("Getting device inches: " + diagonalInches);

            return diagonalInches;
        }

        /// <summary>  
        /// calculate physical inches with pythagoras theorem alternative
        /// </summary>
        public static float DeviceDiagonalSizeInInches2()
        {
            float inch = Mathf.Sqrt((Screen.width * Screen.width) + (Screen.height * Screen.height));
            inch = inch / Screen.dpi;

            return inch;
        }
    }
}