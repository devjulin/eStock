using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_eStock.Entidades
{
    public class CadEstoqueEntidade
    {
        int id_estoque;
        double qtd_estoque, qtd_min, valor_custo;
        string nome_prod, desc_prod, categoria_id;

        public int Id_estoque { get => id_estoque; set => id_estoque = value; }
        public string Nome_prod { get => nome_prod; set => nome_prod = value; }
        public string Desc_prod { get => desc_prod; set => desc_prod = value; }
        public double Qtd_estoque { get => qtd_estoque; set => qtd_estoque = value; }
        public double Qtd_min { get => qtd_min; set => qtd_min = value; }
        public double Valor_custo { get => valor_custo; set => valor_custo = value; }
        public string Categoria_id { get => categoria_id; set => categoria_id = value; }
    }
}
