using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class EnviosRepository : IEnvioRepository
    {
        private EnviosDBContext _context;

        public EnviosRepository(EnviosDBContext context)
        {
            _context = context;
        }

        public List<Envio> GetEnvios(string dni, string direccion)
        {
            return _context.TEnvios
                    .Where(x => x.DniCliente.Equals(dni) && x.Direccion.Contains(direccion)) //x.DireccionContains(domicilio))
                    .ToList();
        }

        public bool Create(Envio e)
        {
            _context.TEnvios.Add(e);
            return _context.SaveChanges() == 1;
        }
    }
}
