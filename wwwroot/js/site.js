var connection = new signalR.HubConnectionBuilder().withUrl("/login").build();


connection.on("VerificacionOk", function (usuario) {
    console.info("Me invocaron desde el servidor, usuario:" + usuario);
    window.location.href = "/PaginaBienvenida";
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

var loginForm = document.getElementById("loginForm");
var loginButton = document.getElementById("loginButton");
var verificationMessage = document.getElementById("verificationMessage");

loginForm.addEventListener("submit", function (e) {
    e.preventDefault();

    var email = document.getElementById("email").value;
    var pass = document.getElementById("password").value;

    
    connection.invoke("Login", email, pass).catch(function (err) {
        return console.error(err.toString());
    });

    loginButton.disabled = true;
    verificationMessage.style.display = "block";
});