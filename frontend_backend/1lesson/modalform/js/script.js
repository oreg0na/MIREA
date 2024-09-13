document.getElementById("applicationForm").addEventListener("submit", function (event) {
  event.preventDefault(); // Останавливаем отправку формы

  // Очищаем предыдущие сообщения об ошибках
  const errorContainer = document.getElementById("errorMessages");
  errorContainer.innerHTML = "";
  let errors = [];

  // Валидация имени (должно содержать три слова - ФИО)
  const name = document.getElementById("name").value.trim();
  if (name === "") {
    errors.push("Введите ваше полное имя.");
  } else {
    const nameParts = name.split(/\s+/); // Разделяем по пробелам
    if (nameParts.length !== 3) {
      errors.push("Введите полное имя, состоящее из трех слов (ФИО).");
    }
  }

  // Валидация email
  const email = document.getElementById("email").value;
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (email.trim() === "") {
    errors.push("Введите ваш e-mail.");
  } else if (!emailPattern.test(email)) {
    errors.push("Введите корректный e-mail.");
  }

  // Валидация телефона
  const phone = document.getElementById("phone").value;
  const phonePattern = /^\+7\s\d{3}\s\d{3}\s\d{2}\s\d{2}$/;
  if (phone.trim() === "") {
    errors.push("Введите номер телефона.");
  } else if (!phonePattern.test(phone)) {
    errors.push("Введите корректный номер телефона в формате +7 919 999 99 99.");
  }

  // Валидация страны
  const country = document.getElementById("country").value;
  if (country === "") {
    errors.push("Выберите страну.");
  }

  // Валидация даты
  const date = document.getElementById("date").value;
  if (date === "") {
    errors.push("Выберите дату.");
  }

  // Валидация комментария (не обязательное поле, но можем добавить при необходимости)
  const comment = document.getElementById("comment").value;
  if (comment.trim() === "") {
    errors.push("Комментарий не может быть пустым.");
  }

  // Валидация согласия на обработку данных
  const consent = document.getElementById("consent").checked;
  if (!consent) {
    errors.push("Необходимо согласиться на обработку данных.");
  }

  // Проверка на наличие ошибок
  if (errors.length > 0) {
    errorContainer.innerHTML = errors.map(error => `<p>${error}</p>`).join('');
    errorContainer.style.display = 'block';
  } else {
    // Если ошибок нет, форма отправляется
    alert("Форма успешно отправлена!");
  }
});
