using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // Almacena el Rigidbody provisional
    Rigidbody rb;

    // Valores de las variables de fuerza
    [Header("Jump Force Value")]
    public float jumpForce;
    public float fallForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //AdjustGravity();
    }

    // Método que permite darle fuerza al jugador con el valor de la variable
    public void OnJump()
    {
        if(Mathf.Abs(rb.linearVelocity.y) < 0.1)
            rb.AddForce(Vector2.up * jumpForce);
    }

    /*
    public void AdjustGravity()
    {
        if (Mathf.Abs(rb.linearVelocity.y) > 0)
            rb.linearVelocity += new Vector3(0, Physics.gravity.y, 0) * (jumpForce - 1) * Time.fixedDeltaTime;

    }*/
}
