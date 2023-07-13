using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prj_eStock.Entidades;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace prj_eStock.DAO
{
    public class CadfuncionarioDAO
    {
        public Conexao conectar_bd = new Conexao(); //instanciando a classe conexao
        MySqlCommand cmdInsereFunc = null;
        MySqlCommand cmdVerificaFunc = null;

        public bool CadUsuario(CadFuncionarioEntidade dadosUs)
        {
            try
            {
                conectar_bd.AbrirConexao();
                // Verificando se já existe um funcionário com o mesmo login
                cmdVerificaFunc = new MySqlCommand("SELECT COUNT(*) FROM funcionario WHERE login = @login", conectar_bd.conexao);
                cmdVerificaFunc.Parameters.AddWithValue("@login", dadosUs.UsuUsuario1);
                int count = Convert.ToInt32(cmdVerificaFunc.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Já existe um funcionário com esse login!");
                    return false;
                }
                else
                {

                    // Inserindo os dados no banco estock passando os parametros para nome, login e senha.
                    cmdInsereFunc = new MySqlCommand("INSERT INTO funcionario (nome, login, senha) VALUES (@nome, @login, @senha)", conectar_bd.conexao);
                    {
                        cmdInsereFunc.Parameters.AddWithValue("@nome", dadosUs.UsuNome1);
                        cmdInsereFunc.Parameters.AddWithValue("@login", dadosUs.UsuUsuario1);
                        cmdInsereFunc.Parameters.AddWithValue("@senha", dadosUs.UsuSenha1);
                    }
                    cmdInsereFunc.ExecuteNonQuery();

                    MessageBox.Show("Funcionário cadastrado com sucesso!");
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao inserir funcionario!!" + ex.Message);
                return false;
            }
            finally
            {
                conectar_bd.FecharConexao();
            }
        }
        public DataTable Listar()
        {
            MySqlCommand comandos_sql = new MySqlCommand();
            Conexao conectar = new Conexao();
            try
            {
                conectar.AbrirConexao();
                // Instanciar uma variável com o código SQL  de Pesquisa
                comandos_sql = new MySqlCommand("SELECT id_func ID,nome Nome ,login Login,Senha Senha FROM funcionario order by id_func desc", conectar.conexao);
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
        public void DadosExib(Login dados)
        {
            Login dadosUs = new Login();
            // Inserindo os dados no banco estock passando os parametros para nome, login e senha.
            cmdInsereFunc = new MySqlCommand("SELECT id_func, login, senha FROM funcionario, ", conectar_bd.conexao);
            {
                cmdInsereFunc.Parameters.AddWithValue("@id_func", dadosUs.UsuId1);
                cmdInsereFunc.Parameters.AddWithValue("@login", dadosUs.UsuUsuario1);
                cmdInsereFunc.Parameters.AddWithValue("@senha", dadosUs.UsuSenha1);
            }
            cmdInsereFunc.ExecuteNonQuery();
        }
        public void Excluir(CadFuncionarioEntidade dados)
        {
            Conexao conectar = new Conexao();
            MySqlCommand comandos_sql = new MySqlCommand();
            try
            {
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("DELETE FROM funcionario WHERE id_func = @id_func", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@id_func", dados.UsuId1);

                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SalvarFun(CadFuncionarioEntidade dados)
        {

            MySqlCommand comandos_sql = new MySqlCommand();
            Conexao conectar = new Conexao();
            comandos_sql = null;
            try
            {
                conectar.AbrirConexao();

                comandos_sql = new MySqlCommand("INSERT INTO funcionario(nome, login, senha) VALUES(@nome, @login, @senha)", conectar.conexao);
                comandos_sql.Parameters.AddWithValue("@nome", dados.UsuNome1);
                comandos_sql.Parameters.AddWithValue("@login", dados.UsuUsuario1);
                comandos_sql.Parameters.AddWithValue("@senha", dados.UsuSenha1);

                comandos_sql.ExecuteNonQuery();

                conectar.FecharConexao();

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERRO AO SALVAR O FUNCIONARIO!!!" + ex.Message);
            }
        }
        public void Atualizar(CadFuncionarioEntidade dados)
        {

            try
            {
                conectar_bd.AbrirConexao();
                cmdInsereFunc = new MySqlCommand("UPDATE funcionario SET nome = @nome, login = @login, senha = @senha WHERE id_func = @id_func", conectar_bd.conexao);

                cmdInsereFunc.Parameters.AddWithValue("@id_func", dados.UsuId1);
                cmdInsereFunc.Parameters.AddWithValue("@nome", dados.UsuNome1);
                cmdInsereFunc.Parameters.AddWithValue("@login", dados.UsuUsuario1);
                cmdInsereFunc.Parameters.AddWithValue("@senha", dados.UsuSenha1);

                cmdInsereFunc.ExecuteNonQuery();
                conectar_bd.FecharConexao();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Atualizar!!", ex.Message);
            }
        }


    }
}


