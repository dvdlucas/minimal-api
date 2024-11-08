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
  
        public Administrador BuscarPorId(int id)
        {
            Administrador adm = _dbContexto.Administradores.Where(a => a.Id == id).FirstOrDefault();
            return adm;
        }

        public void Deletar(Administrador administrador)
        {
            _dbContexto.Administradores.Remove(administrador);
            _dbContexto.SaveChanges();
        }

        public void Editar(Administrador administrador)
        {
            _dbContexto.Administradores.Update(administrador);
            _dbContexto.SaveChanges();
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            Administrador adm = (Administrador)_dbContexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }

        public List<Administrador> Todos(int? pagina)
        {
            var query = _dbContexto.Administradores.AsQueryable();
            var itensPorPagina = 10;
            if (pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }
            return query.ToList();
        }
    }
}
