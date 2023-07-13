using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using MySqlX.XDevAPI;
using prj_eStock.DAO;
using prj_eStock.Entidades;
using prj_eStock.Model;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.Design.WebControls;

namespace prj_eStock.View
{
    public partial class frm_OS : Form
    {
        OS_Entidade dados = new OS_Entidade();
        OS_Model os_Model = new OS_Model();
        public void CarregarDGV()
        {
            dgv_ListaClie.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_ListaClie.ForeColor = Color.Navy;
            dgv_NomeProd.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_NomeProd.ForeColor = Color.Navy;
            dgv_ListaProdutos.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            dgv_ListaProdutos.ForeColor = Color.Navy;
        }

        public void Limpar()
        {
            txtb_ClienteSelecionado.Text = null;
            txtb_IDLanc_OS.Text = null;
            txtb_MaoObra.Text = null;
            txtb_NomeEquip.Text = null;
            txtb_Porcentagem.Text = null;
            txtb_PrecoCusto.Text = null;
            txtb_ProdVendido.Text = null;
            txtb_Qtd.Text = null;
            txtb_ValorVenda.Text = null;
            rtb_Defeito.Text = null;
            rtb_ServExec.Text = null;
            dgv_ListaProdutos.Rows.Clear();

            txtb_ClienteSelecionado.Enabled = false;
            txtb_IDLanc_OS.Enabled = false;
            txtb_MaoObra.Enabled = false;
            txtb_NomeEquip.Enabled = false;
            txtb_Porcentagem.Enabled = false;
            txtb_PrecoCusto.Enabled = false;
            txtb_ProdVendido.Enabled = false;
            txtb_Qtd.Enabled = false;
            txtb_ValorVenda.Enabled = false;
            rtb_Defeito.Enabled = false;
            rtb_ServExec.Enabled = false;
            txtb_NomeEquip.Focus();
        }
        public void EnviarDados(OS_Entidade dados)
        {

            dados.Dt_os = Convert.ToDateTime(dtp_DataAtual.Text);
            dados.Id_cliente = Convert.ToInt32(txtb_ClienteSelecionado.Text);
            dados.Maquina_equip = txtb_NomeEquip.Text;
            dados.Defeito = rtb_Defeito.Text;
            dados.Servico_exe = rtb_ServExec.Text;
            dados.Mao_obra = Convert.ToDouble(txtb_MaoObra.Text);

            os_Model.Salvar(dados);
        }
        public void EnviarDados_Itens_OS(OS_Entidade_Itens_OS dados)
        {
            dados.Id_os = Convert.ToInt32(txtb_IDLanc_OS.Text);
            dados.Produto_vendido = txtb_ProdVendido.Text;
            dados.Quantidade = Convert.ToInt32(txtb_QtdTotal.Text);
            dados.Valor_custo = Convert.ToDouble(txtb_PrcCusto.Text);
            dados.Porcentagem = Convert.ToDouble(txtb_Porcentagem.Text);
            dados.Valor_venda = Convert.ToDouble(txtb_MaoObra.Text);

            os_Model.Salvar(dados);
        }


        public frm_OS()
        {
            InitializeComponent();
        }

        public void btn_Novo_Click(object sender, EventArgs e)
        {
            Limpar();
            dtp_DataAtual.Enabled = true;
            txtb_NomeEquip.Enabled = true;
            txtb_MaoObra.Enabled = true;
            rtb_ServExec.Enabled = true;
            rtb_Defeito.Enabled = true;
            dgv_ListaClie.Enabled = true;
            btn_limpar.Enabled = true;

        }

        public void frm_OS_Load(object sender, EventArgs e)
        {
            lbl_ListaClie.Text = "LISTA\nCLIENTES";
            lbl_NomeProd.Text = "LISTA\nPRODUTOS";
            Conexao conexao = new Conexao();
            conexao.AbrirConexao();
            CarregarDGV();

            Dictionary<string, int> clientes = new Dictionary<string, int>();

            string sql = "SELECT id_cliente 'ID CLIENTE', n_fantasia 'NOME CLIENTE' FROM cliente";

            using (MySqlCommand command = new MySqlCommand(sql, conexao.conexao))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dgv_ListaClie.DataSource = dataTable;
                }
            }
            Dictionary<string, int> produto = new Dictionary<string, int>();

            string sql2 = "SELECT id_estoque 'ID PRODUTO', nome_prod 'NOME PRODUTO', valor_custo 'PRECO CUSTO' FROM produto";



            using (MySqlCommand command = new MySqlCommand(sql2, conexao.conexao))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dgv_NomeProd.DataSource = dataTable;
                }
            }

            conexao.FecharConexao();
        }






        private void btn_Inserir_Click(object sender, EventArgs e)
        {
            Conexao conectar = new Conexao();
            MySqlCommand comandos_sql = new MySqlCommand();
            txtb_Qtd.Enabled = true;
            txtb_Porcentagem.Enabled = true;

            try
            {
                dgv_NomeProd.Enabled = true;
                conectar.AbrirConexao();
                comandos_sql = new MySqlCommand("SELECT MAX(id_os) as ultima_id FROM lanc_os", conectar.conexao);
                MySqlDataReader leitor = comandos_sql.ExecuteReader();
                this.EnviarDados(dados);
                if (leitor.Read())
                {
                    int resultado = Convert.ToInt32(leitor["ultima_id"].ToString());
                    resultado++;
                    txtb_IDLanc_OS.Text = resultado.ToString();
                    MessageBox.Show("Lançamento de OS inserida com Sucesso!!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro, Verifique os Dados inseridos, Certifique-se de estar preenchendo todos os campos do formulário, e selecionando ao clicar na tablea clientes um cliente válido!! ");
            }
            finally
            {
                conectar.FecharConexao();
            }






        }

        private void dgv_ListaClie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtb_ClienteSelecionado.Text = dgv_ListaClie.CurrentRow.Cells[0].Value.ToString();
        }

        private void dgv_NomeProd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtb_ProdVendido.Text = dgv_NomeProd.CurrentRow.Cells[0].Value.ToString();
            txtb_PrecoCusto.Text = dgv_NomeProd.CurrentRow.Cells[2].Value.ToString();
            txtb_PrecoCusto.ForeColor = Color.DarkGreen;

        }


        public void btn_SalvarItens_OS_Click(object sender, EventArgs e)
        {
            try
            {

                OS_Entidade_Itens_OS dados = new OS_Entidade_Itens_OS();


                EnviarDados_Itens_OS(dados);

                MessageBox.Show("Dados salvos com sucesso!!", "SUCESSO!!");

                frm_OS frm_OS = new frm_OS();
                frm_OS.Close();
                frm_SalvarOS frm2 = new frm_SalvarOS();
                frm2.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao tentar salvar itens_os", "ERRO AO SALVAR OS DADOS!!" + ex.Message);
            }





        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            Limpar();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma caixa de diálogo para salvar arquivos
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Imagens PNG|*.png";
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = "form";

            // Mostra a caixa de diálogo e verifica se o usuário clicou em "Salvar"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtém o caminho do arquivo escolhido pelo usuário
                string filePath = saveFileDialog.FileName;

                // Cria um Bitmap com a largura e altura do formulário
                Bitmap formImage = new Bitmap(this.Width, this.Height);

                // Cria um Graphics a partir do Bitmap
                using (Graphics g = Graphics.FromImage(formImage))
                {
                    // Desenha o formulário na imagem
                    this.DrawToBitmap(formImage, new Rectangle(0, 0, this.Width, this.Height));

                    // Desenha cada textbox, richbox e gridview na imagem
                    foreach (TextBox textbox in this.Controls.OfType<TextBox>())
                    {
                        textbox.DrawToBitmap(formImage, new Rectangle(textbox.Location, textbox.Size));
                    }

                    foreach (RichTextBox richTextBox in this.Controls.OfType<RichTextBox>())
                    {
                        richTextBox.DrawToBitmap(formImage, new Rectangle(richTextBox.Location, richTextBox.Size));
                    }

                    foreach (DataGridView gridView in this.Controls.OfType<DataGridView>())
                    {
                        gridView.DrawToBitmap(formImage, new Rectangle(gridView.Location, gridView.Size));
                    }
                    foreach (DateTimePicker dateTimePicker in this.Controls.OfType<DateTimePicker>())
                    {
                        dateTimePicker.DrawToBitmap(formImage, new Rectangle(dateTimePicker.Location, dateTimePicker.Size));
                    }
                    foreach (Label label in this.Controls.OfType<Label>())
                    {
                        label.DrawToBitmap(formImage, new Rectangle(label.Location, label.Size));
                    }
                    foreach (Button button in this.Controls.OfType<Button>())
                    {
                        button.DrawToBitmap(formImage, new Rectangle(button.Location, button.Size));
                    }
                    foreach (Panel panel in this.Controls.OfType<Panel>())
                    {
                        panel.DrawToBitmap(formImage, new Rectangle(panel.Location, panel.Size));
                    }


                }

                // Salva a imagem em um arquivo
                formImage.Save(filePath, ImageFormat.Png);

                // Mostra a mensagem de sucesso
                MessageBox.Show("A imagem do formulário foi salva com sucesso no arquivo " + filePath);
            }



        }
        public class Item
        {
            public string NomeProduto { get; set; }
            public double Quantidade { get; set; }
            public double PrecoCusto { get; set; }
        }

        public double TotalResultado;
        private void button2_Click(object sender, EventArgs e)
        {
            Conexao conexao = new Conexao();
            int prodID = Convert.ToInt32(txtb_ProdVendido.Text);
            int qtdVendida = Convert.ToInt32(txtb_Qtd.Text);

            conexao.AbrirConexao();
            try
            {
                btn_limpar.Enabled = false;
                if (txtb_ProdVendido.Text == "" && txtb_PrecoCusto.Text == "" && txtb_Qtd.Text == "")
                {
                    MessageBox.Show("Preencha Todos os Campos!!", "CAMPO VAZIO!!");
                }
                else
                {
                    // Verifica se a quantidade disponível é suficiente antes de atualizar o banco
                    int qtdEstoque = 0;
                    MySqlCommand comandoVerifica = conexao.conexao.CreateCommand();
                    comandoVerifica.CommandText = "SELECT qtd_estoque FROM produto WHERE id_estoque = @prodID";
                    comandoVerifica.Parameters.AddWithValue("@prodID", prodID);

                    MySqlDataReader reader = comandoVerifica.ExecuteReader();
                    while (reader.Read())
                    {
                        qtdEstoque = reader.GetInt32("qtd_estoque");
                    }

                    reader.Close();

                    if (qtdEstoque >= qtdVendida)
                    {
                        int quantidade = Convert.ToInt32(txtb_Qtd.Text);
                        double preco_custo = Convert.ToDouble(txtb_PrecoCusto.Text);
                        double porcentagem = Convert.ToDouble(txtb_Porcentagem.Text);
                        double maoObra = Convert.ToDouble(txtb_MaoObra.Text);
                        double resultado = (quantidade * preco_custo) + (quantidade * preco_custo * porcentagem / 100);

                        TotalResultado += resultado + maoObra;
                        txtb_ValorVenda.Text = TotalResultado.ToString();

                        // Atualiza o estoque apenas se houver quantidade suficiente em estoque
                        MySqlCommand comando = conexao.conexao.CreateCommand();
                        comando.CommandText = "UPDATE produto SET qtd_estoque = qtd_estoque - @qtdVendida WHERE id_estoque = @prodID";
                        comando.Parameters.AddWithValue("@prodID", prodID);
                        comando.Parameters.AddWithValue("@qtdVendida", qtdVendida);

                        if (dgv_ListaProdutos.Columns.Count == 0)
                        {
                            dgv_ListaProdutos.Columns.Add("NomeProduto", "Nome do Produto");
                            dgv_ListaProdutos.Columns.Add("Quantidade", "Quantidade");
                            dgv_ListaProdutos.Columns.Add("Preço Custo", "Preço Custo");
                        }

                        dgv_ListaProdutos.Rows.Add(dgv_NomeProd.CurrentRow.Cells[1].Value.ToString(), txtb_Qtd.Text, resultado);


                        double totalQuantidade = 0;
                        double totalPrecoCusto = 0;
                        for (int i = 0; i < dgv_ListaProdutos.Rows.Count; i++)
                        {
                            totalQuantidade += Convert.ToDouble(dgv_ListaProdutos.Rows[i].Cells[1].Value);
                            totalPrecoCusto += Convert.ToDouble(dgv_ListaProdutos.Rows[i].Cells[2].Value);
                        }

                        txtb_QtdTotal.Text = totalQuantidade.ToString();
                        txtb_PrcCusto.Text = totalPrecoCusto.ToString();

                        btn_SalvarItensOS.Enabled = true;
                        btn_SalvPDF.Enabled = true;



                       


                        int rowsAffected = comando.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Produto vendido com sucesso!", "PRODUTO SUBTRAIDO DO ESTOQUE");
                        }
                        else
                        {
                            MessageBox.Show("Erro ao vender produto.", "PRODUTO NAO SUBTRAIDO DO ESTOQUE");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não há mais produtos em estoque para venda.", "ERRO");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao vender produto. \n" + ex.Message);
                dgv_ListaProdutos.Rows.Clear();

            }
            finally
            {
                conexao.FecharConexao();
            }
        }

        public void btn_SalvarItensOS_Click(object sender, EventArgs e)
        {
            try
            {
                btn_limpar.Enabled = true;
                double maoObra = Convert.ToDouble(txtb_MaoObra.Text);
                double ValorVenda = Convert.ToDouble(txtb_ValorVenda.Text);
                double resultadoFinal = (ValorVenda);
                txtb_ValorVenda.Text = resultadoFinal.ToString();

                OS_Entidade_Itens_OS dados = new OS_Entidade_Itens_OS();

                /* Inserção ao salvar valor venda */
                dados.Id_os = Convert.ToInt32(txtb_IDLanc_OS.Text);
                dados.Produto_vendido = txtb_ProdVendido.Text;
                dados.Quantidade = Convert.ToInt32(txtb_QtdTotal.Text);
                dados.Valor_custo = Convert.ToDouble(txtb_PrecoCusto.Text);
                dados.Porcentagem = Convert.ToDouble(txtb_Porcentagem.Text);
                dados.Valor_venda = Convert.ToDouble(resultadoFinal);

                os_Model.Salvar(dados);

                MessageBox.Show("Dados salvos com sucesso!!", "SUCESSO!!");

                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocorreu um erro ao tentar salvar itens_os", "ERRO AO SALVAR OS DADOS!!" + ex.Message);
            }
        }

            private void btn_SalvPDF_Click(object sender, EventArgs e)
        {
            // Cria uma caixa de diálogo para salvar arquivos
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Imagens PNG|*.png";
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = "form";

            // Mostra a caixa de diálogo e verifica se o usuário clicou em "Salvar"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtém o caminho do arquivo escolhido pelo usuário
                string filePath = saveFileDialog.FileName;

                // Cria um Bitmap com a largura e altura do formulário
                Bitmap formImage = new Bitmap(this.Width, this.Height);

                // Cria um Graphics a partir do Bitmap
                using (Graphics g = Graphics.FromImage(formImage))
                {
                    // Desenha o formulário na imagem
                    this.DrawToBitmap(formImage, new Rectangle(0, 0, this.Width, this.Height));

                    // Desenha cada textbox, richbox e gridview na imagem
                    foreach (TextBox textbox in this.Controls.OfType<TextBox>())
                    {
                        textbox.DrawToBitmap(formImage, new Rectangle(textbox.Location, textbox.Size));
                    }

                    foreach (RichTextBox richTextBox in this.Controls.OfType<RichTextBox>())
                    {
                        richTextBox.DrawToBitmap(formImage, new Rectangle(richTextBox.Location, richTextBox.Size));
                    }

                    foreach (DataGridView gridView in this.Controls.OfType<DataGridView>())
                    {
                        gridView.DrawToBitmap(formImage, new Rectangle(gridView.Location, gridView.Size));
                    }
                    foreach (DateTimePicker dateTimePicker in this.Controls.OfType<DateTimePicker>())
                    {
                        dateTimePicker.DrawToBitmap(formImage, new Rectangle(dateTimePicker.Location, dateTimePicker.Size));
                    }
                    foreach (Label label in this.Controls.OfType<Label>())
                    {
                        label.DrawToBitmap(formImage, new Rectangle(label.Location, label.Size));
                    }
                    foreach (Button button in this.Controls.OfType<Button>())
                    {
                        button.DrawToBitmap(formImage, new Rectangle(button.Location, button.Size));
                    }
                    foreach (Panel panel in this.Controls.OfType<Panel>())
                    {
                        panel.DrawToBitmap(formImage, new Rectangle(panel.Location, panel.Size));
                    }


                }

                // Salva a imagem em um arquivo
                formImage.Save(filePath, ImageFormat.Png);

                // Mostra a mensagem de sucesso
                MessageBox.Show("A imagem do formulário foi salva com sucesso no arquivo " + filePath);
            }
        }
    }
}
