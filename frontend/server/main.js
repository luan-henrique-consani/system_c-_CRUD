let currentPage = 1;
const pageSize = 2;

const tableBody = document.querySelector("#user-table tbody");
const pageInfo = document.getElementById("page-info");
const prevBtn = document.getElementById("prev");
const nextBtn = document.getElementById("next");

function loadUsers(page) {
  fetch(`http://localhost:5124/users?page=${page}&pageSize=${pageSize}`)
    .then((res) => res.json())
    .then((data) => {
      console.log("DATA RECEBIDA:", data);

      if (!data || !Array.isArray(data.data)) {
        console.error("Formato inválido:", data);
        return;
      }
      tableBody.innerHTML = "";

      data.data.forEach((user) => {
        const row = document.createElement("tr");

        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "Deletar";

        deleteBtn.addEventListener("click", () => {
          if (!confirm(`Deseja deletar ${user.name}?`)) return;

          fetch(`http://localhost:5124/users/${user.id}`, {
            method: "DELETE",
          }).then((res) => {
            if (res.ok) {
              loadUsers(currentPage); // 🔄 recarrega lista
            } else {
              alert("Erro ao deletar usuário");
            }
          });
        });

        row.innerHTML = `
          <td>${user.id}</td>
          <td>${user.name}</td>
          <td>${user.email}</td>
          <td>${user.status}</td>
        `;

        const tdAction = document.createElement("td");
        tdAction.appendChild(deleteBtn);

        row.appendChild(tdAction);
        tableBody.appendChild(row);
      });

      currentPage = data.page;
      pageInfo.textContent = `Página ${data.page} de ${data.totalPages}`;

      prevBtn.disabled = currentPage === 1;
      nextBtn.disabled = currentPage === data.totalPages;
    });

  prevBtn.addEventListener("click", () => {
    if (currentPage > 1) {
      loadUsers(currentPage - 1);
    }
  });

  nextBtn.addEventListener("click", () => {
    loadUsers(currentPage + 1);
  });
}

loadUsers(currentPage);
