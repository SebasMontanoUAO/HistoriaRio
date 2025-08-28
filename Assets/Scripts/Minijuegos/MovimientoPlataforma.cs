using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    public Rigidbody2D rbPlataforma;

    [SerializeField]
    private float velocidad = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbPlataforma = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        float direccion = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(direccion, 0, 0) * Time.deltaTime * velocidad);
    }
}
