using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(RawImage))]
public class PictureLoader : MonoBehaviour {

    public Image defaultImage;

    private FacebookManager facebookManager;

    void Start()
    {
        facebookManager = FacebookManager.Instance;
    }

    public void LoadPicture(Texture2D profilePicture)
    {
        AssignProfilePicture(profilePicture);
    }

    private void AssignProfilePicture(Texture2D profilePicture)
    {
        if (profilePicture != null)
        {
            if (GetComponent<RawImage>() != null)
            {
                defaultImage.color = new Color(0, 0, 0, 0);
                GetComponent<RawImage>().texture = profilePicture;
            }
        }
    }

    private bool IsProfilePictureEmpty()
    {
        return GetComponent<RawImage>().texture == null;
    }
}
