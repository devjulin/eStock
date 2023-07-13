using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_eStock.Entidades
{
    public class CadClienteEntidade
    {
        int id_cliente;
        string n_fantasia, cpf_cnpj, e_logradouro, e_cidade, e_estado;

        public int Id_cliente { get => id_cliente; set => id_cliente = value; }
        public string N_fantasia { get => n_fantasia; set => n_fantasia = value; }
        public string Cpf_cnpj { get => cpf_cnpj; set => cpf_cnpj = value; }
        public string E_logradouro { get => e_logradouro; set => e_logradouro = value; }
        public string E_cidade { get => e_cidade; set => e_cidade = value; }
        public string E_estado { get => e_estado; set => e_estado = value; }
    }
}
