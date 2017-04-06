using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class FacebookManager : Singleton<FacebookManager>
{
    [System.Serializable]
    public class UserProfilePictureLoadedEvent : UnityEvent<Texture2D> { };

    [System.Serializable]
    public class LoginEvent : UnityEvent<FacebookUser> { };

    public UserProfilePictureLoadedEvent userProfilePictureLoaded;
    public LoginEvent loginSuccess;
    public UnityEvent loginFail;

    public FacebookUser User { get; set; }
    
    private FacebookManager() { }

    new void Awake()
    {
        base.Awake();
        User = new FacebookUser();

        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            InitializeApp();
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            InitializeApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void InitializeApp()
    {
        FB.ActivateApp();
        if (FB.IsLoggedIn)
        {
            SetUpFacebookUser();
        }
    }

    public void Login()
    {
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
       // FB.LogInWithPublishPermissions(perms, LoginCallback);
        FB.LogInWithReadPermissions(perms, null);
        FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, LoginCallback);
    }

    private void LoginCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            SetUpFacebookUser();
        }
        else
        {
            loginFail.Invoke();
        }
    }

    private void SetUpFacebookUser()
    {
        User.Id = AccessToken.CurrentAccessToken.UserId;

        GetUserInfo();
        GetUserPicture();
    }

    private void GetUserInfo()
    {
        string parameters = ConstructFieldsParameters(new string[] { FacebookParameters.User.FirstNameFieldName, FacebookParameters.User.LastNameFieldName });
        string path = "/" + User.Id + parameters;
        FB.API(path, HttpMethod.GET, GetUserInfoCallback);
    }

    private string ConstructFieldsParameters(string[] parameterList)
    {
        return "?fields=" + String.Join(",", parameterList);
    }

    private void GetUserInfoCallback(IGraphResult result)
    {
        if (!WasErrorInResult(result))
        {
            User.FirstName = result.ResultDictionary[FacebookParameters.User.FirstNameFieldName].ToString();
            User.LastName = result.ResultDictionary[FacebookParameters.User.LastNameFieldName].ToString();
            loginSuccess.Invoke(User);
        }
        else
        {
            loginFail.Invoke();
        }
    }

    private void GetUserPicture()
    {
        string path = "/" + User.Id + "/" + FacebookParameters.User.ProfilePictureEdgeName + "?redirect=false" +
            "&fields=" + FacebookParameters.User.ProfilePicture.DaraUrlFieldName +
            "&width=" + FacebookParameters.User.ProfilePicture.Width.ToString();
        FB.API(path, HttpMethod.GET, GetUserPictureCallback);
    }

    private void GetUserPictureCallback(IGraphResult result)
    {
        if (!WasErrorInResult(result))
        {
            IDictionary dataDictionary = result.ResultDictionary[FacebookParameters.User.ProfilePicture.DataFieldName] as IDictionary;
            User.ProfilePicturePath = dataDictionary[FacebookParameters.User.ProfilePicture.DaraUrlFieldName].ToString();
            LoadProfilePicture(User.ProfilePicturePath);
        }
    }

    public void LoadProfilePicture(string url)
    {
        StartCoroutine(coLoad(url));
    }

    private IEnumerator coLoad(string url)
    {
        Texture2D profilePicture = new Texture2D(FacebookParameters.User.ProfilePicture.Width,
                                            FacebookParameters.User.ProfilePicture.Height,
                                            TextureFormat.DXT1, false);

        WWW www = new WWW(url);
        yield return www;

        www.LoadImageIntoTexture(profilePicture as Texture2D);

        userProfilePictureLoaded.Invoke(profilePicture);
    }

    private bool WasErrorInResult(IResult result)
    {
        return !String.IsNullOrEmpty(result.Error);
    }

    public void SharePost()
    {
        if (AccessToken.CurrentAccessToken.Permissions.Contains("publish_actions"))
        {
            FB.ShareLink(new Uri("https://www.facebook.com/Grupa.NET.POLSL/"), "Ale super!", "Umiem juz uzywac Facebooka w swojej grze;)");
        }
    }   
}
