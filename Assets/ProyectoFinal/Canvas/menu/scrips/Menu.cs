using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuOpciones;

    public void OpenOpcionsPanel()
    {
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }
    public void OpenMenuPanel()
    {
        menuPrincipal.SetActive(true);
        menuOpciones.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("nivel 1");
    }
}
