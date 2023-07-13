using MySql.Data.MySqlClient;
using prj_eStock.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace prj_eStock.DAO
{
    public class OS_DAO
    {
        MySqlCommand comandos_sql = new MySqlCommand();
        Conexao conectar = new Conexao();
        public void CadLanc_OS(OS_Entidade lanc_os)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("INSERT INTO lanc_os (dt_os, id_cliente, maquina_equip, defeito, servico_exe, mao_obra) VALUES (@dt_os, @id_cliente, @maquina_equip, @defeito, @servico_exe, @mao_obra)", conectar.conexao);
                // Adicionando os valores de entrada para o comando SQL
                comandos_sql.Parameters.AddWithValue("@dt_os", lanc_os.Dt_os);
                comandos_sql.Parameters.AddWithValue("@id_cliente", lanc_os.Id_cliente);
                comandos_sql.Parameters.AddWithValue("@maquina_equip", lanc_os.Maquina_equip);
                comandos_sql.Parameters.AddWithValue("@defeito", lanc_os.Defeito);
                comandos_sql.Parameters.AddWithValue("@servico_exe", lanc_os.Servico_exe);
                comandos_sql.Parameters.AddWithValue("@mao_obra", lanc_os.Mao_obra);


                comandos_sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.FecharConexao();
            }
        }
        public void CadItens_OS(OS_Entidade_Itens_OS dados)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("INSERT INTO itens_os (id_os, produto_vendido, quantidade, valor_custo, porcentagem, valor_venda) VALUES (@id_os, @produto_vendido, @quantidade, @valor_custo, @porcentagem, @valor_venda)", conectar.conexao);
                // Adicionando os valores de entrada para o comando SQL
                comandos_sql.Parameters.AddWithValue("@id_os", dados.Id_os);
                comandos_sql.Parameters.AddWithValue("@produto_vendido", dados.Produto_vendido);
                comandos_sql.Parameters.AddWithValue("@quantidade", dados.Quantidade);
                comandos_sql.Parameters.AddWithValue("@valor_custo", dados.Valor_custo);
                comandos_sql.Parameters.AddWithValue("@porcentagem", dados.Porcentagem);
                comandos_sql.Parameters.AddWithValue("@valor_venda", dados.Valor_venda);


                comandos_sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conectar.FecharConexao();
            }
        }

    }
}
