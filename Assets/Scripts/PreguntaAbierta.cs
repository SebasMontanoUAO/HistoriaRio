using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PreguntaAbierta
    {
        private string pregunta;
        private string respuesta;

        public PreguntaAbierta()
        {
        }

        public PreguntaAbierta(string pregunta, string respuesta)
        {
            this.pregunta = pregunta;
            this.respuesta = respuesta;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
    }
}
