using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using prj_eStock.DAO;
using prj_eStock.Entidades;
using prj_eStock.Model;
using prj_eStock.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace prj_eStock
{
    public partial class frm_TelaInicial : Form
    {
        
        Conexao conexao = new Conexao();
        public string UsuLogin { get; set; }

        public frm_TelaInicial()
        {
            InitializeComponent();
        }
        //criar projeto
        //gerar form
        //instalar mysql.data.mysqlclient com nuget
        //criar classe conexao
        //criar pasta DAO, ENTIDADES, MODEL

        private void frm_TelaInicial_Load(object sender, EventArgs e)
        {
            lbl_InfTelInicial.Text = "INFORMAÇÃO SOBRE O PROGRAMA!\n" +
                "Faça o cadastramento de seu usuário [funcionário]\n" +
                "logo após, cadastre seus produtos direto no estoque\n" +
                "Adicionando quantidade min, como parâmetro para exibição\n" +
                "Faça o lançamento dos produtos do estoque utilizando O.S\n" +
                "Para Suporte contate o email:";

            lbl_InfOBS.Text = "OBS: Este software foi desenvolvido para uso interno\n controlado aos funcionários da empresa\n D.A Comércios e Serviços";
            link_NaoPCad.Text = "Não Possui Cadastro?\nClique Aqui!!";
        }
        private void btn_RedCadastrar_Click(object sender, EventArgs e)
        {
            CadFuncionarioModel verifcamposcad = new CadFuncionarioModel();
            // No formulário principal, chame a função e atribua o resultado a uma variável
            bool valido = verifcamposcad.Verificar(txtb_Nome_Tela_Inicial.Text, txtb_Login_Tela_Inicial.Text, txtb_Senha_Tela_Inicial.Text);
            if (valido)
            {
                CadFuncionarioEntidade usuariocadastro = new CadFuncionarioEntidade();
                usuariocadastro.UsuNome1 = txtb_Nome_Tela_Inicial.Text;
                usuariocadastro.UsuUsuario1 = txtb_Login_Tela_Inicial.Text;
                usuariocadastro.UsuSenha1 = txtb_Senha_Tela_Inicial.Text;
                CadfuncionarioDAO user = new CadfuncionarioDAO();
                bool conectou = user.CadUsuario(usuariocadastro);
            }
        }

        private void link_NaoPCad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btn_RedEntrar.Visible = false;
            btn_RedCadastrar.Visible = true;
            lbl_nome_Tela_Principal.Visible = true;
            txtb_Nome_Tela_Inicial.Visible = true;
            pictureBox3.Visible = true;
            link_NaoPCad.Visible = false;
            btn_RetLogin.Visible = true;
           
        }

        private void btn_RetLogin_Click(object sender, EventArgs e)
        {
            btn_RedEntrar.Visible = true;
            btn_RedCadastrar.Visible = false;
            lbl_nome_Tela_Principal.Visible = false;
            txtb_Nome_Tela_Inicial.Visible = false;
            pictureBox3.Visible = false;
            link_NaoPCad.Visible = true;
            btn_RetLogin.Visible = false;
        }

        public void btn_RedEntrar_Click(object sender, EventArgs e)
        {
            VerifCamposLog verifCamposLog = new VerifCamposLog();
            Login login = new Login();

            login.UsuUsuario1 = txtb_Login_Tela_Inicial.Text;
            login.UsuSenha1 = txtb_Senha_Tela_Inicial.Text;
            UsuLogin = txtb_Login_Tela_Inicial.Text;
            

            bool con = verifCamposLog.confirma_login(login);

            if (con == true)
            {
                
                frm_MenuPrincipal MenuP = new frm_MenuPrincipal();
                MenuP.ShowDialog();
                // Libera os recursos usados pelo formulário
                MenuP.Dispose();

            }
            else
            {
                MessageBox.Show("Login ou Senha INCORRETOS , TENTE NOVAMENTE !!! ");
            }
          
            
        }


    private void botaoinserirpdf_Click(object sender, EventArgs e)
        {

        }
      
    }
}
