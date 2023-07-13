using MySql.Data.MySqlClient;
using prj_eStock.Entidades;
using prj_eStock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prj_eStock.View
{
    public partial class frm_Estoque : Form
    {
        CadEstoqueEntidade dados = new CadEstoqueEntidade();
        CadEstoqueModel EstoqueModel = new CadEstoqueModel();
        public frm_Estoque()
        {
            InitializeComponent();
        }
        public void HabilitarCampos()
        {
            txtb_NomeProd.Enabled = true;
            rtb_DescProd.Enabled = true;
            txtb_QTDProd.Enabled = true;
            cb_CategoriaProd.Enabled = true;
            txtb_QTDMin.Enabled = true;
            txtb_ValorCusto.Enabled = true;
            txtb_InsCategoria.Enabled = true;
        }
        private void dgv_ProdutoEstoque_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtb_IDProd.Text = dgv_ProdutoEstoque.CurrentRow.Cells[0].Value.ToString();
            txtb_NomeProd.Text = dgv_ProdutoEstoque.CurrentRow.Cells[1].Value.ToString();
            rtb_DescProd.Text = dgv_ProdutoEstoque.CurrentRow.Cells[2].Value.ToString();
            txtb_QTDProd.Text = dgv_ProdutoEstoque.CurrentRow.Cells[3].Value.ToString();
            cb_CategoriaProd.Text = dgv_ProdutoEstoque.CurrentRow.Cells[4].Value.ToString();
            txtb_QTDMin.Text = dgv_ProdutoEstoque.CurrentRow.Cells[5].Value.ToString();
            txtb_ValorCusto.Text = dgv_ProdutoEstoque.CurrentRow.Cells[6].Value.ToString().Replace("R$", "");

            btn_Salvar.Enabled = false;
            btn_Atualizar.Enabled = true;
            btn_Excluir.Enabled = true;
            HabilitarCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btn_Salvar.Enabled = true;
            btn_Atualizar.Enabled = false;
            btn_Excluir.Enabled = false;
            LimparCampos();
            HabilitarCampos();
        }

        private void frm_Estoque_Load(object sender, EventArgs e)
        {
            cb_CategoriaProd.Items.AddRange(new string[] { "Suzuki Lavanderia", "Elevacar Elevadores" });
            dgv_ProdutoEstoque.CellFormatting += new DataGridViewCellFormattingEventHandler(gridView_CellFormatting);
            CarregarDGV();



        }
        public void EnviarDados(CadEstoqueEntidade dados)
        {
            CadEstoqueModel estoqueModel = new CadEstoqueModel();

            dados.Nome_prod = txtb_NomeProd.Text;
            dados.Desc_prod = rtb_DescProd.Text;
            dados.Qtd_estoque = Convert.ToInt32(txtb_QTDProd.Text);
            dados.Categoria_id = cb_CategoriaProd.Text;
            dados.Qtd_min = Convert.ToInt32(txtb_QTDMin.Text);
            dados.Valor_custo = Convert.ToDouble(txtb_ValorCusto.Text);

            estoqueModel.Salvar(dados);
        }
        public void CarregarDGV()
        {
            CadEstoqueModel estoqueModel = new CadEstoqueModel();
            dgv_ProdutoEstoque.DataSource = estoqueModel.Chamar_Listar();
            dgv_ProdutoEstoque.Font = new Font("Arial", 12, FontStyle.Bold);
            dgv_ProdutoEstoque.ForeColor = Color.Navy;
        }

        private void btn_SalvarDadosCliente_Click(object sender, EventArgs e)
        {
            if (txtb_NomeProd.Text == "")
            {
                MessageBox.Show("PREENCHA TODOS OS CAMPOS!!");
                txtb_NomeProd.Focus();
                return;
            }



            //O this foi usado aqui para definir que o objeto dados que ja foi instanciado no começo do form será utilizado com os dados que estao neste ponto;
            this.EnviarDados(dados);

            CarregarDGV();
        }
        private void gridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv_ProdutoEstoque.Columns[e.ColumnIndex].Name == "Indicador de Estoque")
            {
                string indicador = e.Value as string;
                if (indicador == "↓ Abaixo Estoque")
                {
                    e.CellStyle.BackColor = Color.DarkRed;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (indicador == "= Na Média")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.Navy;
                }
                else if (indicador == "↑ Estoque OK")
                {
                    e.CellStyle.BackColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.ForeColor = Color.White;
                }
            }
        }



        private void btn_InsCategoria_Click(object sender, EventArgs e)
        {
            try
            {

                cb_CategoriaProd.Items.Add(txtb_InsCategoria.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um ERRO ao tentar inserir uma nova categoria!!" + ex.Message);
            }
        }
        public void LimparCampos()
        {
            txtb_IDProd.Text = null;
            txtb_InsCategoria.Text = null;
            txtb_NomeProd.Text = null;
            txtb_QTDMin.Text = null;
            txtb_QTDProd.Text = null;
            txtb_ValorCusto.Text = null;
            cb_CategoriaProd.Text = null;
        }
        public void AtualizaDados(CadEstoqueEntidade dados)
        {
            try
            {
                dados.Id_estoque = Convert.ToInt32(txtb_IDProd.Text);
                dados.Nome_prod = txtb_NomeProd.Text;
                dados.Desc_prod = rtb_DescProd.Text;
                dados.Qtd_estoque = Convert.ToDouble(txtb_QTDProd.Text);
                dados.Categoria_id = cb_CategoriaProd.Text;
                dados.Qtd_min = Convert.ToInt32(txtb_QTDMin.Text);
                dados.Valor_custo = Convert.ToDouble(txtb_ValorCusto.Text);

                EstoqueModel.Atualizar(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Atualizar os Dados do Cliente!!\n" + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (txtb_IDProd.Text == "")
            {
                MessageBox.Show("Selecione um Produto para Atualizar ou Excluir!!");
            }
            CadEstoqueEntidade dados = new CadEstoqueEntidade();

            AtualizaDados(dados);

            CarregarDGV();
            LimparCampos();
        }
        public void ExcluirDados(CadEstoqueEntidade dados)
        {
            try
            {
                dados.Id_estoque = Convert.ToInt32(txtb_IDProd.Text);
                dados.Nome_prod = txtb_NomeProd.Text;
                dados.Desc_prod = rtb_DescProd.Text;
                dados.Qtd_estoque = Convert.ToDouble(txtb_QTDProd.Text);
                dados.Categoria_id = cb_CategoriaProd.Text;
                dados.Qtd_min = Convert.ToInt32(txtb_QTDMin.Text);
                dados.Valor_custo = Convert.ToDouble(txtb_ValorCusto.Text);

                EstoqueModel.Excluir(dados);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir os Dados do Cliente!!\n" + ex.Message);
            }
        }
        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            if (txtb_IDProd.Text == "")
            {
                MessageBox.Show("Selecione um cliente para Atualizar ou Excluir!!");
            }
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir os dados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                CadEstoqueEntidade dados = new CadEstoqueEntidade();

                ExcluirDados(dados);
                CarregarDGV();
                LimparCampos();
            }
        }

    }
}
