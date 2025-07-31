using WebApi.Models;

namespace WebApi.Data.Repository
{
    public interface IEnvioRepository
    {
        List<Envio> GetEnvios(string dni, string direccion); // consultar envios por dni
        bool Create(Envio e); //
    }
}
