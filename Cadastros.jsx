
function Cadastros() {

    function getFormulario() {
        return (
            <form>
                <label for="name">Nome</label>
                <input type="text" id="name" name="name" />
                <label for="cpf">CPF</label>
                <input type="text" id="cpf" name="cpf" multiple />
                <label For="telefone">Telefone</label>
                <input type="text" id="telefone" name="telefone" />
                <label For="email" >e-mail</label>
                <input type="text" id="email" name="email" />
                <button>Salvar</button>
            </form>
        );
    }

    function getTabela() {
        return (
            <table>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>CPF</th>
                    <th>Ações</th>
                </tr>
                <tr>
                    <td>1</td>
                    <td>Maria</td>
                    <td>12345</td>
                    <td>
                        <button>Excluir</button>
                        <button>Editar</button>
                    </td>
                </tr>
                <button>Editar</button>
            </table>
        );
    }


    return (
        <div>
            <h1>Formulário CRUD</h1>
            {getFormulario()}
            {getTabela()}
        </div>

    );
}

export default Cadastros;