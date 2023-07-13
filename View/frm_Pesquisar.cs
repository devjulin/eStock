using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using prj_eStock.Entidades;

namespace prj_eStock.View
{
    public partial class frm_Pesquisar : Form
    {
        public void CarregarDGV()
        {
            dgv_Pesquisar.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_Pesquisar.ForeColor = Color.Navy;
            dgv_Pesquisar.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_Pesquisar.ForeColor = Color.Navy;
            dgv_Pesquisar.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_Pesquisar.ForeColor = Color.Navy;
        }
        public frm_Pesquisar()
        {
            InitializeComponent();
        }

        public void btn_SalvarItensOS_Click(object sender, EventArgs e)
        {
            Conexao conexaoBD = new Conexao();
            conexaoBD.AbrirConexao();

            // Obter o nome selecionado no ComboBox
            string nomeCliente = cb_Pesquisar.SelectedItem.ToString();

            // Consulta SQL para obter os dados relacionados ao cliente selecionado
            string consulta = @"SELECT lanc_os.dt_os DATA, lanc_os.maquina_equip EQUIPAMENTO, itens_os.quantidade QUANTIDADE, lanc_os.defeito DEFEITO, lanc_os.servico_exe 'SERVICO EXECUTADO', itens_os.valor_venda 'VALOR TOTAL VENDA'
                   FROM lanc_os
                    INNER JOIN itens_os ON lanc_os.id_os = itens_os.id_os
                    INNER JOIN cliente ON lanc_os.id_cliente = cliente.id_cliente
                    INNER JOIN produto ON itens_os.produto_vendido = produto.id_estoque
                    WHERE cliente.n_fantasia = @nomeCliente
                    GROUP BY lanc_os.id_os, cliente.n_fantasia, produto.nome_prod
                    ORDER BY lanc_os.dt_os DESC";

            // Criar o objeto de comando e passar a consulta e a conexão como parâmetros
            using (MySqlCommand comando = new MySqlCommand(consulta, conexaoBD.conexao))
            {
                // Adicionar o parâmetro com o nome do cliente selecionado
                comando.Parameters.AddWithValue("@nomeCliente", nomeCliente);

                // Criar o objeto de adaptador de dados e passar o comando como parâmetro
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                {
                    // Criar um novo objeto DataTable para armazenar os dados
                    DataTable dados = new DataTable();

                    // Preencher o DataTable com os dados do adaptador
                    adaptador.Fill(dados);

                    // Atribuir o DataTable como o DataSource do DataGridView
                    dgv_Pesquisar.DataSource = dados;
                }
            }
            // Definir a formatação monetária para a coluna 5
            dgv_Pesquisar.Columns[5].DefaultCellStyle.Format = "C2";

            // Definir a cor do texto para a célula na linha 0 e coluna 6 como vermelho
            dgv_Pesquisar.Columns[5].DefaultCellStyle.ForeColor = Color.DarkGreen;


            conexaoBD.FecharConexao();
        }



        private void frm_Pesquisar_Load(object sender, EventArgs e)
        {
            CarregarDGV();
            Conexao conectar_bd = new Conexao();
            try
            {
                conectar_bd.AbrirConexao();
                MySqlCommand comando_sql = new MySqlCommand("SELECT n_fantasia FROM cliente, lanc_os WHERE lanc_os.id_cliente = cliente.id_cliente GROUP BY n_fantasia", conectar_bd.conexao);
                MySqlDataReader leitor_sql = comando_sql.ExecuteReader();


                cb_Pesquisar.Items.Clear(); // Limpa itens existentes no ComboBox

                while (leitor_sql.Read())
                {
                    // Adiciona cada nome ao ComboBox
                    string n_fantasia = leitor_sql.GetString("n_fantasia");
                    cb_Pesquisar.Items.Add(n_fantasia);
                }

                leitor_sql.Close();
            }
            catch (Exception ex)
            {
                // Trata o erro de alguma forma (ex: exibe uma mensagem de erro)
                MessageBox.Show("Ocorreu um erro ao buscar os clientes: " + ex.Message);
            }
            conectar_bd.FecharConexao();
        }

        public void dgv_Pesquisar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rtb_DefeitoApresent.Text = dgv_Pesquisar.CurrentRow.Cells[3].Value.ToString();
            rtb_ServicoExec.Text = dgv_Pesquisar.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
