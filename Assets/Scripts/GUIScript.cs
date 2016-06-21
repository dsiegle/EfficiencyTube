using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;



public class GUIScript : MonoBehaviour
{
    private TimeCalculationScript to;


    public void SettingsButtonClicked()
    {
        Debug.Log("SettingsButtonCLicked");
        SceneManager.LoadScene(2);
    }

    public void ReturnToStartPage()
    {
        Debug.Log("OK button on settings page clicked.");
        SceneManager.LoadScene(0);
    }

    public void Toggle1Selected()
    {
        Debug.Log("GUIScript:Toggle1Selected() called.");
    }
    public void Toggle2Selected()
    {
        Debug.Log("GUIScript:Toggle2Selected() called.");
    }

}