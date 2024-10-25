using minimal_api.Enums;

namespace minimal_api.Dominios.Entidades
{
    public class Usuario
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}