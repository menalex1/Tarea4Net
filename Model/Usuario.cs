namespace Tarea4Net.Model
{
    public class Usuario
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Usuario(string email, string pass)
        {
            Email = email;
            Password = pass;
        }

        public bool EsUsuarioValido()
        {
            // Para el demo se acepta cualquier usuario, pero acá
            // podemos exigir p.ej. que el mail contenga "@" y el pass tenga 3+ chars.
            return Email.Contains("@") && Password.Length >= 3;
        }

        public bool NecesitarVerificacion()
        {
            // Simplificado: todo usuario nuevo necesita verificarse.
            return true;
        }
    }
}