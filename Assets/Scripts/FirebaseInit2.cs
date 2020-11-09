using Firebase;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit2 : MonoBehaviour
{
    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    void Start()
    {
        GameManager.instance.SetStatus("Firebase initializing");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
                GameManager.instance.SetStatus("Firebase Error");
                return;
            }

            GameManager.instance.SetStatus("Firebase Initialized");
            OnFirebaseInitialized.Invoke();
       });
    }
}
