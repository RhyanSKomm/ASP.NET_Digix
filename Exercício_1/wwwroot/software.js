// document.getElementById("softwareform").addEventListener("submit", salvarSofware());

carregarSofware();

function carregarSofware() {
    fetch("http://localhost:5000/Software")
        .then(res => res.json())
        .then(data => {
            const tbody = document.querySelector("#tabelaSoftwares tbody");
            tbody.innerHTML = "";
            data.forEach(software => {
                tbody.innerHTML += `
                    <tr>
                        <td>${software.id}</td>
                        <td>${software.produto}</td>
                        <td>${software.hardDisk}</td>
                        <td>${software.memoriaRam}</td>
                        <td>${software.fkMaquina}</td>
                        <td>
                            <button class="edit" onclick="editarSoftware(${software.id})">Editar</button>
                            <button class="delete" onclick='deletarSoftware(${software.id})'>Deletar</button>
                        </td>
                    </tr>
                `;
            });
        });
}



function salvarSoftware(event) {
    event.preventDefault();

    const software = {
        id: parseInt(document.getElementById("id_Software").value),
        produto: document.getElementById("produto").value, // corrigido
        hardDisk: parseInt(document.getElementById("harddisk").value),
        memoriaRam: parseInt(document.getElementById("memoria_ram").value),
        fkUsuario: parseInt(document.getElementById("fk_maquina").value)
    };


    fetch("http://localhost:5000/Software", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(software)
    })
        .then(res => res.json())
        .then(data => {
            console.log("Usuário salvo:", data);
            carregarSoftwares();
            document.getElementById("softwareform").reset();
        })
        .catch(error => console.error("Erro ao salvar:", error));
}



function editarSoftware(id) {
    fetch(`${"http://localhost:5000/Software"}/${id}`)
        .then(res => res.json())
        .then(software => {
                document.getElementById("id").value = software.id,
                document.getElementById("produto").value = software.produto,
                document.getElementById("harddisk").value = software.hardDisk,
                document.getElementById("memoria_ram").value = software.memoriaRam,
                document.getElementById("fk_maquina").value = software.fkMaquina
        });
}

function deletarSoftware(id) {
    if (confirm("Deseja realmente excluir este usuário?")) {
        fetch(`${"http://localhost:5000/Software"}/${id}`, { method: "DELETE" })
            .then(res => {
                if (!res.ok) throw new Error("Erro ao deletar");
                carregarSofware();
            })
            .catch(error => console.error("Erro:", error));
    }
}