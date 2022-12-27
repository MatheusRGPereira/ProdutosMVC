using System.Reflection;
using EstatisticasMVC.Repositorys;
using MySql.Data.MySqlClient;

namespace EstatisticasMVC.DataBase
{
    public class MySqlDataBase<T> :IRepository<T>
    {

        public static readonly string connectionString = "server=localhost;user=root;database=db_Produtos;port=3306;password=root";


        private string NomeDaTabela()
        {
            var nome = typeof(T).Name.ToLower() + "s";

            TabelaAttribute? tabelaAttr = (TabelaAttribute?)typeof(T).GetCustomAttribute(typeof(TabelaAttribute));
            if (tabelaAttr != null)
                nome = tabelaAttr.Nome;

            return nome;
        }
        private string NomeDaPropriedade(PropertyInfo prop)
        {
            var nome = prop.Name.ToLower();
            ColunaAttribute? colunaAttr = (ColunaAttribute?)typeof(T).GetCustomAttribute(typeof(ColunaAttribute));
            if (colunaAttr != null)
                nome = colunaAttr.Nome;
            return nome;
        }


        public void Salvar(T obj)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var colunasArray = new List<string>();
                var valoresArray = new List<string>();
                var updateArray = new List<string>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    var nome = this.NomeDaPropriedade(prop);
                    if (nome == "Id") continue;
                    colunasArray.Add(nome);
                    valoresArray.Add($"'{prop.GetValue(obj)}'");
                    updateArray.Add($"${nome}='{prop.GetValue(obj)}'");
                }
                var colunas = string.Join(", ", colunasArray.ToArray());
                var valores = string.Join(", ", valoresArray.ToArray());
                var update = string.Join(", ", updateArray.ToArray());

                var query = $"insert into {this.NomeDaTabela()} ({colunas})values({valores});";
                var id = Convert.ToInt32(typeof(T).GetProperty("Id")?.GetValue(obj));
                if (id > 0)
                    query = $"update {this.NomeDaTabela()} set {update} where id = {id};";

                var command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public  List<T> Todos()
        {
            return new List<T>();
        }

        public void ApagarPorId(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var query = $"delete from {this.NomeDaTabela()} where id = {id};";

                var command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();

                conn.Close();
            }
        }
        public T? BuscarPorId(int id)
        {
            var obj = Activator.CreateInstance<T>();
            return obj;
        }
    }
}
