using System;
using System.Collections;
using System.Collections.Generic;
using _Karambol.Scripts.Event;
using Firebase.Auth;
using InternshipUnity3D.Widget;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseUser _user;
    
    [Header("UI Container")] 
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject menuUI;
    public GameObject userSetting;

    [Header("Login Form")] 
    public TMP_InputField _emailField;
    public TMP_InputField _passwordField;
    public Button anonymousLogin;
    
    [Header("Button")] 
    public Button loginButton;
    public Button registerButton;

    [Header("MenuUI")] 
    public TMP_Text _userName;
    public Image _userImage;
    public Button logoutButton;

    [Header("User Setting")] 
    public TMP_InputField nameUser;
    public TMP_InputField passwordUser;
    public TMP_InputField confPasswordUser;
    public Button saveButton;

    private IEnumerator Start()
    {
        yield return Backend.Instance.CheckSession();
        _user = Backend.Instance._user;
        if (_user != null)
        {
            ShowMenu();
        }
        else
        {
            menuUI.SetActive(false);
            loginUI.SetActive(true);
        }

        #region Login

        loginButton.onClick.AddListener(delegate { LoginUser(); });
        anonymousLogin.onClick.AddListener(delegate { LoginAnonymous(); });

        #endregion
        
        logoutButton.onClick.AddListener(delegate { Logout(); });

        AuthEvent.current.onLoginEvent += ShowMenu;
    }

    private void Menu()
    {
        if (String.IsNullOrWhiteSpace(Backend.Instance._user.DisplayName))
        {
            userSetting.SetActive(true);
            nameUser.text = Backend.Instance._user.DisplayName;
        }
        _userName.text = Backend.Instance._user.DisplayName;
    }
    
    private void LoginUser()
    {
        if (EmailValidation() != "valid")
        {
            WidgetProxy.Root.ShowPopup(EmailValidation(), "Error", new PopupButton("Okay"));
        }
        else
        {
            StartCoroutine(Backend.Instance.LoginEmail(_emailField.text, _passwordField.text));
        }
    }

    private void LoginAnonymous()
    {
        StartCoroutine(Backend.Instance.LoginAnonymous());
    }
    
    private string EmailValidation()
    {
        string message = string.Empty;
        if (String.IsNullOrEmpty(_emailField.text))
        {
            message = "Email kosong!";
        }
        else if (String.IsNullOrEmpty(_passwordField.text))
        {
            message = "Password kosong!";
        }
        else
        {
            message = "valid";
        }
        
        return message;
    }

    private void UpdateUserMetadata()
    {
        
    }
    
    private void Logout()
    {
        Backend.Instance.LogoutAuth();
        menuUI.SetActive(false);
        loginUI.SetActive(true);
    }

    private void ShowMenu()
    {
        loginUI.SetActive(false);
        menuUI.SetActive(true);
        Menu();
    }

    private void OnDestroy()
    {
        AuthEvent.current.onLoginEvent -= ShowMenu;
    }
}
