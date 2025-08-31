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

        int incorrectas = totalRespuestas - respuestasCorrectas;
        float porcentajeCorrectas = (float)respuestasCorrectas / totalRespuestas * 100f;

        // Reporte detallado
        Debug.Log($"Reporte final del juego:");
        Debug.Log($"Respuestas correctas: {respuestasCorrectas}");
        Debug.Log($"Respuestas incorrectas: {incorrectas}");
        Debug.Log($"Porcentaje de aciertos: {porcentajeCorrectas:F2}%");

        if (porcentajeCorrectas >= 80f)
        {
            Debug.Log("El río mejoró: el agua está más limpia y la biodiversidad regresa.");
        }
        else if (porcentajeCorrectas <= 30f)
        {
            Debug.Log("El río empeoró: el agua se ve más contaminada y los problemas sanitarios son más graves");
        }
        else
        {
            Debug.Log("El río sigue igual: no hubo grandes cambios. No cambiaste el destino del rio");
        }

        respuestasCorrectas = 0;
        totalRespuestas = 0;
    }
}

