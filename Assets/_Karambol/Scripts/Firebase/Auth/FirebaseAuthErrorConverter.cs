using Firebase.Auth;
using UnityEngine;

namespace DisassemblAR.Scripts.Firebase.Auth
{
    public class FirebaseAuthErrorConverter
    {
        public static string ConvertAuthErrorCode(AuthError _errorCode)
        {
            string result = "";
            switch (_errorCode)
            {
                case AuthError.UserNotFound:
                    result = "Email or Password is wrong, please try again !";
                    break;
                case AuthError.WrongPassword:
                    result = "Email or Password is wrong, please try again !";
                    break;
                case AuthError.EmailAlreadyInUse:
                    result = "Email has been used, use another Email !";
                    break;
                case AuthError.InvalidEmail:
                    result = "Email is invalid, please use another email !";
                    break;
                case AuthError.MissingEmail:
                    result = "Email is empty !";
                    break;
                case AuthError.MissingPassword:
                    result = "Password is empty !";
                    break;
                case AuthError.UserDisabled:
                    result = "User is currenly dissabled !";
                    break;
                case AuthError.WeakPassword:
                    result = "Password is Weak! Try another password combination !";
                    break;
                case AuthError.Failure:
                    result = "Error, something went wrong in Internal. Please try again";
                    break;
                default:
                    result = "Error, something went wrong. Please try again";
                    break;
            }
            return result;
        }
    }
}