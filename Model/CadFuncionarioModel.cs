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
using System.Data;

namespace prj_eStock.Model
{
    public class CadFuncionarioModel
    {
        CadfuncionarioDAO ComunicaFun = new CadfuncionarioDAO();
        // Crie uma função para verificar se os campos estão preenchidos e se a senha tem o tamanho correto
        public bool Verificar(string nome, string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos!");
                return false;
            }
            if (senha.Length < 4)
            {
                MessageBox.Show("A senha deve ter no mínimo 4 caracteres!");
                return false;
            }
            return true;

        }
        public void Excluir(CadFuncionarioEntidade dados)
        {
            CadfuncionarioDAO funcionarioDAO = new CadfuncionarioDAO();
            try
            {
                funcionarioDAO.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao atualizar os dados do Funcionario!!\n" + ex.Message);
            }
        }
        public DataTable Chamar_Listar()
        {
            CadfuncionarioDAO funcDAO = new CadfuncionarioDAO();
            try
            {
                DataTable dt_Listar = new DataTable();
                dt_Listar = funcDAO.Listar();
                return dt_Listar;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Salvar(CadFuncionarioEntidade dados)
        {
            
            try
            {
                ComunicaFun.SalvarFun(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao Tentar Salvar o Funcionario!!" + ex.Message );
            }
        }
        public void Atualizar(CadFuncionarioEntidade dados)
        {

            try
            {

                ComunicaFun.Atualizar(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao Atualizar os Dados do Funcionario!!" + ex.Message);
            }
        }
    }
}
