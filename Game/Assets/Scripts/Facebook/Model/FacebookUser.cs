using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class FacebookUser
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicturePath { get; set; }
    public Texture2D ProfilePicture { get; set; }
}