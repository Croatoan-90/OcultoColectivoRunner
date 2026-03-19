using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;


public class GoogleLogInManager : MonoBehaviour
{
    // Almacena el string del Token obtenido al iniciar sesion
    private string m_GooglePlayGamesToken;

    private void Awake()
    {
        /// Inicializa el componente de Google Play Games
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        // Se llama al metodo respectivo
        LogInGooglePlayGames();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Realiza el LogIn al comienzo del juego
    private void LogInGooglePlayGames()
    {
        // Valida el estatus de la autenticacion
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            // Si es exitoso, se guarda el token en el string
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play Games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization Code: " + code);
                    m_GooglePlayGamesToken = code;
                });

            }
            else
            {
                // En caso de que no sea exitoso, envia el estado actual
                Debug.Log($"Google Play Games login unsuccesful, status: {status}");
            }
        });
    }

    // Inicia el sign in con Google Play Games
    public void StartSignInWithGooglePlayGames()
    {
        // Verifica si el usuario esta autenticado o no. Si no lo esta,
        // se inicia el log in
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.LogWarning("Not yet authenticated with Google Play Games -- attempting login again");
            LogInGooglePlayGames();
            return;
        }

        // En caso que ya este autenticado, se ejecuta el sign in, o el \
        // enlace con la cuenta,
        SignInOrLinkWithGooglePlayGames();
    }


    // Metodo que permite enlazar o ingresar en Google Play Games
    private async void SignInOrLinkWithGooglePlayGames()
    {
        // Verifica si el jugador no tiene el token
        if (string.IsNullOrEmpty(m_GooglePlayGamesToken))
        {
            Debug.LogWarning("Authorization code is null or empty");
            return;
        }

        // Verifica si el jugador aun no ha iniciado sesion
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await SigninWithGooglePlayGamesAsync(m_GooglePlayGamesToken);
        }
        else
        {
            await LinkWithGooglePlayGamesAsync(m_GooglePlayGamesToken);
        }
    }

    // Metodo para iniciar sesion
    private async Task SigninWithGooglePlayGamesAsync(string authCode)
    {
        try
        {   // Espera a que se ejecute el metodo que permite el signin con Google Play Games
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("SigIn is successful");
        }
        // Devuelve los errores de autenticacion o de solicitud
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    // Realiza el enlace con la cuenta de Google Play Games
    private async Task LinkWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            // Se realiza el enlace
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(authCode);
            Debug.Log("Link is successful");
        }
        // Devuelve los errores cuando el usuario ya esta enlazado con otra cuenta, errores de autenticacion  y
        // errores de fallos de solicitud
        catch (AuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            Debug.LogWarning("This user is already linked with another account. Log in instead");
        }

        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }

        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
