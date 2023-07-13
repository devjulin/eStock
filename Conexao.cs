using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using prj_eStock;

namespace prj_eStock
{
    public class Conexao
    {
        String conexao_bd = "dataSource=localhost; username=root; password=; database=estock; sslmode=none";
 
public MySqlConnection conexao = null;


        public void AbrirConexao()
        {
            try
            {
                conexao = new MySqlConnection(conexao_bd);
                conexao.Open();

            }
            catch (Exception ex)
            {

                MessageBox.Show("OCORREU UM ERRO NA ABERTURA DO BANCO!! \n" + ex.Message);
            }
        }

        //Método para fechar a Conexão com o Banco
        public void FecharConexao()
        {
            try
            {
                conexao = new MySqlConnection(conexao_bd);
                //FECHANDO CONEXAO
                conexao.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um ERRO ao fechar banco de dados!!!" + ex.Message);
            }
        }
    }
}
