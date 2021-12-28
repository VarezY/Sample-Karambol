using System;
using System.Collections;
using System.Collections.Generic;
using _Karambol.Scripts.Event;
using DisassemblAR.Scripts.Firebase.Auth;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using InternshipUnity3D.Widget;
using UnityEngine;
using ZXing.OneD.RSS;

public class Backend: MonoBehaviour
{
    private static Backend _instance;

    public static Backend Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Backend>();
                if (_instance == null)
                {
                    GameObject backend = new GameObject();
                    backend.transform.name = "_Backend";
                    _instance = backend.AddComponent<Backend>();
                }
            }
            return _instance;
        }
    }

    private FirebaseAuth _auth;
    
    private void Awake()
    {
        if(_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _auth = FirebaseAuth.DefaultInstance;
        // CheckSession();
    }

    private FirebaseApp _app;
    public FirebaseUser _user { get; private set;}

    [Header("UI")]
    public GameObject _loginUI;
    public GameObject _menuUI;

    public IEnumerator CheckSession()
    {
        var Session = String.Empty;
        var checkApp = FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            var authSession = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
            if (authSession.CurrentUser == null)
            {
                // _loginUI.SetActive(true);
                Debug.Log("No current User Session");
            }
            else
            {
                // _menuUI.SetActive(true);
                _user = authSession.CurrentUser;
                Session = authSession.CurrentUser.UserId;
                Debug.Log($"Current user Session for {authSession.CurrentUser.Email} with userID: {authSession.CurrentUser.UserId}");
                
            }
            _app = authSession.App;
        });
        yield return checkApp;
    }

    public IEnumerator LoginEmail(string email, string password)
    {
        // var auth = FirebaseAuth.DefaultInstance;
        var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);
        if (loginTask.Exception != null)
        {
            // Debug.LogWarning($"Failed to login Task with {loginTask.Exception}");
            foreach (Exception exception in loginTask.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                FirebaseException firebaseEx = exception as FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ",
                        ((AuthError)firebaseEx.ErrorCode).ToString());
                }
                // Debug.LogWarning("number- "+ authErrorCode +"the exception is- "+ exception);
                AuthError code = ((AuthError)firebaseEx.ErrorCode);
                var errorMessage = FirebaseAuthErrorConverter.ConvertAuthErrorCode(code);
                WidgetProxy.Root.ShowPopup(errorMessage, "Error", new PopupButton("Okay"));
            }
        }
        else
        {
            Debug.Log($"Successfully Login User {loginTask.Result.Email} with name {loginTask.Result.DisplayName}");
            WidgetProxy.Root.ShowPopup($"Successfully Login User {loginTask.Result.Email}", "Success", new PopupButton("Okay"));
            _user = loginTask.Result;
            AuthEvent.current.LoginEvent();
        }
    }

    public IEnumerator LoginAnonymous()
    {
        var loginTask = _auth.SignInAnonymouslyAsync();
        yield return new WaitUntil(() => loginTask.IsCompleted);
        if (loginTask.Exception != null)
        {
            // Debug.LogWarning($"Failed to login Task with {loginTask.Exception}");
            foreach (Exception exception in loginTask.Exception.Flatten().InnerExceptions)
            {
                Debug.LogError(exception);
                string authErrorCode = "";
                FirebaseException firebaseEx = exception as FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ",
                        ((AuthError)firebaseEx.ErrorCode).ToString());
                }
                // Debug.LogWarning("number- "+ authErrorCode +"the exception is- "+ exception);
                AuthError code = ((AuthError)firebaseEx.ErrorCode);
                var errorMessage = FirebaseAuthErrorConverter.ConvertAuthErrorCode(code);
                WidgetProxy.Root.ShowPopup(errorMessage, "Error", new PopupButton("Okay"));
            }
        }
        else
        {
            Debug.Log($"Successfully Login User {loginTask.Result.Email} with name {loginTask.Result.DisplayName}");
            WidgetProxy.Root.ShowPopup($"Successfully Login User {loginTask.Result.Email}", "Success", new PopupButton("Okay"));
            _user = loginTask.Result;
            AuthEvent.current.LoginEvent();

        }
    }

    public void TestFacebookLogin()
    {
        // Firebase.Auth.Credential credential =
        //     Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken:);
        
    }
    
    public void LogoutAuth()
    {
        Debug.Log("Logout User: " + _auth.CurrentUser.Email);
        _auth.SignOut();
    }
}
