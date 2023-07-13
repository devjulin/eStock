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
    public class CadEstoqueModel
    {
        CadEstoqueDAO estoqueDAO = new CadEstoqueDAO();
        public DataTable Chamar_Listar()
        {
            try
            {
                DataTable dt_Listar = new DataTable();
                dt_Listar = estoqueDAO.Listar();
                return dt_Listar;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Salvar(CadEstoqueEntidade dados)
        {
            try
            {
                estoqueDAO.Salvar(dados);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCORREU UM ERRO AO TENTAR SALVAR NO ESTOQUE!!" + ex.Message);
            }
        }
        public void Atualizar(CadEstoqueEntidade dados)
        {
            try
            {
                estoqueDAO.Atualizar(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao atualizar os dados do Estoque!!\n" + ex.Message);
            }
        }
        public void Excluir(CadEstoqueEntidade dados)
        {
            try
            {
                estoqueDAO.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao atualizar os dados do Estoque!!\n" + ex.Message);
            }
        }
    }
}
