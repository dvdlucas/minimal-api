using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entidades;

namespace minimal_api.Dominios.Interfaces
{
    public interface IAdministradorServico
    { 
        Administrador? Login(LoginDTO loginDTO);
        List<Administrador> Todos(int? pagina);
        Administrador BuscarPorId(int id);
        void Deletar(Administrador administrador);
        void Editar(Administrador administrador);   

    }
}
