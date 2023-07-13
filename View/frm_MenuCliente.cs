using prj_eStock.DAO;
using prj_eStock.Entidades;
using prj_eStock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prj_eStock.View
{
    public partial class frm_MenuCliente : Form
    {
        CadClienteModel clienteModel = new CadClienteModel();
        public frm_MenuCliente()
        {
            InitializeComponent();
        }

        private void CB_Cpf_Cnpj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Cpf_CnpjCliente.Text == "CNPJ")
            {
                lbl_CPFCliente.Text = "-- Informe o CNPJ do Cliente";
                msk_CPF_CNPJ.Mask = "00,000,000/0000-00";

            }
            else if (cb_Cpf_CnpjCliente.Text == "CPF")
            {
                lbl_CPFCliente.Text = "-- Informe o CPF do Cliente";
                msk_CPF_CNPJ.Mask = "000,000,000-00";
            }
        }

        private void btn_SalvarDadosCliente_Click(object sender, EventArgs e)
        {
            List<Tuple<TextBox, Label>> LabelsCadCliente = new List<Tuple<TextBox, Label>>(){
new Tuple<TextBox, Label>(txtb_LogradouroCliente, lbl_LogradouroCliente),
};
            bool CampVazio = false;
            foreach (Tuple<TextBox, Label> alterar in LabelsCadCliente)
            {
                if (alterar.Item1.Text == "")
                {
                    alterar.Item2.ForeColor = Color.Red;
                    CampVazio = true;
                }
                else
                {
                    alterar.Item2.ForeColor = ForeColor = Color.SteelBlue;
                }
            }
            List<Tuple<ComboBox, Label>> LabelsCadClienteCB = new List<Tuple<ComboBox, Label>>(){
new Tuple<ComboBox, Label>(cb_Cpf_CnpjCliente, lbl_InfCPFCNPJCliente),
new Tuple<ComboBox, Label>(cb_EstadoCliente, lbl_EstadoCliente),
new Tuple<ComboBox, Label>(cb_CidadeCliente, lbl_CidadeCliente),
};
            foreach (Tuple<ComboBox, Label> alterar2 in LabelsCadClienteCB)
            {
                if (alterar2.Item1.Text == "")
                {
                    alterar2.Item2.ForeColor = Color.Red;
                    CampVazio = true;
                }
                else
                {
                    alterar2.Item2.ForeColor = ForeColor = Color.SteelBlue;
                }
            }

            List<Tuple<TextBox, Label>> LabelsCadCliente2 = new List<Tuple<TextBox, Label>>(){
    new Tuple<TextBox, Label>(txtb_NomeCliente, lbl_InfNomeCliente),
    new Tuple<TextBox, Label>(txtb_NomeCliente, lbl_nomeCliente)
};
            foreach (Tuple<TextBox, Label> alterar2 in LabelsCadCliente2)
            {
                if (alterar2.Item1.Text == "")
                {
                    alterar2.Item2.ForeColor = Color.Red;
                    CampVazio = true;
                }
                else
                {
                    alterar2.Item2.ForeColor = ForeColor = Color.SlateGray;
                }
            }

            if (CampVazio)
            {
                MessageBox.Show("Preencha todos os campos!!");
            }
            else
            {
                string cbCidade = cb_CidadeCliente.SelectedItem.ToString();
                string cbEstado = cb_EstadoCliente.SelectedItem.ToString();
                CadClienteEntidade usuariocadastro = new CadClienteEntidade();
                usuariocadastro.N_fantasia = txtb_NomeCliente.Text;
                usuariocadastro.Cpf_cnpj = msk_CPF_CNPJ.Text;
                usuariocadastro.E_logradouro = txtb_LogradouroCliente.Text;
                usuariocadastro.E_cidade = cbCidade;
                usuariocadastro.E_estado = cbEstado;
                CadClienteDAO user = new CadClienteDAO();
                bool conectou = user.CadastrarCliente(usuariocadastro);
                if (conectou)
                {
                    MessageBox.Show("Usuário cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar usuário, Verifique se o usuário já existe no Sistema!.");
                }
            }
            CarregarDGV();
            Excluir_cb_AUTO();
        }


        private void frm_MenuCliente_Load(object sender, EventArgs e)
        {
            cb_EstadoCliente.Items.AddRange(new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" });
            cb_CidadeCliente.Items.AddRange(new string[] { "Vila Velha","São Paulo", "Cariacica", "Rio de Janeiro", "Belo Horizonte", "Porto Alegre", "Salvador",
                                                  "Fortaleza", "Brasília", "Curitiba", "Recife", "Goiânia", "Belém", "Manaus",
                                                  "João Pessoa", "Campo Grande", "Natal", "Cuiabá", "São Luís", "Maceió",
                                                  "Vitoria", "Florianópolis", "Campinas", "Belford Roxo", "São Gonçalo",
                                                  "Mauá", "Duque de Caxias", "São José dos Campos", "Niterói", "São Bernardo do Campo",
                                                  "Teresina", "Ribeirão Preto", "Uberlândia", "Contagem", "Osasco", "Juiz de Fora",
                                                  "Aracaju", "Feira de Santana", "Canoas", "Aparecida de Goiânia", "Campos dos Goytacazes",
                                                  "Ananindeua", "Jaboatão dos Guararapes", "Santos", "Rio Branco", "João Pessoa",
                                                  "Porto Velho", "Boa Vista", "Macapá", "Maceió", "Palmas", "Teresina", "Aracaju",
                                                  "João Pessoa", "Campo Grande", "Cuiabá", "Goiânia", "Belo Horizonte", "Vitória",
                                                  "Florianópolis", "São Paulo", "Rio de Janeiro", "Belém", "Fortaleza", "Brasília",
                                                  "Salvador", "Recife", "Porto Alegre", "Curitiba", "Manaus", "Natal", "João Pessoa",
                                                  "Campo Grande", "Cuiabá", "Goiânia", "Belo Horizonte", "Vitória", "Florianópolis",
                                                  "São Paulo", "Rio de Janeiro", "Belém", "Fortaleza", "Brasília", "Salvador",
                                                  "Recife", "Porto Alegre", "Curitiba", "Manaus", "Natal" });
            CarregarDGV();
        }
        public void CarregarDGV()
        {
            CadClienteModel cadClienteModel = new CadClienteModel();
            dgv_CadCliente.DataSource = cadClienteModel.Chamar_Listar();
            dgv_CadCliente.Font = new Font("Arial", 12, FontStyle.Bold);
            dgv_CadCliente.ForeColor = Color.Navy;
        }
        private void btn_Atualizar_Click(object sender, EventArgs e)
        {
            CarregarDGV();
        }
        private void HabilitarCampos()
        {
            txtb_NomeCliente.Enabled = true;
            txtb_LogradouroCliente.Enabled = true;
            cb_CidadeCliente.Enabled = true;
            cb_Cpf_CnpjCliente.Enabled = true;
            cb_EstadoCliente.Enabled = true;
            msk_CPF_CNPJ.Enabled = true;

        }

        private void Desabilitarcampos()
        {
            txtb_NomeCliente.Enabled = false;
            txtb_LogradouroCliente.Enabled = false;
            cb_CidadeCliente.Enabled = false;
            cb_Cpf_CnpjCliente.Enabled = false;
            cb_EstadoCliente.Enabled = false;
            msk_CPF_CNPJ.Enabled = false;
        }

        private void LimparCampos()
        {

            txtb_NomeCliente.Text = "";
            txtb_LogradouroCliente.Text = "";
            cb_CidadeCliente.Text = null;
            cb_Cpf_CnpjCliente.Text = null;
            cb_EstadoCliente.Text = null;
            msk_CPF_CNPJ.Mask = "000,000,000-00";
        }
        public void Excluir_cb_AUTO()
        {
            while (cb_Cpf_CnpjCliente.Items.Contains("[CNPJ]") || cb_Cpf_CnpjCliente.Items.Contains("[CPF]"))
            {
                if (cb_Cpf_CnpjCliente.Items.Contains("[CNPJ]"))
                {
                    cb_Cpf_CnpjCliente.Items.Remove("[CNPJ]");
                }

                if (cb_Cpf_CnpjCliente.Items.Contains("[CPF]"))
                {
                    cb_Cpf_CnpjCliente.Items.Remove("[CPF]");
                }
            }
        }

        public void TamanhoAlterado_CPF_CNPJ()
        {

            if (msk_CPF_CNPJ.TextLength == 18)
            {
                lbl_CPFCliente.Text = "-- Informe o CNPJ do Cliente";
                cb_Cpf_CnpjCliente.Text = "[CNPJ]";
            }
            else if (msk_CPF_CNPJ.TextLength == 14)
            {
                lbl_CPFCliente.Text = "-- Informe o CPF do Cliente";
                cb_Cpf_CnpjCliente.Text = "[CPF]";
            }
        }


        private void dgv_CadCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtb_IDCliente.Text = dgv_CadCliente.CurrentRow.Cells[0].Value.ToString();
            txtb_NomeCliente.Text = dgv_CadCliente.CurrentRow.Cells[1].Value.ToString();
            msk_CPF_CNPJ.Mask = dgv_CadCliente.CurrentRow.Cells[2].Value.ToString().Replace(".", ",");
            txtb_LogradouroCliente.Text = dgv_CadCliente.CurrentRow.Cells[3].Value.ToString();
            cb_CidadeCliente.Text = dgv_CadCliente.CurrentRow.Cells[4].Value.ToString();
            cb_EstadoCliente.Text = dgv_CadCliente.CurrentRow.Cells[5].Value.ToString();
            cb_Cpf_CnpjCliente.Items.Add("[CPF]");
            cb_Cpf_CnpjCliente.Items.Add("[CNPJ]");
            TamanhoAlterado_CPF_CNPJ();
            btn_SalvarDadosCliente.Enabled = false;
            btn_AtualizarCliente.Enabled = true;
            btn_ExcluirCliente.Enabled = true;
            HabilitarCampos();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimparCampos();
            Excluir_cb_AUTO();
            btn_ExcluirCliente.Enabled = false;
            btn_AtualizarCliente.Enabled = false;
            btn_SalvarDadosCliente.Enabled = true;
            msk_CPF_CNPJ.Text = null;

        }

        public void AtualizaDados(CadClienteEntidade dados)
        {
            try
            {
                dados.Id_cliente = Convert.ToInt32(txtb_IDCliente.Text);
                dados.N_fantasia = txtb_NomeCliente.Text;
                dados.Cpf_cnpj = msk_CPF_CNPJ.Text;
                dados.E_logradouro = txtb_LogradouroCliente.Text;
                dados.E_cidade = cb_CidadeCliente.Text;
                dados.E_estado = cb_EstadoCliente.Text;

                clienteModel.Atualizar(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Atualizar os Dados do Cliente!!\n" + ex.Message);
            }
        }
        public void ExcluirDados(CadClienteEntidade dados)
        {
            try
            {
                dados.Id_cliente = Convert.ToInt32(txtb_IDCliente.Text);
                dados.N_fantasia = txtb_NomeCliente.Text;
                dados.Cpf_cnpj = msk_CPF_CNPJ.Text;
                dados.E_logradouro = txtb_LogradouroCliente.Text;
                dados.E_cidade = cb_CidadeCliente.Text;
                dados.E_estado = cb_EstadoCliente.Text;

                clienteModel.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir os Dados do Cliente!!\n" + ex.Message);
            }
        }
        public void carrega_dados()
        {
            try
            {
                dgv_CadCliente.DataSource = clienteModel.Chamar_Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na lista de dados!!" + ex);
            }
        }
        private void btn_AtualizarCliente_Click(object sender, EventArgs e)
        {
            if (txtb_IDCliente.Text == "")
            {
                MessageBox.Show("Selecione um cliente para Atualizar ou Excluir!!");
            }
            CadClienteEntidade dados = new CadClienteEntidade();

            AtualizaDados(dados);

            carrega_dados();
            Excluir_cb_AUTO();
            LimparCampos();
        }

        private void btn_ExcluirCliente_Click(object sender, EventArgs e)
        {
            if (txtb_IDCliente.Text == "")
            {
                MessageBox.Show("Selecione um cliente para Atualizar ou Excluir!!");
            }
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir os dados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                CadClienteEntidade dados = new CadClienteEntidade();

                ExcluirDados(dados);

                carrega_dados();
                Excluir_cb_AUTO();
                LimparCampos();
            }
        }
    }
}
