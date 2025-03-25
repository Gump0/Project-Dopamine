//Class that handles the music that plays in the game.
//If a scene is under the "blackList" section than music is muted.
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public string[] blackList;   // stores list of scenes where music DOES NOT WANT TO BE PLAYED

    AudioSource audio;

    private void OnSceneChanged(Scene oldScene, Scene newScene) { // called upon scene switch
        bool blackListed = false;
        string currentScene = newScene.name;
        
        for(int i = 0; i < blackList.Length; i++) {
            if(blackList[i] == currentScene)
                blackListed = true;
        }
        audio.mute = blackListed;   // mute depending on forloop result

        Debug.Log($"Scene changed from {oldScene.name} to {newScene.name}");
    }

    void Awake() {  // Called upon init in login screen
        if(audio == null) audio = GetComponent<AudioSource>();
        if(audio.clip == null) Debug.LogError("Music Manager: NO MUSIC REFERENCE");
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged; // add OnSceneChanged to eventListener
        audio.Play();
        audio.loop = true;
    }

    void OnDestroy() { // Unsubscribe method called to prevent memory leaks
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
