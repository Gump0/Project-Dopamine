using UnityEngine;
using UnityEngine.Events;

public class SaveDataAndTime : MonoBehaviour
{
    public UnityEvent saveData;

    public void SaveDataAndTimeMethod() {
        saveData.Invoke();
    }
}
