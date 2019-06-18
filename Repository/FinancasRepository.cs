using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FinancasRepository
    {
        private Conexao conexao;

        public FinancasRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM financas WHERE id =@ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public ContaPagar ObterpeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM financas WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if(tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            ContaPagar financa = new ContaPagar();
            financa.Id = Convert.ToInt32(linha["id"]);
            financa.Nome = linha["nome"].ToString();
            financa.Valor = Convert.ToDecimal(linha["valor"]);
            financa.Tipo = Convert.ToDecimal(linha["tipo"]);
            financa.Descricao = linha["descricao"].ToString();
            financa.Status = linha["status"].ToString();

            return financa;

        }

        public int Inserir(ContaPagar financa)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO financas (id, nome, valor, decricao, tipo, status)
OUTPUT INSERTD.ID
VALUES (@VALOR, @NOME, @DESCRICAO, @TIPO, @STATUS)";
            comando.Parameters.AddWithValue("@NOME", financa.Nome);
            comando.Parameters.AddWithValue("@VALOR", financa.Valor);
            comando.Parameters.AddWithValue("@DESCRICAO", financa.Descricao);
            comando.Parameters.AddWithValue("@TIPO", financa.Tipo);
            comando.Parameters.AddWithValue("@STATUS", financa.Status);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;

        }

        public List<ContaPagar> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM financas WHERE nome LIKE @NOME";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaPagar> financas = new List<ContaPagar>();
            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaPagar financa = new ContaPagar();
                financa.Id = Convert.ToInt32(linha["id"]);
                financa.Nome = linha["nome"].ToString();
                financa.Valor = Convert.ToDecimal(linha["valor"]);
                financa.Tipo = Convert.ToDecimal(linha["tipo"]);
                financa.Descricao = linha["descricao"].ToString();
                financa.Status = linha["status"].ToString();
                financas.Add(financa);
            }

            return financas;
        }

        public bool Atualizar(ContaPagar contaPagar)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE financa SET
nome = @NOME,
id = @ID,
valor = @VALOR,
tipo = @TIPO,
descricao = @DESCRICAO,
status = @STATUS";
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@ID", contaPagar.Id);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@TIPO", contaPagar.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contaPagar.Descricao);
            comando.Parameters.AddWithValue("@STATUS", contaPagar.Status);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }

   
}
