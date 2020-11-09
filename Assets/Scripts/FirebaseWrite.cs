using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class FirebaseWrite : MonoBehaviour
{
    private const string PLAYER_KEY = "PLAYER_KEY";
    FirebaseDatabase database;



    void Start()
    {
        // Can only call DefaultInstance now because I called check and fix dependencies async in my Firebase init script
        // Failure to do so can result in an exception on some Android devices
        database = FirebaseDatabase.DefaultInstance;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            string player = "Alpha";
            string json = JsonUtility.ToJson(player);
            PlayerPrefs.SetString(PLAYER_KEY, json);
            database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(json);
        }
    }
}
