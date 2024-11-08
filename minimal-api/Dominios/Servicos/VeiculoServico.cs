using Microsoft.EntityFrameworkCore;
using minimal_api.Dominios.Entidades;
using minimal_api.Dominios.Infraestrutura.Db;
using minimal_api.Dominios.Interfaces;

namespace minimal_api.Dominios.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _dbContexto;

        public VeiculoServico(DbContexto dbContexto)
        {
            _dbContexto = dbContexto;
        }

        public void Apagar(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Remove(veiculo);
            _dbContexto.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _dbContexto.Update(veiculo);
            _dbContexto.SaveChanges();
        }

        public Veiculo? BuscarPorId(int id)
        {
            Veiculo veiculo = _dbContexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
            return veiculo;
        }

        public void Incluir(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Add(veiculo);
            _dbContexto.SaveChanges();
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _dbContexto.Veiculos.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
            }
            var itensPorPagina = 10;
            if (pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }
            return query.ToList();
        }



    }
}
