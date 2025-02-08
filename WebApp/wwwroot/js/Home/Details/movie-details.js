document.getElementById('scrollLeft').addEventListener('click', function () {
    document.getElementById('actorCarousel').scrollBy({ left: -200, behavior: 'smooth' });
});

document.getElementById('scrollRight').addEventListener('click', function () {
    document.getElementById('actorCarousel').scrollBy({ left: 200, behavior: 'smooth' });
});

document.getElementById('mscrollLeft').addEventListener('click', function () {
    document.getElementById('movieCarousel').scrollBy({ left: -200, behavior: 'smooth' });
});

document.getElementById('mscrollRight').addEventListener('click', function () {
    document.getElementById('movieCarousel').scrollBy({ left: 200, behavior: 'smooth' });
});

const groupedSessions = JSON.parse(document.getElementById('groupedSessions').textContent);
const dates = JSON.parse(document.getElementById('dates').textContent);
var selectedIndex = 0;

document.getElementById('prevDateBtn')?.addEventListener('click', function () {
    if (selectedIndex > 0) updateDate(selectedIndex - 1);
});

document.getElementById('nextDateBtn')?.addEventListener('click', function () {
    if (selectedIndex < dates.length - 1) updateDate(selectedIndex + 1);
});

function updateDate(index) {
    selectedIndex = index;
    var selectedDate = new Date(dates[selectedIndex]);

    document.getElementById('selectedDate').innerText = selectedDate.toLocaleDateString('uk-UA', { day: 'numeric', month: 'long' });
    document.getElementById('selectedDay').innerText = selectedDate.toLocaleDateString('uk-UA', { weekday: 'long' });

    loadSessionsForDate();
}

function loadSessionsForDate() {
    var formattedDate = dates[selectedIndex];
    var sessions = groupedSessions[formattedDate];
    var sessionContainer = document.getElementById('sessionContainer');
    sessionContainer.innerHTML = '';

    if (sessions.length > 0) {
        sessions.forEach(session => {
            var sessionElement = document.createElement('a');
            sessionElement.href = `/Home/SessionDetails/${session.id}`;
            sessionElement.classList.add('session-link');
            sessionElement.innerHTML = `<div class="session-time">
                ${new Date(session.startTime).toLocaleTimeString('uk-UA', { hour: '2-digit', minute: '2-digit' })}
            </div>`;
            sessionContainer.appendChild(sessionElement);
        });
    } else {
        sessionContainer.innerHTML = '<div class="no-sessions">Немає сеансів для цієї дати</div>';
    }
}
