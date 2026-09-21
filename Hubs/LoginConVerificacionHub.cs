using Microsoft.AspNetCore.SignalR;
using Tarea4Net.Model;

namespace Tarea4Net.Hubs
{
    public class LoginConVerificacionHub : Hub
    {
        private readonly ILogger<LoginConVerificacionHub> _logger;

        public LoginConVerificacionHub(ILogger<LoginConVerificacionHub> logger)
        {
            _logger = logger;
        }

        public void Login(string email, string pass)
        {
            _logger.LogInformation("SignalR identificacion del usuario: " + Context.ConnectionId);

            Usuario usr = new(email, pass);

            if (usr.EsUsuarioValido())
            {
                if (usr.NecesitarVerificacion())
                {
                    string usrId = Context.ConnectionId;
                    _logger.LogInformation($"*** Copiar esta url para simular el click del email:");
                    _logger.LogInformation($"    https://localhost:xxxx/verificar/usuario/{usrId}");
                }
            }
        }
    }
}