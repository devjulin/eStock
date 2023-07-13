using prj_eStock.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prj_eStock.Entidades;
using System.Windows.Forms;

namespace prj_eStock.Model
{
    public class CadClienteModel
    {
       CadClienteDAO clienteDAO = new CadClienteDAO();

        public DataTable Chamar_Listar()
        {
            try
            {
                DataTable dt_Listar = new DataTable();
                dt_Listar = clienteDAO.Listar();
                return dt_Listar;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Atualizar(CadClienteEntidade dados)
        {
            try
            {
                clienteDAO.Atualizar(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao atualizar os dados do Cliente!!\n" + ex.Message);
            }
        }
        public void Excluir(CadClienteEntidade dados)
        {
            try
            {
                clienteDAO.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao atualizar os dados do Cliente!!\n" + ex.Message);
            }
        }
    }

}
