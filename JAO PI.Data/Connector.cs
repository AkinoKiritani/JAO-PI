using System;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace JAO_PI.Data
{
    public class Connector
    {
        private SQLiteConnection Connection = new SQLiteConnection();
        public Connector(string database)
        {
            Connection.ConnectionString = "Data Source = " + database;
        }
        public async Task<int> Open(string DataBase)
        {
            await Connection.OpenAsync();
            return 1;
        }
        public bool Close()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
                return true;
            }
            return false;
        }
        public async void ExecuteQuery(string CommandText)
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                SQLiteCommand command = new SQLiteCommand(Connection);
                command.CommandText = CommandText;
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.ToString());
                }
                command.Dispose();
            }
        }
    }
}
