using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration.Attributes;
using EstatisticasMVC.DataBase;
using Microsoft.SqlServer.Server;

namespace EstatisticasMVC.Entitys
{

    [Tabela(Nome = "tb_produtos")]
    public class Produto
    {
        public int Id { get; set; }

        [Coluna(Nome = "nome")]
        public string Nome { get; set; }

        [Coluna(Nome = "descricao")]
        public string Descricao { get; set; }

        [Coluna(Nome = "datacriacao")]
        [Format("yyyy/MM/dd")]
        public DateOnly DataCriacao { get; set; }
     

        [Coluna(Nome = "datavalidade")]
        [Format("yyyy/mm/dd")]
        public DateOnly DataValidade { get; set; }


        [Coluna(Nome = "quantidade")]
        public int Quantidade { get; set; }



    }
}
