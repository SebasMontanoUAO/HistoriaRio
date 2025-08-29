using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PreguntasMultiples
    {
        private string pregunta;
        private string opcion1;
        private string opcion2;
        private string opcion3;
        private string opcion4;
        private string respuesta;

        public PreguntasMultiples()
        {
        }

        public PreguntasMultiples(string pregunta, string opcion1, string opcion2, string opcion3, string opcion4, string respuesta)
        {
            this.pregunta = pregunta;
            this.opcion1 = opcion1;
            this.opcion2 = opcion2;
            this.opcion3 = opcion3;
            this.opcion4 = opcion4;
            this.respuesta = respuesta;
        }

        public string Pregunta { get => pregunta; set => pregunta = value; }
        public string Opcion1 { get => opcion1; set => opcion1 = value; }
        public string Opcion2 { get => opcion2; set => opcion2 = value; }
        public string Opcion3 { get => opcion3; set => opcion3 = value; }
        public string Opcion4 { get => opcion4; set => opcion4 = value; }
        public string Respuesta { get => respuesta; set => respuesta = value; }
    }
}
