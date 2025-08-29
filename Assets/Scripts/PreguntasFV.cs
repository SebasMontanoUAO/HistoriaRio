using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PreguntasFV
    {
        private string pregunta;
        private bool respuesta;

        public PreguntasFV()
        {
        }

        public PreguntasFV(string pregunta, bool respuesta)
        {
            this.pregunta = pregunta;
            this.respuesta = respuesta;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public bool Respuesta { get => respuesta; set => respuesta = value; }
    }
}
