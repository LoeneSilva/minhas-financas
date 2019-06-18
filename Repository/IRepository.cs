using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepository
    {
        int Inserir(ContaPagar financas);

        bool Apagar(int id);

        bool Atualizar(ContaPagar financas);

        ContaPagar ObterPeloId(int id);

        List<ContaPagar> ObterTodos(string busca);
    }
}
