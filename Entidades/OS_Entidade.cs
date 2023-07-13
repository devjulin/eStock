using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_eStock.Entidades
{
    public class OS_Entidade
    {
        int id_os, id_cliente;
        double mao_obra;
        string servico_exe, maquina_equip, defeito;
        DateTime dt_os;

        public int Id_os { get => id_os; set => id_os = value; }
        public int Id_cliente { get => id_cliente; set => id_cliente = value; }
        public double Mao_obra { get => mao_obra; set => mao_obra = value; }
        public string Servico_exe { get => servico_exe; set => servico_exe = value; }
        public string Maquina_equip { get => maquina_equip; set => maquina_equip = value; }
        public string Defeito { get => defeito; set => defeito = value; }
        public DateTime Dt_os { get => dt_os; set => dt_os = value; }
    }
    public class OS_Entidade_Itens_OS
    {
        string produto_vendido;
        int  id_itens, id_os, quantidade;
        double porcentagem, valor_custo, valor_venda;

        public string Produto_vendido { get => produto_vendido; set => produto_vendido = value; }
        public int Id_itens { get => id_itens; set => id_itens = value; }
        public int Id_os { get => id_os; set => id_os = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public double Porcentagem { get => porcentagem; set => porcentagem = value; }
        public double Valor_custo { get => valor_custo; set => valor_custo = value; }
        public double Valor_venda { get => valor_venda; set => valor_venda = value; }
    }
}
