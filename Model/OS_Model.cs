using prj_eStock.DAO;
using prj_eStock.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prj_eStock.DAO;

namespace prj_eStock.Model
{
    public class OS_Model
    {
        OS_DAO OS_DAO = new OS_DAO();
        public void Salvar(OS_Entidade dados)
        {
            try
            {
                OS_DAO.CadLanc_OS(dados);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCORREU UM ERRO AO TENTAR LANÇAR A ORDEM DE SERVIÇO!!", "ERRO AO SALVAR" + ex.Message);
            }
        }
        public void Salvar(OS_Entidade_Itens_OS dados)
        {
            try
            {
                OS_DAO.CadItens_OS(dados);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCORREU UM ERRO AO TENTAR CADASTRAR ITENS_OS!!", "ERRO AO SALVAR" + ex.Message);
            }
        }
    }
}
