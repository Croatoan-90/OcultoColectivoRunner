using System;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager instance;

    [Header("Elecciones")]
    // Guarda una lista con las 5 decisiones a realizar
    private int[] decisions = new int[5];
    // Almacena el índice actual del jugador
    private int step = 0;

    [HideInInspector] public int ending;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
        
    }
    
    // Método para los botones de las elecciones
    public void Choice(int option)
    {
        // El elemento designado del arreglo se guarda con la opción del botón
        decisions[step] = option;

        // Aumenta el índice
        step++;

        // En caso que se llegue a la quinta elección, se calcula el final y se almacena en {
        if(step == 5)
        {
            ending = CalculateEnding();
            UIResultManager.instance.UpdateResultText(ending);
            Clear();
        }
    }

    // Método que se llama al final, para obtener el resultado
    private int CalculateEnding()
    {
        // La variable se inicializa en 0 para comenzar de nuevo
        int result = 0;

        // Recorre el arreglo con las decisiones tomadas
        for (int i = 0; i < decisions.Length; i++)
        {
            result += decisions[i] * Mathf.RoundToInt(Mathf.Pow(3, decisions.Length - 1 - i));
        }
        return result;
    }

    // Limpia el ejercicio de testeo
    public void Clear()
    {
        for (int i = 0; i < decisions.Length; i++)
        {
            decisions[i] = 0;
            step = 0;
        }
    }
}
