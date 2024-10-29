using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entidades;

namespace minimal_api.Dominios.Interfaces
{
    public interface IAdministradorServico
    {
        List<Administrador> Administradores();
        Administrador? Login(LoginDTO loginDTO);
    }
}
