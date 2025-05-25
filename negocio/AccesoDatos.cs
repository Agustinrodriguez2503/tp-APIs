using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get {  return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void setearConsultaImagen(string consulta)
        {
            // Si comando se reutiliza, asegúrate de limpiarlo o crearlo nuevo.
            // Es común crear el comando aquí si no existe, o solo setear CommandText.
            // Ejemplo:
            // if (this.comando == null) this.comando = new SqlCommand();
            // this.comando.Parameters.Clear(); // Importante si reutilizas el mismo objeto SqlCommand
            this.comando.CommandType = System.Data.CommandType.Text;
            this.comando.CommandText = consulta;
        }

        public void setearParametroImagen(string nombre, object valor)
        {
            // AddWithValue es conveniente pero puede tener implicaciones de rendimiento/tipado.
            // Para mayor control, considera usar:
            // comando.Parameters.Add(new SqlParameter(nombre, tipoSql)).Value = valor ?? DBNull.Value;
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value); // Manejar nulls pasándolos como DBNull.Value
        }

        public void ejecutarAccionImagen() // Para INSERT, UPDATE, DELETE
        {
            // Asignar la conexión al comando. Esto es crucial si 'comando' y 'conexion'
            // son miembros y 'comando' podría haber sido usado con otra conexión o es nuevo.
            // Si 'comando' siempre usa la 'conexion' miembro, esto podría ser redundante
            // si se establece en el constructor de AccesoDatos o al crear 'comando'.
            comando.Connection = conexion;
            try
            {
                // Abrir la conexión SOLO SI está cerrada
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear ex.ToString() para tener todos los detalles del error
                throw; // Re-lanza la excepción original para que sea manejada por la capa de negocio/API
            }
            // NO se cierra la conexión aquí, se deja abierta para posibles operaciones adicionales
            // o para ser cerrada por el método 'cerrarConexion'.
        }

        public void ejecutarLecturaImagen() // Para SELECT que devuelve filas
        {
            // comando.Connection = conexion; // Similar a ejecutarAccion
            try
            {
                // Abrir la conexión SOLO SI está cerrada
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                // Es importante que el lector se cierre después de usarse.
                // Si 'lector' es un miembro de la clase, 'cerrarConexion' debe manejarlo.
                // Si 'ejecutarLectura' devuelve el lector, el llamador es responsable.
                // Asumiendo que 'lector' es un miembro:
                if (lector != null && !lector.IsClosed) // Cerrar lector previo si existe y está abierto
                {
                    lector.Close();
                }
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                // Loguear ex.ToString()
                throw;
            }
            // NO se cierra la conexión ni el lector aquí.
        }

        public void cerrarConexionImagen()
        {
            // Cerrar el DataReader si existe y está abierto
            if (lector != null && !lector.IsClosed)
            {
                lector.Close();
                // lector.Dispose(); // También es buena práctica hacer Dispose()
            }

            // Cerrar la conexión si existe y está abierta
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
                // conexion.Dispose(); // Considera hacer Dispose si AccesoDatos implementa IDisposable
            }
        }

    }
}
