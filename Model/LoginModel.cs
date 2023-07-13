using MySql.Data.MySqlClient;
using prj_eStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prj_eStock.Entidades;
using prj_eStock.DAO;
using System.Security.Cryptography;

namespace prj_eStock.Model
{
    public class VerifCamposLog
    {
        LoginDAO com_LoginDAO = new LoginDAO();
        public bool confirma_login(Login DadosUs)
        {
            try
            {
                bool lRet = com_LoginDAO.Dados_Login(DadosUs);
                return lRet;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro no login e senha" + ex);
                return false;
            }

        }
    }
}
