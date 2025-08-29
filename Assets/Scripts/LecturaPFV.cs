using Models;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LecturaPFV : MonoBehaviour
{
    List<PreguntasFV> listaPreguntasFV;
    public TextMeshProUGUI textoPregunta;
    public GameObject panelTerminarFV;
    public int indicadorPreguntaFV;
    bool respuestaCorrecta;

    void Start()
    {
        listaPreguntasFV = new List<PreguntasFV>();
        lecturaPreguntasFV();

        FindObjectOfType<ControllerAllS>().AvisarLecturaLista();
    }

    public void lecturaPreguntasFV()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "Scenes/RecursosTXT/FALSO_VERDADERO_2024.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                string lineaLeida;
                while ((lineaLeida = sr.ReadLine()) != null)
                {
                    string[] lineaPartida = lineaLeida.Split('-');

                    if (lineaPartida.Length >= 2)
                    {
                        string pregunta = lineaPartida[0];
                        bool respuesta = bool.Parse(lineaPartida[1]);

                        PreguntasFV preguntaFV = new PreguntasFV(pregunta, respuesta);
                        listaPreguntasFV.Add(preguntaFV);
                    }
                    else
                    {
                        Debug.LogWarning("Línea malformada: " + lineaLeida);
                    }
                }

                Debug.Log("Preguntas FV cargadas: " + listaPreguntasFV.Count);
                indicadorPreguntaFV = listaPreguntasFV.Count;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
    }

    public void asignarPregunta()
    {
        if (listaPreguntasFV.Count > 0)
        {
            System.Random random = new System.Random();
            int numeroPregunta = random.Next(0, listaPreguntasFV.Count);
            PreguntasFV pregunta = listaPreguntasFV[numeroPregunta];

            textoPregunta.text = pregunta.Pregunta;
            respuestaCorrecta = pregunta.Respuesta;

            listaPreguntasFV.RemoveAt(numeroPregunta);
            indicadorPreguntaFV -= 1;
        }
        else
        {
            panelTerminarFV.SetActive(true);

            ResultadoJuego.MostrarResultadoFinal();
        }
    }

    public void validarRespuesta(bool respuestaUsuario)
    {
        bool esCorrecta = (respuestaUsuario == respuestaCorrecta);

        ResultadoJuego.RegistrarRespuesta(esCorrecta);

        Debug.Log("Respuesta del jugador: " + (esCorrecta ? "Correcta" : "Incorrecta"));
        FindObjectOfType<ControllerAllS>().SelectQuestionFaciles();
    }
}
