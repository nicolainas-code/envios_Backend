using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApi.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instancia; //INSTANCIA ESTATIC
        private string StrConnection; //atributo privado
        private SqlConnection _connection;
        public DataHelper()
        {
            _connection = new SqlConnection("Data Source=DESKTOP-N2PU4DD\\SQLEXPRESS;Initial Catalog=db_envios_final_prograII;Integrated Security=True;Trust Server Certificate=True");
        }
        public static DataHelper GetInstance() //PATRON SINGLETON DEL METODO OBTENER INSTANCIA
        {
            if (_instancia == null)
                _instancia = new DataHelper();

            return _instancia;
        }
        public DataTable ExecuteSPQuery(string sp, List<ParameterSql>? parametros)
        {
            DataTable t = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                t = null;
            }

            return t;
        }
        public int ExecuteSPDML(string sp, List<ParameterSql>? parametros)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }
        public int ExecuteSPDMLTransac(string sp, List<ParameterSql>? parametros, SqlTransaction Transaccion)
        {
            // grabar con transaccion
            return 0;
        }
        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
