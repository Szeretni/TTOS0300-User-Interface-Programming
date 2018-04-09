using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace JAMK.IT
{
    public class DBAutotalli
    {
        public static DataTable GetAllAutosFromMySQLDt()
        {
            try
            {
                //haetaan autojen tiedot ja palautetaan ne DataTablena
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT merkki,malli,vm,hinta,url FROM autotalli";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch 
            {
                throw;
            }
        }
        public static List<Auto> GetAllAutosFromMySQL()
        {
            try
            {
                //metodi palauttaa listan auto-olioita joitten tiedot haettu mysql
                List<Auto> autos = new List<Auto>();
                //luodaan yhteys tietokantaan
                string connStr = GetConnectionString();
                string sql = "SELECT merkki,malli,vm,hinta FROM autotalli";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Auto auto = new Auto();
                            auto.Merkki = rdr.GetString(0);
                            auto.Malli = rdr.GetString(1);
                            auto.VM = rdr.GetInt16(2);
                            auto.Hinta = rdr.GetFloat(3);
                            autos.Add(auto);
                        }
                    }
                }
                return autos;
            }
            catch
            {
                throw;
            }
        }

        private static string GetConnectionString()
        {
            //HUONO TAPA = kovakoodattu
            /*
            string pw = "q4ARIboJAkdZeErWozcP13NbCCtojxx6";
            return string.Format("Data source=mysql.labranet.jamk.fi;Initial catalog=L2912_1;user=L2912;password={0}",pw);
            //string palvelin = "mysql.labranet.jamk.fi";
            //string tietokanta = "L2912_1";
            //string tunnus = "L2912";
            //string pw = "q4ARIboJAkdZeErWozcP13NbCCtojxx6";
            */
            //PAREMPI TAPA = luetaan app.configista
            string palvelin = Autotalli.Properties.Settings.Default.palvelin;
            string tietokanta = Autotalli.Properties.Settings.Default.tietokanta;
            string tunnus = Autotalli.Properties.Settings.Default.tunnut;
            string pw = Autotalli.Properties.Settings.Default.salasana;
            return string.Format("Data source={0};Initial catalog={1};user={2};password={3}", palvelin, tietokanta, tunnus, pw);
        }
    }
}