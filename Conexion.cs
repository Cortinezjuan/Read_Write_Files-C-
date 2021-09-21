using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_Write_Read_Files
{
    class Conexion
    {
        public MySqlConnection conexion()
        {
            string servidor = "localhost";
            string bd = "utn";
            string usuario = "root";
            string password = "";
            string cadenaConexion = "server=" + servidor + ";user id=" + usuario + ";Password=" + password + ";database=" + bd + ";SslMode=none";
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
