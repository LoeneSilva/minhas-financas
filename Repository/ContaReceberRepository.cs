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
    public class ContaReceberRepository
    {
        private Conexao conexao;

        public ContaReceberRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(linha["id"]);
            contaReceber.Nome = linha["nome"].ToString();
            contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
            contaReceber.Tipo = linha["tipo"].ToString();
            contaReceber.Descricao = linha["descricao"].ToString();
            contaReceber.Status = linha["status"].ToString();

            return contaReceber;
        }

        public int Inserir(ContaReceber receber)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO receber (nome, valor, tipo, descricao, status)
OUTPUT INSERTED.ID
VALUES (@NOME,@VALOR,@TIPO,@DESCRICAO,@STATUS)";
            comando.Parameters.AddWithValue("@NOME", receber.Nome);
            comando.Parameters.AddWithValue("@VALOR", receber.Valor);
            comando.Parameters.AddWithValue("@TIPO", receber.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", receber.Descricao);
            comando.Parameters.AddWithValue("@STATUS", receber.Status);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public List<ContaReceber> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM receber WHERE nome LIKE @NOME";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaReceber> contasReceber = new List<ContaReceber>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaReceber conta = new ContaReceber();
                conta.Id = Convert.ToInt32(linha["id"]);
                conta.Nome = linha["nome"].ToString();
                conta.Valor = Convert.ToDecimal(linha["valor"]);
                conta.Tipo = linha["tipo"].ToString();
                conta.Descricao = linha["descricao"].ToString();
                conta.Status = linha["status"].ToString();
                contasReceber.Add(conta);
            }

            return contasReceber;
        }

        public bool Atualizar(ContaReceber contasReceber)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE receber SET
nome = @NOME,
id = @ID,
valor = @VALOR,
tipo = @TIPO,
descricao = @DESCRICAO,
status = @STATUS";
            comando.Parameters.AddWithValue("@NOME", contasReceber.Nome);
            comando.Parameters.AddWithValue("@ID", contasReceber.Id);
            comando.Parameters.AddWithValue("@VALOR", contasReceber.Valor);
            comando.Parameters.AddWithValue("@TIPO", contasReceber.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contasReceber.Descricao);
            comando.Parameters.AddWithValue("@STATUS", contasReceber.Status);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
