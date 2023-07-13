using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prj_eStock.Entidades;


namespace prj_eStock.DAO
{
    public class CadEstoqueDAO
    {
        Conexao conectar = new Conexao();
        MySqlCommand comandos_sql = new MySqlCommand();

        public DataTable Listar()
        {
            try
            {
                conectar.AbrirConexao();
                // Instanciar uma variável com o código SQL  de Pesquisa
                comandos_sql = new MySqlCommand("SELECT id_estoque AS 'ID', nome_prod AS 'Nome Produto', desc_prod AS 'Descricao', qtd_estoque AS 'Quantidade', categoria AS 'Categoria', qtd_min AS 'QTD Min', valor_custo AS 'Valor Custo' FROM produto ORDER BY id_estoque DESC", conectar.conexao);

                MySqlDataAdapter pesq_dados = new MySqlDataAdapter(comandos_sql);
                pesq_dados.SelectCommand = comandos_sql;

                DataTable dados_tabela = new DataTable();

                pesq_dados.Fill(dados_tabela);

                // Adicionar coluna "Valor Formatado"
                dados_tabela.Columns.Add("Valor Formatado", typeof(string));
                foreach (DataRow row in dados_tabela.Rows)
                {
                    row["Valor Formatado"] = "R$ " + row["Valor Custo"].ToString();
                }
                dados_tabela.Columns.Remove("Valor Custo");
                dados_tabela.Columns["Valor Formatado"].ColumnName = "Valor Custo";

                // Adicionar coluna "Indicador de Estoque"
                dados_tabela.Columns.Add("Indicador de Estoque", typeof(string));
                foreach (DataRow row in dados_tabela.Rows)
                {
                    int qtdMin = int.Parse(row["QTD Min"].ToString());
                    int qtdEstoque = int.Parse(row["Quantidade"].ToString());
                    if (qtdEstoque < qtdMin)
                    {
                        row["Indicador de Estoque"] = "↓ Abaixo Estoque";

                    }
                    else if (qtdEstoque == qtdMin)
                    {
                        row["Indicador de Estoque"] = "= Na Média";
                        
                    }
                    else
                    {
                        row["Indicador de Estoque"] = "↑ Estoque OK";
                    }
                }

                    return dados_tabela;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public void Salvar(CadEstoqueEntidade dados)
        {
            try
            {
                conectar.AbrirConexao();

                // Verifica se já existe um produto com o mesmo nome_prod
                string verificacao = "SELECT COUNT(*) FROM produto WHERE nome_prod = @nome_prod";
                MySqlCommand cmdVerificacao = new MySqlCommand(verificacao, conectar.conexao);
                cmdVerificacao.Parameters.AddWithValue("@nome_prod", dados.Nome_prod);
                int resultado = Convert.ToInt32(cmdVerificacao.ExecuteScalar());

                // Se não houver nenhum produto com o mesmo nome_prod, realiza a inserção
                if (resultado == 0)
                {
                    comandos_sql = new MySqlCommand("INSERT INTO produto(nome_prod, desc_prod, qtd_estoque, categoria, qtd_min, valor_custo) VALUES(@nome_prod, @desc_prod, @qtd_estoque, @categoria, @qtd_min, @valor_custo)", conectar.conexao);
                    comandos_sql.Parameters.AddWithValue("@nome_prod", dados.Nome_prod);
                    comandos_sql.Parameters.AddWithValue("@desc_prod", dados.Desc_prod);
                    comandos_sql.Parameters.AddWithValue("@qtd_estoque", dados.Qtd_estoque);
                    comandos_sql.Parameters.AddWithValue("@categoria", dados.Categoria_id);
                    comandos_sql.Parameters.AddWithValue("@qtd_min", dados.Qtd_min);
                    comandos_sql.Parameters.AddWithValue("@valor_custo", dados.Valor_custo);

                    comandos_sql.ExecuteNonQuery();

                    MessageBox.Show("Produto salvo com sucesso.");
                }
                else
                {
                    MessageBox.Show("Já existe um produto com o nome informado.");
                }

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o produto.\n" + ex.Message);
            }
        }
        public void Atualizar(CadEstoqueEntidade dados)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("UPDATE produto SET nome_prod = @nome_prod, desc_prod = @desc_prod, qtd_estoque = @qtd_estoque, categoria = @categoria, qtd_min = @qtd_min, valor_custo = @valor_custo WHERE id_estoque = @id_estoque", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@id_estoque", dados.Id_estoque);
                comandos_sql.Parameters.AddWithValue("@nome_prod", dados.Nome_prod);
                comandos_sql.Parameters.AddWithValue("@desc_prod", dados.Desc_prod);
                comandos_sql.Parameters.AddWithValue("@qtd_estoque", dados.Qtd_estoque);
                comandos_sql.Parameters.AddWithValue("@categoria", dados.Categoria_id);
                comandos_sql.Parameters.AddWithValue("@qtd_min", dados.Qtd_min);
                comandos_sql.Parameters.AddWithValue("@valor_custo", dados.Valor_custo);


                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Excluir(CadEstoqueEntidade dados)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("DELETE FROM produto WHERE id_estoque = @id_estoque", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@id_estoque", dados.Id_estoque);

                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
