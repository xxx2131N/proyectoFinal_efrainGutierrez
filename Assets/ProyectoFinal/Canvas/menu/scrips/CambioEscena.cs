using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
   public int numeroJuegoPuzzle;
   public int numeroConfiguracion;

    public void CambiarEscena()
    {
        SceneManager.LoadScene(numeroJuegoPuzzle);
    }
    public void ConfMenu()
    {
        SceneManager.LoadScene(numeroConfiguracion);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("juego acabado");
    }



}
