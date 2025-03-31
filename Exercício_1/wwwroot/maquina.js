document.getElementById("maquinaform").addEventListener("submit", salvarMaquina);

carregarMaquina();

function carregarMaquina() {
    fetch("http://localhost:5000/Maquina")
        .then(res => res.json())
        .then(data => {
            const tbody = document.querySelector("#tabelaMaquinas tbody");
            tbody.innerHTML = "";
            data.forEach(maquina => {
                tbody.innerHTML += `
                    <tr>
                        <td>${maquina.tipo}</td>
                        <td>${maquina.velocidade}</td>
                        <td>${maquina.hardDisk}</td>
                        <td>${maquina.placaRede}</td>
                        <td>${maquina.memoriaRam}</td>
                        <td>${maquina.fkUsuario}</td>
                        <td>
                            <button class="edit" onclick="editarMaquina(${maquina.id})">Editar</button>
                            <button class="delete" onclick='deletarMaquina(${maquina.id})'>Deletar</button>
                        </td>
                    </tr>
                `;
            });
        });
}



function salvarMaquina(event) {
    event.preventDefault();

    const maquina = {
        id: parseInt(document.getElementById("id_maquina").value),
        tipo: document.getElementById("tipo").value, // corrigido
        velocidade: parseInt(document.getElementById("velocidade").value),
        hardDisk: parseInt(document.getElementById("harddisk").value),
        placaRede: parseInt(document.getElementById("placa_rede").value),
        memoriaRam: parseInt(document.getElementById("memoria_ram").value),
        fkUsuario: parseInt(document.getElementById("fk_usuario").value)
    };


    fetch("http://localhost:5000/Maquina", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(maquina)
    })
        .then(res => res.json())
        .then(data => {
            console.log("Usuário salvo:", data);
            carregarMaquinas();
            document.getElementById("maquinaform").reset();
        })
        .catch(error => console.error("Erro ao salvar:", error));
}



function editarMaquina(id) {
    fetch(`${"http://localhost:5000/Maquina"}/${id}`)
        .then(res => res.json())
        .then(maquina => {
            document.getElementById("id_maquina").value = maquina.id,
                document.getElementById("tipo").value = maquina.tipo, // corrigido
                document.getElementById("velocidade").value = maquina.velocidade,
                document.getElementById("harddisk").value = maquina.hardDisk,
                document.getElementById("placa_rede").value = maquina.placaRede,
                document.getElementById("memoria_ram").value = maquina.memoriaRam,
                document.getElementById("fk_usuario").value = maquina.fkUsuario
        });
}

function deletarMaquina(id) {
    if (confirm("Deseja realmente excluir este usuário?")) {
        fetch(`${"http://localhost:5000/Maquina"}/${id}`, { method: "DELETE" })
            .then(res => {
                if (!res.ok) throw new Error("Erro ao deletar");
                carregarMaquina();
            })
            .catch(error => console.error("Erro:", error));
    }
}