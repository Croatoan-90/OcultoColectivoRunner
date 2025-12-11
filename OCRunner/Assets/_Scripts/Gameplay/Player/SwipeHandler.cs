
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeHandler : MonoBehaviour
{
    // Almacena el mapa de acciones
    InputActionMap touchMap;

    // Almacena las acciones del mapa
    InputAction contact;
    InputAction position;

    // Almacena la posición inicial en la que el usuario toca la pantalla
    private Vector2 startPos;

    // Almacena el momento en el cual el jugador presiona la pantalla
    private float startTime;

    // Valores para la distancia menor en la que el jugador debe mover el dedo en pantalla y el tiempo mínimo que tiene para ello
    [Header("Interaction Values")]
    public float minDistance = 80f;
    public float maxTime = 0.5f;

    [Header("Player")]
    public PlayerJump playerJump;

    [Header("Test")]
    public TestInteractions testInteractions;

    private void OnEnable()
    {
        // Al habilitarse el objeto, se almacena el mapa y las acciones.
        touchMap = InputSystem.actions.FindActionMap("Touch");
        contact = touchMap.FindAction("TouchContact");
        position = touchMap.FindAction("TouchPosition");

        // Valida si el usuario toca o retira el dedo de la pantalla
        contact.performed += OnTouchStart;
        contact.canceled += OnTouchEnd;

        // Valida si el usuario mueve el dedo por la pantalla
        position.performed += OnTouchMove;
    }

    private void OnDisable()
    {
        // El evento termina cuando el objeto se deshabilita
        contact.performed -= OnTouchStart;
        contact.canceled -= OnTouchEnd;

        position.performed -= OnTouchMove;
    }

    /// <summary>
    /// Método que se ejecuta cuando hay contacto por primera vez en pantalla.
    /// Se almacena la posición del toque y el momento en el tiempo en el que fue realizado.
    /// </summary>
    /// <param name="context"></param>
    private void OnTouchStart(InputAction.CallbackContext context)
    {
        startPos = position.ReadValue<Vector2>();
        startTime = Time.time;
    }

    private void OnTouchMove(InputAction.CallbackContext context)
    {
        // Valida swipe en vivo.
    }

    /// <summary>
    /// Método que se ejecuta cuando el usuario despega el dedo de pantalla.
    /// Se almacena la posición final en dicho momento y se obtiene la diferencia entre la posición inicial y final
    /// para detectar la dirección.
    /// Se guarda la duración de la interacción del movimiento.
    /// Finalmente se valida si la duración y la distancia entran entre los parámetros dados, para ejecutar la detección del Swipe.
    /// </summary>
    /// <param name="context"></param>
    private void OnTouchEnd(InputAction.CallbackContext context)
    {
        Vector2 endPos = position.ReadValue<Vector2>();
        Vector2 delta  = endPos - startPos;
        float duration = Time.time - startTime;

        if (duration < maxTime && delta.magnitude > minDistance)
            DetectSwipe(delta);
    }

    /// <summary>
    ///  Se normaliza la diferencia entre las dos posiciones. Luego se verifica si la diferencia o delta de x es mayor a la de Y para validar
    ///  que el jugador este moviendo el dedo lateralmente. Si la diferencia o delta es mayor a cero, se mueve a la derecha, de lo contrario
    ///  a la izquierda. Pero si delta de x no es mayor a y, quiere decir que el jugador está moviendo el dedo frontalmente, en ese caso
    ///  si es mayor a cero el delta, se mueve hacia arriba y si no hacia a bajo.
    /// </summary>
    /// <param name="delta"></param>
    private void DetectSwipe(Vector2 delta)
    {
        delta.Normalize();

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0) testInteractions.OnSwipeRight();
            else testInteractions.OnSwipeLeft();
        }
        else
        {
            if (delta.y > 0) playerJump.OnJump();
            else testInteractions.OnSwipeDown();
        }
    }

}
