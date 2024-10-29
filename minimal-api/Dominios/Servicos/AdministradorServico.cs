using minimal_api.Dominios.DTOs;
using minimal_api.Dominios.Entidades;
using minimal_api.Dominios.Infraestrutura.Db;
using minimal_api.Dominios.Interfaces;

namespace minimal_api.Dominios.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _dbContexto;

        public AdministradorServico(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }
        public List<Administrador> Administradores()
        {
            throw new NotImplementedException();
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            Administrador adm = (Administrador)_dbContexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }
    }
}
