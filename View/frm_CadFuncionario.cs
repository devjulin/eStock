using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prj_eStock.Model;
using prj_eStock.Entidades;

namespace prj_eStock.View
{
    public partial class frm_CadFuncionario : Form
    {
        public frm_CadFuncionario()
        {
            InitializeComponent();
        }

        private void dgv_CadFuncionario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarCampos();
            txtb_IDFunc.Text = dgv_CadFuncionario.CurrentRow.Cells[0].Value.ToString();
            txtb_NomeFunc.Text = dgv_CadFuncionario.CurrentRow.Cells[1].Value.ToString();
            txtb_Login.Text = dgv_CadFuncionario.CurrentRow.Cells[2].Value.ToString();
            txtb_Senha.Text = dgv_CadFuncionario.CurrentRow.Cells[3].Value.ToString();

            btn_AtualizarFunc.Enabled = true;
            btn_ExcluirFunc.Enabled = true;
            btn_SalvarDadosFunc.Enabled = false;
        }
        public void HabilitarCampos()
        {
            txtb_NomeFunc.Enabled = true;
            txtb_Senha.Enabled = true;
            txtb_Login.Enabled = true;
        


        }
        public void DesabilitarCampos()
        {
            txtb_NomeFunc.Enabled = false;
            txtb_Senha.Enabled = false;
            txtb_Login.Enabled = false;
            btn_SalvarDadosFunc.Enabled = false;
            btn_AtualizarFunc.Enabled = false;
            btn_ExcluirFunc.Enabled = false;
        }

        public void LimparCampos()
        {
            txtb_IDFunc.Text = null;
            txtb_NomeFunc.Text = null;
            txtb_Senha.Text = null;
            txtb_Login.Text = null;

        }
        public void CarregarDGV()
        {
            CadFuncionarioModel funcionarioModel = new CadFuncionarioModel();
            dgv_CadFuncionario.DataSource = funcionarioModel.Chamar_Listar();
            dgv_CadFuncionario.Font = new Font("Arial", 12, FontStyle.Bold);
            dgv_CadFuncionario.ForeColor = Color.Navy;
        }

        private void btn_ExcluirFunc_Click(object sender, EventArgs e)
        {
            if (txtb_IDFunc.Text == "")
            {
                MessageBox.Show("Selecione um Funcionario para Atualizar ou Excluir!!");
            }
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir os dados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                CadFuncionarioEntidade dados = new CadFuncionarioEntidade();

                ExcluirDados(dados);
                CarregarDGV();
                LimparCampos();
            }
        }
        public void ExcluirDados(CadFuncionarioEntidade dados)
        {
            CadFuncionarioModel FuncModel = new CadFuncionarioModel();
            try
            {
                dados.UsuId1 = Convert.ToInt32(txtb_IDFunc.Text);
                dados.UsuNome1 = txtb_NomeFunc.Text;
                dados.UsuUsuario1 = txtb_Login.Text;
                dados.UsuSenha1 = txtb_Senha.Text;

                FuncModel.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir os Dados do Funcionario!!\n" + ex.Message);
            }
        }

        private void frm_CadFuncionario_Load(object sender, EventArgs e)
        {
            CarregarDGV();
        }

        private void btn_NovoFunc_Click(object sender, EventArgs e)
        {
            btn_SalvarDadosFunc.Enabled = true;
            btn_AtualizarFunc.Enabled = false;
            btn_ExcluirFunc.Enabled = false;
            LimparCampos();
            HabilitarCampos();
        }
        public void enviaDados(CadFuncionarioEntidade dados)
        {
            CadFuncionarioModel userFuncModel = new CadFuncionarioModel(); 
            try
            {
                dados.UsuNome1 = txtb_NomeFunc.Text;
                dados.UsuUsuario1 = txtb_Login.Text;
                dados.UsuSenha1 = txtb_Senha.Text;
                

                userFuncModel.Salvar(dados);

                MessageBox.Show("Dados do Usuario Salvos com SUCESSO!!");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao salvar os Dados do Usuario \n" + ex.Message);
            }
        }
        private void btn_SalvarDadosFunc_Click(object sender, EventArgs e)
        {
            if (txtb_NomeFunc.Text == "")
            {
                MessageBox.Show("O campo Nome não pode ser VAZIO");
                txtb_NomeFunc.Focus();
                return;
            }


            CadFuncionarioEntidade dados = new CadFuncionarioEntidade();
            this.enviaDados(dados);

            CarregarDGV();

            DesabilitarCampos();
        }
        public void AtualizaDados(CadFuncionarioEntidade dados)
        {
            CadFuncionarioModel FunModel = new CadFuncionarioModel();
            try
            {
                dados.UsuId1 = Convert.ToInt32(txtb_IDFunc.Text);
                dados.UsuNome1 = txtb_NomeFunc.Text;
                dados.UsuUsuario1 = txtb_Login.Text;
                dados.UsuSenha1 = txtb_Senha.Text;

                FunModel.Atualizar(dados);

                MessageBox.Show("Dados foram alterados com sucesso!!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Atualizar os dados do Funcionario!! \n" + ex.Message);
            }

        }
        private void btn_AtualizarFunc_Click(object sender, EventArgs e)
        {
            if (txtb_IDFunc.Text == "")
            {
                MessageBox.Show("Selecione um Funcionario para Atualizar e Excluir!! ");
            }


            CadFuncionarioEntidade dados = new CadFuncionarioEntidade();
            AtualizaDados(dados);

            CarregarDGV();
        }
    }
}
