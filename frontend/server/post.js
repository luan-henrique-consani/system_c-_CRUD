const envBtn = document.getElementById("env");
const nameText = document.getElementById("name");
const emailText = document.getElementById("email");
const passwordText = document.getElementById("password");
const createdSpan = document.getElementById("created");

envBtn.addEventListener("click", (e) => {
  e.preventDefault();

  fetch("http://localhost:5124/users", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      name: nameText.value,
      email: emailText.value,
      password: passwordText.value,
    }),
  })
  .then(res => res.json())
  .then(data => {
    createdSpan.innerHTML = "Usuário criado com sucesso!";
  })
  .catch(err => createdSpan.innerHTML = err);
});

