using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CargarNivel
{
    public static string siguienteNivel;

    public static void cargarNivel(string nivel)
    {
        siguienteNivel = nivel;
        SceneManager.LoadScene("CargaNivel");
    }
}
