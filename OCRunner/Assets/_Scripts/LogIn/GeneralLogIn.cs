
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class GeneralLogIn : MonoBehaviour
{

    private async void Awake()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            Debug.Log("Service Initializing");
            await UnityServices.InitializeAsync();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await CheckInitToken();
    }

    // Metodo para inicializar el sign in anonimo
    public async void StartAnonymousSignIn()
    {
        await SignInAnonymouslyAsync();
    }

    // Valida el sign aninimo del usuario
    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            // Espera a que se ejecute la autenticacion
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // mMuestra la identificacion del usuario
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

        }
        // Verifica e informa de errores de autenticacion o solicitud fallida
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    // Metodo inicial de verificacion
    public async Task CheckInitToken()
    {
        // Verifica si el jugador anonimo cuenta con un token existente
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            Debug.Log("Session Token not found.");
            return;
        }

        // Si no cuenta con token hace el sign in anonimo
        Debug.Log("Returning player signing in...");
        await SignInAnonymouslyAsync();
    }

    // Cierra sesion
    public void SignOut()
    {
        Debug.Log("Signing out...");
        AuthenticationService.Instance.SignOut();
        PlayerAccountService.Instance.SignOut();
        Debug.Log("Signed out");
    }

    // Limpia el token del usuraio
    public void ClearSessionToken()
    {
        Debug.Log("Clearing Session Token...");
        AuthenticationService.Instance.ClearSessionToken();
        Debug.Log("Session Token cleared");
    }

    // Elimina la cuenta de usuario de Unity Play
    public void DeleteAccount()
    {
        Debug.Log("Deleting Account...");
        AuthenticationService.Instance.DeleteAccountAsync();
        Debug.Log("Account deleted");
    }

}
