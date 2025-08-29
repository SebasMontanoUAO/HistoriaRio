using UnityEngine;

public class EjemploInteractuable : Interactuable
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interactuar(GameObject jugador)
    {
        Debug.Log("El jugador ha interactuado con este objeto");
    }
}
