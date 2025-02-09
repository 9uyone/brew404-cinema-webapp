$(document).on('submit', 'form[data-ajax="true"]', function (e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr('action'),
        method: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            window.location.reload();
        },
        error: function (response) {
            const data = response.responseJSON;

            if (data && data.errors !== undefined)
                alert(Object.values(data.errors).flat().join('\n'));

            else if (data)
                alert(Object.values(data).flat().join('\n'));

            else if (response.responseText !== null)
                alert(response.responseText);

            else alert('Сталася помилка');
        }
    });
}); 

document.getElementById('openRegisterModal').addEventListener('click', function () {
    // Отримуємо екземпляр loginModal і закриваємо його
    const loginModal = bootstrap.Modal.getInstance(document.getElementById('loginModal'));
    if (loginModal)
        loginModal.hide();

    // Коли модальне вікно логіну повністю закрилося, відкриваємо вікно реєстрації
    const registerModal = new bootstrap.Modal(document.getElementById('registerModal'));
        setTimeout(() => {
            registerModal.show();
            }, 200); // Додаємо невелику затримку для плавного переходу
});

// Забезпечуємо видалення залишкових backdrop, якщо виникають проблеми
document.addEventListener('hidden.bs.modal', function () {
    const backdrop = document.querySelector('.modal-backdrop');
    if (backdrop)
        backdrop.remove();

    document.body.classList.remove('modal-open'); // Видаляємо залишковий клас, якщо він є
    document.body.style.paddingRight = ''; // Скидаємо padding, якщо Bootstrap додавав його
});