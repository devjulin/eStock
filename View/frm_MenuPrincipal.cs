using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using prj_eStock.DAO;
using prj_eStock.Entidades;
using prj_eStock.View;
using Org.BouncyCastle.Bcpg;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Threading;
using MySqlX.XDevAPI;

namespace prj_eStock.View
{
    public partial class frm_MenuPrincipal : Form
    {
        private Form frmAtivo;
        public frm_MenuPrincipal()
        {
            InitializeComponent();
        }
        private void btn_AbMenFechar_Click(object sender, EventArgs e)
        {
            frm_TelaInicial frm_TelaInicial = new frm_TelaInicial();
            this.Close();
        }

        private void MostrarForm(Form frm)
        {
            FecharFormAtivo();
            frmAtivo = frm;
            frm.TopLevel = false;
            Painel_FormulariosGeralMenu.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();

        }
        private void FecharFormAtivo()
        {
            if (frmAtivo != null)
                frmAtivo.Close();
        }

        private void BotaoAtivo(Button frmAtivo)
        {
            foreach (Control ctrl in Painel_Principal.Controls)
                ctrl.ForeColor = Color.White;
            frmAtivo.ForeColor = Color.Red;
        }
        private void btn_AbMenClie_Click(object sender, EventArgs e)
        {
            BotaoAtivo(btn_AbMenClie);
            MostrarForm(new frm_MenuCliente());
        }

        private void btn_AbMenFunc_Click(object sender, EventArgs e)
        {
            BotaoAtivo(btn_AbMenFunc);
            MostrarForm(new frm_CadFuncionario());
        }

        private void btn_AbMenEstoque_Click(object sender, EventArgs e)
        {
            BotaoAtivo(btn_AbMenEstoque);
            MostrarForm(new frm_Estoque());
        }
 
        private void btn_AbMenOS_Click(object sender, EventArgs e)
        {
            BotaoAtivo(btn_AbMenOS);
            MostrarForm(new frm_OS());
        }

        private void picturebox_IcMenPrincipal_Click(object sender, EventArgs e)
        {
            MostrarForm(new frm_MenuPrincipal());
        }

        private void btn_AbPesquisar_Click(object sender, EventArgs e)
        {
            BotaoAtivo(btn_AbPesquisar);
            MostrarForm(new frm_Pesquisar());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Fecha o formulário atual
            this.Hide();
            frm_MenuPrincipal form2 = new frm_MenuPrincipal();
            // Exibe o novo formulário
            form2.ShowDialog();
         
        }

        private void btn_SalvarItensOS_Click(object sender, EventArgs e)
        {
            Conexao conectar_bd = new Conexao();
            try
            {
                conectar_bd.AbrirConexao();

                // Busca a quantidade atual do produto no banco de dados
                MySqlCommand comando_sql = new MySqlCommand("SELECT qtd_estoque from produto WHERE id_estoque = @id_estoque", conectar_bd.conexao);
                comando_sql.Parameters.AddWithValue("@id_estoque", txtb_IdProd.Text);
                int qtd_atual = (int)comando_sql.ExecuteScalar();

                // Soma a quantidade atual com a quantidade inserida no txtb_Qtd
                int qtd_inserir = int.Parse(txtb_Qtd.Text);
                int qtd_total = qtd_atual + qtd_inserir;

                // Atualiza a quantidade no banco de dados
                MySqlCommand comando_sql2 = new MySqlCommand("UPDATE produto SET qtd_estoque = @qtd_estoque WHERE id_estoque = @id_estoque", conectar_bd.conexao);
                comando_sql2.Parameters.AddWithValue("@qtd_estoque", qtd_total);
                comando_sql2.Parameters.AddWithValue("@id_estoque", txtb_IdProd.Text);
                comando_sql2.ExecuteNonQuery();

                // Exibe uma mensagem de sucesso
                MessageBox.Show("Quantidade atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                // Trata o erro de alguma forma (ex: exibe uma mensagem de erro)
                MessageBox.Show("Ocorreu um erro ao atualizar a quantidade: " + ex.Message);
            }
            conectar_bd.FecharConexao();
        }


        private void frm_MenuPrincipal_Load(object sender, EventArgs e)
        {
            Conexao conectar_bd = new Conexao();
            try
            {
                conectar_bd.AbrirConexao();
                MySqlCommand comando_sql = new MySqlCommand("SELECT id_estoque, nome_prod from produto", conectar_bd.conexao);
                MySqlDataReader leitor_sql = comando_sql.ExecuteReader();


                cb_InserirProd.Items.Clear(); // Limpa itens existentes no ComboBox

                while (leitor_sql.Read())
                {
                    // Adiciona cada nome ao ComboBox
                    string n_fantasia = leitor_sql.GetString("nome_prod");
                    cb_InserirProd.Items.Add(n_fantasia);
                }

                leitor_sql.Close();
            }
            catch (Exception ex)
            {
                // Trata o erro de alguma forma (ex: exibe uma mensagem de erro)
                MessageBox.Show("Ocorreu um erro ao listar produtos: " + ex.Message);
            }
            conectar_bd.FecharConexao();
        }

        private void cb_InserirProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexao conectar_bd = new Conexao();
            try
            {
                conectar_bd.AbrirConexao();

                // Recupera o id_estoque correspondente ao produto selecionado
                MySqlCommand comando_sql = new MySqlCommand("SELECT id_estoque from produto WHERE nome_prod = @nome_prod", conectar_bd.conexao);
                comando_sql.Parameters.AddWithValue("@nome_prod", cb_InserirProd.SelectedItem.ToString());
                int id_estoque = (int)comando_sql.ExecuteScalar();

                // Atribui o id_estoque ao Text do txtb_idprod
                txtb_IdProd.Text = id_estoque.ToString();
            }
            catch (Exception ex)
            {
                // Trata o erro de alguma forma (ex: exibe uma mensagem de erro)
                MessageBox.Show("Ocorreu um erro ao selecionar o produto: " + ex.Message);
            }
            conectar_bd.FecharConexao();
        }

    }
}

