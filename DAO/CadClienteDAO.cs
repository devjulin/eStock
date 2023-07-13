using MySql.Data.MySqlClient;
using prj_eStock.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_eStock.DAO
{
    public class CadClienteDAO
    {
        MySqlCommand comandos_sql = null;
        Conexao conectar = new Conexao();

        public DataTable Listar()
        {
            try
            {
                conectar.AbrirConexao();
                // Instanciar uma variável com o código SQL  de Pesquisa
                comandos_sql = new MySqlCommand("SELECT id_cliente ID,n_fantasia \"Nome Fantasia\",cpf_cnpj \"CPF/CNPJ\",e_logradouro Logradouro,e_cidade Cidade,e_estado Estado FROM cliente order by id_cliente desc", conectar.conexao);
                MySqlDataAdapter pesq_dados = new MySqlDataAdapter(comandos_sql);
                pesq_dados.SelectCommand = comandos_sql;

                DataTable dados_tabela = new DataTable();

                pesq_dados.Fill(dados_tabela);

                return dados_tabela;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Atualizar(CadClienteEntidade dados)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("UPDATE cliente SET n_fantasia = @n_fantasia, cpf_cnpj = @cpf_cnpj, e_logradouro = @e_logradouro, e_cidade = @e_cidade, e_estado = @e_estado WHERE id_cliente = @id_cliente", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@id_cliente", dados.Id_cliente);
                comandos_sql.Parameters.AddWithValue("@n_fantasia", dados.N_fantasia);
                comandos_sql.Parameters.AddWithValue("@cpf_cnpj", dados.Cpf_cnpj);
                comandos_sql.Parameters.AddWithValue("@e_logradouro", dados.E_logradouro);
                comandos_sql.Parameters.AddWithValue("@e_cidade", dados.E_cidade);
                comandos_sql.Parameters.AddWithValue("@e_estado", dados.E_estado);


                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Excluir(CadClienteEntidade dados)
        {
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("DELETE FROM cliente WHERE id_cliente = @id_cliente", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@id_cliente", dados.Id_cliente);

                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CadastrarCliente(CadClienteEntidade cliente)
        {
            try
            {
                conectar.AbrirConexao();
                // Verifica se já existe um cliente com o mesmo nome fantasia
                comandos_sql = new MySqlCommand("SELECT COUNT(*) FROM cliente WHERE n_fantasia = @n_fantasia", conectar.conexao);
                comandos_sql.Parameters.Add("@n_fantasia", MySqlDbType.VarChar).Value = cliente.N_fantasia;
                int count = int.Parse(comandos_sql.ExecuteScalar().ToString());

                if (count > 0)
                {
                    return false;
                }
                else
                {
                    conectar.AbrirConexao();
                    // Instanciar uma variável com o código SQL  de Inserção
                    comandos_sql = new MySqlCommand("INSERT INTO cliente (n_fantasia, cpf_cnpj, e_logradouro, e_cidade, e_estado) VALUES (@n_fantasia, @cpf_cnpj, @e_logradouro, @e_cidade, @e_estado);", conectar.conexao);

                    // Adicionando os valores de entrada para o comando SQL
                    comandos_sql.Parameters.Add("@n_fantasia", MySqlDbType.VarChar).Value = cliente.N_fantasia;
                    comandos_sql.Parameters.Add("@cpf_cnpj", MySqlDbType.VarChar).Value = cliente.Cpf_cnpj;
                    comandos_sql.Parameters.Add("@e_logradouro", MySqlDbType.VarChar).Value = cliente.E_logradouro;
                    comandos_sql.Parameters.Add("@e_cidade", MySqlDbType.VarChar).Value = cliente.E_cidade;
                    comandos_sql.Parameters.Add("@e_estado", MySqlDbType.VarChar).Value = cliente.E_estado;
                    //Verifica se já existe um cliente com o mesmo nome de fantasia
                    MySqlCommand comandos_sql2 = new MySqlCommand("SELECT COUNT(*) FROM cliente WHERE n_fantasia=@n_fantasia", conectar.conexao);
                    comandos_sql2.Parameters.Add("@n_fantasia", MySqlDbType.VarChar).Value = cliente.N_fantasia;
                    int counts = Convert.ToInt32(comandos_sql2.ExecuteScalar());
                    if (count > 0)
                    {
                        // Já existe um cliente com o mesmo nome de fantasia
                        return false;
                    }
                    else
                    {
                        // Executando o comando no banco de dados
                        int affectedRows = comandos_sql.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
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
