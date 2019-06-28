using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepositoryContasReceber
    {
        int Inserir(ContaReceber contasReceber);

        bool Apagar(int id);

        bool Atualizar(ContaReceber contasReceber);

        ContaReceber ObterPeloId(int id);

        List<ContaReceber> ObterTodos(string busca);
    }
}
