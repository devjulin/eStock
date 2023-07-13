using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using prj_eStock.Properties;
using prj_eStock.DAO;
using prj_eStock.Entidades;

namespace prj_eStock.DAO
{
    public class LoginDAO
    {

        Conexao conectar_BD = new Conexao();
        MySqlCommand cmdVerificaLogin = null;
        public bool Dados_Login(Login DadosUs)
        {
            try
            {
                conectar_BD.AbrirConexao();
                //Caminho para pegar dados do banco de dados 

                cmdVerificaLogin = new MySqlCommand("SELECT id_func, login, senha FROM funcionario WHERE login = @login And senha = @senha", conectar_BD.conexao);

                cmdVerificaLogin.Parameters.AddWithValue("@login", DadosUs.UsuUsuario1);
                cmdVerificaLogin.Parameters.AddWithValue("@senha", DadosUs.UsuSenha1);

                MySqlDataReader leitor = cmdVerificaLogin.ExecuteReader(); // Criação de elemento que fará a comparação dos dados

                if (leitor.Read())
                {
                    int id = Convert.ToInt32(leitor["id_func"]);
                    conectar_BD.FecharConexao();
                    return id > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;

            }
            finally
            { 
                conectar_BD.FecharConexao();
            }

        }
    }

}
