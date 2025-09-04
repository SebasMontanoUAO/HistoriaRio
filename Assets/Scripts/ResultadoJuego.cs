using System.Text;
using UnityEngine;

public static class ResultadoJuego
{
    private static int respuestasCorrectas = 0;
    private static int totalRespuestas = 0;

    // Guardar un registro detallado de cada respuesta
    private static StringBuilder historialRespuestas = new StringBuilder();

    public static void RegistrarRespuesta(bool esCorrecta, string pregunta = "")
    {
        totalRespuestas++;
        if (esCorrecta) respuestasCorrectas++;

        string estado = esCorrecta ? "Correcta" : "Incorrecta";
        if (!string.IsNullOrEmpty(pregunta))
            historialRespuestas.AppendLine($"Pregunta: {pregunta} - Resultado: {estado}");
    }
    public static float ObtenerPorcentajeAciertos()
    {
        // Retornar valor entre 0 y 1 según aciertos / total
        return (float)respuestasCorrectas / totalRespuestas;
    }


    public static string ObtenerResumen()
    {
        if (totalRespuestas == 0)
            return "No se respondieron preguntas.";

        int incorrectas = totalRespuestas - respuestasCorrectas;
        float porcentajeCorrectas = (float)respuestasCorrectas / totalRespuestas * 100f;

        string estadoRio = "";
        if (porcentajeCorrectas >= 80f)
            estadoRio = "El río mejoró: el agua está más limpia y la biodiversidad regresa.";
        else if (porcentajeCorrectas <= 30f)
            estadoRio = "El río empeoró: el agua se ve más contaminada y los problemas sanitarios son más graves";
        else
            estadoRio = "El río sigue igual: no hubo grandes cambios. No cambiaste el destino del río";

        string resumen = $" RESULTADO FINAL\n\n" +
                         $"Respuestas correctas: {respuestasCorrectas}\n" +
                         $"Respuestas incorrectas: {incorrectas}\n" +
                         $"Porcentaje de aciertos: {porcentajeCorrectas:F2}%\n" +
                         $"{estadoRio}\n\n" ;

        return resumen;
    }

    public static void Reiniciar()
    {
        respuestasCorrectas = 0;
        totalRespuestas = 0;
        historialRespuestas.Clear();
    }
}

