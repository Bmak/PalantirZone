using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthService : ILoggable
{

    private FirebaseAuth _auth = FirebaseAuth.DefaultInstance;


    public void AddNewUser(string email, string password)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Log.Error("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Log.Error("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignInUser(string email, string password)
    {
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Log.Error("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Log.Error("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Log.Debug(string.Format("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId));
        });
    }

    public void SignInWithCredential(string email, string password)
    {
        Credential credential = EmailAuthProvider.GetCredential(email, password);
        _auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Log.Error("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Log.Error("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Log.Debug(string.Format("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId));
        });
    }

    public FirebaseUser GetUserInfo()
    {
        FirebaseUser user = _auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
        return user;
    }

    public void SignOut()
    {
        _auth.SignOut();
    }
}
