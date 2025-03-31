//Class that handles the music that plays in the game.
//If a scene is under the "blackList" section than music is muted.
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public string[] blackList;   // stores list of scenes where music DOES NOT WANT TO BE PLAYED

    AudioSource audio;

    private void DestroyDublicateMusicManager() {
        GameObject[] musicManagers = GameObject.FindGameObjectsWithTag("MusicManager");
        if (musicManagers.Length > 1)
            Destroy(this.gameObject);
    }

    private void CheckIfOnBlackListedScene(string newScene)
    {
        bool blackListed = false;
        for (int i = 0; i < blackList.Length; i++) {
            if (blackList[i] == newScene)
                blackListed = true;
        }
        if(blackListed) audio.Pause();   // pause depending on forloop result
        else audio.UnPause();

        Debug.Log(blackListed);
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene) { // called upon scene switch
        string s = newScene.name;
        CheckIfOnBlackListedScene(s);
        Debug.Log($"Scene changed from {oldScene.name} to {newScene.name}");
    }

    void Awake() {  // Called upon init in login screen
        DestroyDublicateMusicManager();
        if (audio == null) audio = GetComponent<AudioSource>();
        if(audio.clip == null) Debug.LogError("Music Manager: NO MUSIC REFERENCE");
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged; // add OnSceneChanged to eventListener
        audio.loop = true;
        string s = SceneManager.GetActiveScene().name;
        audio.Play();
        CheckIfOnBlackListedScene(s);
    }

    void OnDestroy() { // Unsubscribe method called to prevent memory leaks
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
