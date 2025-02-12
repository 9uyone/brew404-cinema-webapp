document.addEventListener("DOMContentLoaded", function () {
    const seatsContainer = document.getElementById("seatsContainer");
    const vipContainer = document.getElementById("vipContainer");
    const bookingDetails = document.getElementById("bookingDetails");
    const totalPriceElement = document.getElementById("totalPrice");

    const rows = 6;
    const seatsPerRow = 10;

    const VIProws = 1;
    const VIPseatsPerRow = 6;

    let totalPrice = 0;
    let selectedSeats = new Map();

    const tooltip = document.createElement("div");
    tooltip.className = "tooltip";
    document.body.appendChild(tooltip);
    let tooltipTimeout;

    const sessionId = @Model.Id;

    async function fetchOccupiedSeats() {
        const sessionId = 1;  // Тут вкажи актуальний sessionId
        try {
            const response = await fetch(`/api/tickets/occupiedSeats/${sessionId}`);
            if (!response.ok) {
                throw new Error(`Помилка запиту: ${response.statusText}`);
            }
            return await response.json();  // Повертає масив зайнятих місць, наприклад: ["1-1", "2-3", "VIP 1-2"]
        } catch (error) {
            console.error("Не вдалося отримати зайняті місця:", error);
            return [];
        }
    }

    const occupiedSeats = await fetchOccupiedSeats();

    // Створення місця
    function createSeat(row, seatNumber, isVip) {
        const seat = document.createElement("div");
        const seatId = `${row}-${seatNumber}`;
        seat.className = isVip ? "seat seat-vip" : "seat seat-available";
        seat.dataset.row = row;
        seat.dataset.seat = seatNumber;
        seat.dataset.price = isVip ? 420 : 190;

        if (occupiedSeats.includes(seatId)) {
            seat.classList.add("seat-occured");
            seat.title = "Місце зайняте";
            return seat;
        } // тут я вказав, якщо він зайнятий, то я додаю клас зайнятий 

        // Додавання події для вибору місця
        seat.addEventListener("click", function () {
            const price = parseInt(seat.dataset.price);

            if (selectedSeats.has(seatId)) {
                selectedSeats.delete(seatId);
                seat.classList.remove(isVip ? "seat-selected-vip" : "seat-selected");
            } else {
                selectedSeats.set(seatId, { type: isVip ? "Диван" : "Стандартний", price });
                seat.classList.add(isVip ? "seat-selected-vip" : "seat-selected");
            }
            updateBooking();
        });

        // Tooltip при наведенні
        seat.addEventListener("mouseenter", (event) => showTooltip(event, seat));
        seat.addEventListener("mouseleave", hideTooltip);

        return seat;
    }

    // Оновлення деталей бронювання
    function updateBooking() {
        bookingDetails.innerHTML = "";
        totalPrice = 0;

        selectedSeats.forEach(({ type, price }, seatId) => {
            const detail = document.createElement("div");
            detail.className = "booking-details";
            detail.innerHTML = `<span>${type} (${seatId})</span><span>${price} грн</span>`;
            bookingDetails.appendChild(detail);
            totalPrice += price;
        });

        totalPriceElement.textContent = `${totalPrice} грн`;
    }

    // Показати tooltip
    function showTooltip(event, seat) {
        clearTimeout(tooltipTimeout);
        tooltipTimeout = setTimeout(() => {
            tooltip.innerHTML = `${seat.dataset.row} Ряд, ${seat.dataset.seat} Місце<br>Ціна: ${seat.dataset.price} грн`;
            tooltip.style.left = `${event.pageX + 10}px`;
            tooltip.style.top = `${event.pageY - 30}px`;
            tooltip.style.visibility = "visible";
            tooltip.style.opacity = "1";
        }, 100);
    }

    // Приховати tooltip
    function hideTooltip() {
        clearTimeout(tooltipTimeout);
        tooltip.style.visibility = "hidden";
        tooltip.style.opacity = "0";
    }

    // Генерація стандартних місць
    seatsContainer.style.display = "grid";
    seatsContainer.style.gridTemplateColumns = `repeat(${seatsPerRow}, 1fr)`;
    seatsContainer.style.gap = "8px";

    for (let r = 1; r <= rows; r++) {
        for (let s = 1; s <= seatsPerRow; s++) {
            seatsContainer.appendChild(createSeat(r, s, false));
        }
    }

    // Генерація VIP місць
    vipContainer.style.display = "grid";
    vipContainer.style.gridTemplateColumns = `repeat(${VIPseatsPerRow}, 1fr)`;
    vipContainer.style.gap = "8px";

    for (let r = 1; r <= VIProws; r++) {
        for (let s = 1; s <= VIPseatsPerRow; s++) {
            vipContainer.appendChild(createSeat(`VIP ${r}`, s, true));
        }
    }
});
