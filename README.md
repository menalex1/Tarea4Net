# Tarea4Net - Login con verificación por email usando SignalR

Proyecto ASP.NET Core (Razor Pages) que implementa un login con verificación de
email. Al verificar la cuenta haciendo click en un enlace, el usuario es
redirigido automáticamente a la página principal sin necesidad de volver a
loguearse. Eso lo logro gracias a SignalR: el servidor notifica al cliente
(página de login) cuando el email fue verificado.

## Cómo funciona

1. El usuario abre la página de login e ingresa sus credenciales.
2. La página se conecta al servidor por SignalR (hub `/login`).
3. El usuario "recibe" un email de verificación (que simulo en la consola dando la URL para la pagina de verificacion respectiva).
4. Al hacer click en el enlace, el navegador pega a `/verificar/usuario/{id}`.
5. El servidor detecta la verificación y notifica SOLO a ese cliente mediante el evento `VerificacionOk`.
6. La página de login recibe el evento y redirige automáticamente a
   `/PaginaBienvenida`, sin que el usuario haga nada.

## Requisitos

- .NET SDK (el proyecto fue creado con .NET 10)

## Cómo ejecutar

dotnet run --launch-profile http


Abrir en el navegador: http://localhost:5191

## Cómo probar el flujo completo

1. En la página de login, completar un email que contenga "@" y una password de
   3 o más caracteres y presionar **Login**.
2. En la consola del servidor aparece el connectionId asociado a esa conexión.
3. Copiar la URL que se muestra en el log y completarla con el puerto real:
   `http://localhost:5191/verificar/usuario/{connectionId}`
4. Pegar esa URL en OTRA pestaña del navegador (simula el click en el mail).
5. Volver a la pestaña del login: la página se redirige sola a la bienvenida.

## Estructura del proyecto

- `Program.cs` — configuración: `AddSignalR()`, mapeo del hub y del endpoint
  de verificación.
- `Hubs/LoginConVerificacionHub.cs` — hub que recibe el login del cliente y
  registra su connectionId.
- `Model/Usuario.cs` — modelo del usuario con validación básica.
- `Pages/Index.cshtml` + `wwwroot/js/site.js` — página de login y lógica
  SignalR del cliente.
- `Pages/PaginaBienvenida.cshtml` — página a la que se redirige al usuario.
- `wwwroot/css/site.css` — estilos.
- `libman.json` — maneja la librería `@microsoft/signalr` del lado del cliente.

## Notas

- No se envía un email real: la URL de verificación simula el click del enlace.
- El envío de la notificación se hace a un cliente específico (no a todos) con
  `Clients.Client(connectionId).SendAsync(...)`.
- En producción, NO debería exponerse el connectionId en una URL (en este
  proyecto es solo con fines demostrativos).
