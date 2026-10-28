using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONE016
{
    class DS2CSV
    {

        DataTable dt = new DataTable();
        private string nazwaWPliku="Składki IWS w ramach pakietów emerytalnnych-";

        public void Sok(string path, string mies, string rok)
        {
            string dateTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddHHmm"));

            using (var reader = new StreamReader("C:\\OneSupport\\tempCSV.csv"))
            using (var csv = new CsvReader(reader))
            {
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {

                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("PESEL", typeof(string));
                    dt.Columns.Add("ID_Pracownika", typeof(string));
                    dt.Columns.Add("Miesiac", typeof(int));
                    dt.Columns.Add("Rok", typeof(int));
                    dt.Columns.Add("ID_wyplaty", typeof(int));
                    dt.Columns.Add("KwotaSkladek", typeof(string));
                    dt.Columns.Add("KwotaNaKaskade", typeof(string));
                    dt.Columns.Add("NazwaProduktu", typeof(string));
                    dt.Columns.Add("SkladkaPodstawowa", typeof(string));
                    //dt.Columns.Add("SkladkaPodstawowa", typeof(string));
                    dt.Columns.Add("SkladkaDodatkowa", typeof(string));

                    dt.Load(dr);
                }
            }
            var fdt = dt.Select("Miesiac = " + mies + " AND Rok =" + rok);

            DataTable nDT = new DataTable();
            nDT.Columns.Add("ID", typeof(int));
            nDT.Columns.Add("PESEL", typeof(string));
            nDT.Columns.Add("ID_Pracownika", typeof(string));
            nDT.Columns.Add("Miesiac", typeof(int));
            nDT.Columns.Add("Rok", typeof(int));
            nDT.Columns.Add("ID_wyplaty", typeof(int));
            nDT.Columns.Add("KwotaSkladek", typeof(string));
            nDT.Columns.Add("KwotaNaKaskade", typeof(string));
            nDT.Columns.Add("NazwaProduktu", typeof(string));
            nDT.Columns.Add("SkladkaPodstawowa", typeof(string));
            nDT.Columns.Add("SkladkaDodatkowa", typeof(string));
            for (int i = 0; i < 56; i++)
            {
                nDT.Columns.Add("column" + (i+8).ToString());
            }
            foreach (DataRow row1 in fdt)
            {
                nDT.ImportRow(row1);
            }


            nDT.Columns.Remove("ID");
            nDT.Columns.Remove("Miesiac");
            nDT.Columns.Remove("ID_wyplaty");
            nDT.Columns.Remove("Rok");
            try
            {

                string lineBuilder = "";
                //zapełniam date zmieniam ja na rzecz gotową do pliku tekstowego
                //DataSet customerDataSet = new DataSet();
                //customerDataSet.Tables.Add(new DataTable("Customers"));
                //var DataTable = customerDataSet.Tables["Customers"];
                //for (int i = 0; i < 66; i++)
                //{
                //    DataTable.Columns.Add("col"+i.ToString());
                //}
                decimal dec=0.15m;

                //DataTable.Rows.Add(75061292372, "003 / E", 1, 2020, 11, 2331.00m, 0.00m, "PPE",0.00, 0.00);
                //DataTable.Rows.Add(75061292372, "003 / E", 12, 2019, 6, 2018.63m, 0.00m, "PPE", 0.00, 0.00);
                //DataTable.Rows.Add(null, "003 / E", 1, 2020, 17, 93.52m, 0.00m, "PPE", 0.00, 0.00);
                //DataTable.Rows.Add(null, "003 / E", 12, 2019, 7, 54.20m, 0.00m, "PPE", 0.00, 0.00);


                //var filteredDaTab = DataTable.Select("col2 = "+mies+ " AND col3 ="+rok);
                StringBuilder csvcontent = new StringBuilder();
                foreach (DataRow row in nDT.Rows)
                {
                    lineBuilder = null;
                    for (int i = 0; i < 63; i++)
                    {
                        bool result = Program.ContainsAny(row[i].ToString(), "~", "`", "#", "$", "%", "^", "&", "*", "_", "+", "=", "{", "[", "]", "}", "|", "\\", ":", ";", "\"", "<", ">", "?", "'");
                        if (result)
                        {
                            MessageBox.Show("Pobrane pole zawiera niedozwolony symbol.", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (row[i].GetType() == dec.GetType())//tu jest problem teraz
                        {
                            lineBuilder += row[i].ToString().Replace(".",",") + ";";
                        }
                        else
                        {
                            lineBuilder += row[i].ToString().Replace(".", ",") + ";";
                            //lineBuilder += row[i].ToString() + ";";
                        }
                    }
                    csvcontent.Append(lineBuilder+"\r\n");
                }
                string csvconn = csvcontent.ToString();

                if (File.Exists(path + "\\" + nazwaWPliku + dateTime + ".csv"))
                {
                    MessageBox.Show("W wybranym folderze istnieje już plik o takiej samej nazwie.", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Tworze plik
                    File.AppendAllText(path + "\\" + nazwaWPliku + dateTime + ".csv", csvconn, Encoding.GetEncoding(1252));
                    //csvcontent.Clear();
                    MessageBox.Show("Zakończono.");
                }

                //Cursor.Current = Cursors.Default;
                return ;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd generowania csv: " + exc, "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
        }

    }
}
