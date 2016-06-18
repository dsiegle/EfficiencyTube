using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsButtonScript : MonoBehaviour
{

    public void ButtonClicked()
    {
        Debug.Log("SettingButtonCLicked");
        SceneManager.LoadScene(2);
    }

    public void ReturnToStartPage()
    {
        Debug.Log("OK button on settings page clicked.");
        SceneManager.LoadScene(0);
    }
}