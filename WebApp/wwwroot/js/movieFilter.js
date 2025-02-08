const MovieFilter = {
    init: function() {
        this.filterButtons = document.querySelectorAll('.filter-button');
        this.filterCheckboxes = document.querySelectorAll('input[type="checkbox"]');
        this.filterInputs = document.querySelectorAll('input[type="number"]');
        this.searchInput = document.getElementById('searchInput');
        this.loadingIndicator = document.querySelector('.loading-indicator');
        
        this.setupEventListeners();
    },

    toggleLoading: function(show) {
        this.loadingIndicator.style.display = show ? 'flex' : 'none';
    },

    filterMovies: function() {
        const selectedGenres = [...document.querySelectorAll('input[name="GenreIds"]:checked')].map(cb => cb.value);
        const year = document.getElementById('yearInput').value;
        const searchQuery = this.searchInput.value.toLowerCase();
        const sortBy = document.getElementById('sortBy').value;
        const sortOrder = document.getElementById('sortOrder').value;

        const movieResults = document.getElementById('movieResults');
        const movies = movieResults.querySelectorAll('.col-md-4');
        let moviesArray = [...movies];

        moviesArray.forEach(movieCol => {
            const card = movieCol.querySelector('.movie-card');
            const title = card.querySelector('.card-title').textContent.toLowerCase();
            const movieYear = card.querySelector('.movie-year').textContent;
            
            const movieGenres = (card.dataset.genres || '').split(',').filter(Boolean);
            const matchesGenres = selectedGenres.length === 0 || 
                selectedGenres.every(genreId => movieGenres.includes(genreId));

            const matchesSearch = searchQuery === '' || title.includes(searchQuery);
            const matchesYear = !year || movieYear.includes(year);

            movieCol.style.display = (matchesSearch && matchesYear && matchesGenres) ? '' : 'none';
        });

        if (sortBy && sortOrder) {
            moviesArray.sort((a, b) => {
                const aValue = a.querySelector(sortBy === 'Title' ? '.card-title' : '.movie-year').textContent;
                const bValue = b.querySelector(sortBy === 'Title' ? '.card-title' : '.movie-year').textContent;
                
                const comparison = aValue.localeCompare(bValue);
                return sortOrder === 'true' ? -comparison : comparison;
            });

            moviesArray.forEach(movie => movieResults.appendChild(movie));
        }

        this.updateNoResultsMessage(moviesArray);
    },

    updateNoResultsMessage: function(moviesArray) {
        const hasVisibleMovies = moviesArray.some(movie => movie.style.display !== 'none');
        let noResultsMessage = document.querySelector('.no-results');
        
        if (!hasVisibleMovies) {
            if (!noResultsMessage) {
                noResultsMessage = document.createElement('div');
                noResultsMessage.className = 'no-results';
                noResultsMessage.textContent = 'Фільмів не знайдено';
                document.getElementById('movieResults').appendChild(noResultsMessage);
            }
        } else if (noResultsMessage) {
            noResultsMessage.remove();
        }
    },

    updateGenreCount: function() {
        const selectedCount = document.querySelectorAll('.genre-checkbox:checked').length;
        const genreButton = document.querySelector('[data-filter="genres"] span');
        genreButton.textContent = selectedCount > 0 ? `Жанри (${selectedCount})` : 'Жанри';
    },

    setupEventListeners: function() {
        // Жанри
        const genreCheckboxes = document.querySelectorAll('.genre-checkbox');
        genreCheckboxes.forEach(checkbox => {
            checkbox.addEventListener('change', () => {
                this.updateGenreCount();
                this.filterMovies();
            });
        });

        // Пошук
        let searchTimeout;
        this.searchInput.addEventListener('input', () => {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => this.filterMovies(), 300);
        });

        // Кнопки фільтрів
        this.filterButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                e.stopPropagation();
                const dropdown = button.nextElementSibling;
                
                document.querySelectorAll('.filter-dropdown.show').forEach(d => {
                    if (d !== dropdown) {
                        d.classList.remove('show');
                        d.previousElementSibling.classList.remove('active');
                    }
                });
                
                dropdown.classList.toggle('show');
                button.classList.toggle('active');
            });
        });

        // Закриття дропдаунів при кліку поза ними
        document.addEventListener('click', (e) => {
            if (!e.target.closest('.filter-section')) {
                document.querySelectorAll('.filter-dropdown').forEach(dropdown => {
                    dropdown.classList.remove('show');
                    if (dropdown.previousElementSibling) {
                        dropdown.previousElementSibling.classList.remove('active');
                    }
                });
            }
        });

        // Форма фільтрації
        document.getElementById('filterForm').addEventListener('submit', (e) => {
            e.preventDefault();
            this.filterMovies();
            
            document.querySelectorAll('.filter-dropdown').forEach(dropdown => {
                dropdown.classList.remove('show');
                if (dropdown.previousElementSibling) {
                    dropdown.previousElementSibling.classList.remove('active');
                }
            });
        });

        // Інші елементи фільтрації
        this.filterCheckboxes.forEach(checkbox => {
            checkbox.addEventListener('change', () => this.filterMovies());
        });

        this.filterInputs.forEach(input => {
            input.addEventListener('change', () => this.filterMovies());
        });

        document.getElementById('sortBy').addEventListener('change', () => this.filterMovies());
        document.getElementById('sortOrder').addEventListener('change', () => this.filterMovies());
    }
};

// Ініціалізація при завантаженні сторінки
document.addEventListener('DOMContentLoaded', () => MovieFilter.init());