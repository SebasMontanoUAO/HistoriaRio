using UnityEngine;

public static class ResultadoJuego
{
    private static int respuestasCorrectas = 0;
    private static int totalRespuestas = 0;

    public static void RegistrarRespuesta(bool esCorrecta)
    {
        totalRespuestas++;
        if (esCorrecta) respuestasCorrectas++;
    }

    public static void MostrarResultadoFinal()
    {
        if (totalRespuestas == 0)
        {
            Debug.Log("No se respondieron preguntas.");
            return;
        }

        float porcentajeCorrectas = (float)respuestasCorrectas / totalRespuestas * 100f;

        if (porcentajeCorrectas >= 80f)
        {
            Debug.Log("El río mejoró");
        }
        else if (porcentajeCorrectas <= 30f)
        {
            Debug.Log("El río empeoró");
        }
        else
        {
            Debug.Log("El río sigue igual");
        }
        respuestasCorrectas = 0;
        totalRespuestas = 0;
    }
}
