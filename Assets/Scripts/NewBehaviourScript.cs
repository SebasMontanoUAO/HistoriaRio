using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI min;
    public TextMeshProUGUI seg;
    public TextMeshProUGUI milSeg;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning=false;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TimerStart() {
        if (!isRunning) {
            Debug.Log("START");
            isRunning = true;
            startTime = Time.time;//guarda el tiempo en la variable startTime 
        }
    }

    public void TimerStop() {
        print("STOP");
        isRunning = false;
        stopTime = timerTime;
        Debug.Log("Mostrar en donde quedé "+stopTime.ToString());
        //Cuando pasan los 30 segundos, mayores a 30, suena un audio
        //if (stopTime >= 30)
        //{
        //    respuestaAudio.clip = stop;
        //    respuestaAudio.Play();
        //}

       
    }

    public void TimerReset() {
        print("RESET");
        stopTime = 0;
        isRunning = false;
        min.text = seg.text = milSeg.text = "00";
    }

    // Update is called once per frame
    void Update()
    {
        timerTime = stopTime + (Time.time - startTime);//Iniciar tiempo
        int minutesInt = (int)timerTime / 60;//Calculando los minutos
        int secondsInt = (int)timerTime % 60;//Calculando los segundos
        int milSecondsInt = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt*60))*100));//Calculando los milisegundos

        if (isRunning) {
            min.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            seg.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            milSeg.text = (milSecondsInt < 10) ? "0" + milSecondsInt : milSecondsInt.ToString();
        }
    }

    private void FixedUpdate()
    {
        
    }
}

