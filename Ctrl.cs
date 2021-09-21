using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_Write_Read_Files
{
    class Ctrl : Conexion
    {
        public void CrearTXT()
        {
            MySqlDataReader reader;
            StreamWriter writer = new StreamWriter(@"D:\articulos.txt");
            int contador = 0;
            while (contador < 14850)
            {
                StringBuilder buffer = new StringBuilder();
                string sql = "SELECT * FROM articulo LIMIT " + contador + ",50";
                MySqlConnection conexionBD = base.conexion();
                conexionBD.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                if (contador == 0)
                {
                    buffer.Append("ID\tFECHA DE ALTA\tCODIGO\tDENOMINACION\tPRECIO\tPUBLICADO\n");
                }
                try
                {
                    reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            buffer.Append(reader.GetString(0) + "\t" + reader.GetString(1) + "\t" + reader.GetString(2) + "\t" + reader.GetString(3) + "\t" + reader.GetString(4) + "\t" + reader.GetString(5) + "\n");
                        }
                        writer.Write(buffer.ToString());
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                conexionBD.Close();
                contador += 50;
            }
            writer.Close();
            MessageBox.Show("TXT creado con exito en la ruta D:/temp");
        }
        
        
        public void LeerTXT()
        {
           
            int contador = 0;
            try
            {
                StreamReader reader = new StreamReader(@"D:\articulos.txt");
                string lineaEncabezado = reader.ReadLine();
                string linea = null;
                while (contador < 14850)
                {
                    MySqlConnection myConnection = base.conexion();
                    myConnection.Open();
                    MySqlCommand myCommand = myConnection.CreateCommand();
                    MySqlTransaction myTrans;
                    myTrans = myConnection.BeginTransaction();
                    myCommand.Connection = myConnection;
                    myCommand.Transaction = myTrans;
                    try
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            linea = reader.ReadLine();
                            string[] columna = linea.Split('\t');
                            DateTime fecha = DateTime.Parse(columna[1]);
                            string fechasalida = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                            columna[4] = columna[4].Replace(',', '.');
                            myCommand.CommandText = "INSERT INTO articulo_copy(fechaAlta,codigo,denominacion,precio,publicado) VALUES ('" + fechasalida + "','" + columna[2] + "','" + columna[3] + "','" + columna[4] + "','" + columna[5] + "')";
                            myCommand.ExecuteNonQuery();


                        }
                        myTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            myTrans.Rollback();
                        }
                        catch (MySqlException ex)
                        {
                            if (myTrans.Connection != null)
                            {
                                Console.WriteLine("Exception de tipo " + ex.GetType() + " al ejecutar el roll back de la transaccion.");
                            }
                        }
                        Console.WriteLine("Exception de tipo: " + e.GetType() + "mientras se insertaban los datos");
                    }
                    finally
                    {
                        myConnection.Close();
                    }
                    
                    contador += 50;
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {
                
                MessageBox.Show("Tabla articulo_copy creada correctamente");
            }
        }
    }
}
