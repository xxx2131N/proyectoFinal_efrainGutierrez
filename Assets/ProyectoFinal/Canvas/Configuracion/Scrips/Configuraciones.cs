using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Configuraciones : MonoBehaviour
{
   private int sonido;
   private int musica;
   public int numeroMenu;
   public int numeroNivel;

   public void cambiarSonido(int valor)
   {
       sonido = valor;
       PlayerPrefs.SetInt("sonido", sonido);
   }
    public void cambiarMusica(int valor)
    {
         musica = valor;
         PlayerPrefs.SetInt("musica", musica);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene(numeroMenu);
    }
    public void OpcionNivel()
    {
        SceneManager.LoadScene(numeroNivel);
    }
}
