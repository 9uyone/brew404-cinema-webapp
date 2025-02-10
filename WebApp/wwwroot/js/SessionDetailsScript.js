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

    function createSeat(row, seatNumber, isVip) {
        const seat = document.createElement("div");
        seat.className = isVip ? "seat seat-vip" : "seat seat-available";
        seat.dataset.row = row;
        seat.dataset.seat = seatNumber;
        seat.dataset.price = isVip ? 420 : 190;

        seat.addEventListener("click", function () {
            const seatId = `${row}-${seatNumber}`;
            const price = parseInt(seat.dataset.price);

            if (selectedSeats.has(seatId)) {
                selectedSeats.delete(seatId);
                seat.classList.toggle(isVip ? "seat-selected-vip" : "seat-selected");
            } else {
                selectedSeats.set(seatId, { type: isVip ? "Диван" : "Стандартний", price });
                seat.classList.toggle(isVip ? "seat-selected-vip" : "seat-selected");
            }
            updateBooking();
        });

        seat.addEventListener("mouseenter", (event) => showTooltip(event, seat));
        seat.addEventListener("mouseleave", hideTooltip);

        return seat;
    }

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

    function hideTooltip() {
        clearTimeout(tooltipTimeout);
        tooltip.style.visibility = "hidden";
        tooltip.style.opacity = "0";
    }

    seatsContainer.style.display = "grid";
    seatsContainer.style.gridTemplateColumns = `repeat(${seatsPerRow}, 1fr)`;
    seatsContainer.style.gap = "8px";

    for (let r = 1; r <= rows; r++) {
        for (let s = 1; s <= seatsPerRow; s++) {
            seatsContainer.appendChild(createSeat(r, s, false));
        }
    }

    vipContainer.style.display = "grid";
    vipContainer.style.gridTemplateColumns = `repeat(${VIPseatsPerRow}, 1fr)`;
    vipContainer.style.gap = "8px";

    for (let r = 1; r <= VIProws; r++) {
        for (let s = 1; s <= VIPseatsPerRow; s++) {
            vipContainer.appendChild(createSeat(`VIP ${r}`, s, true));
        }
    }
});