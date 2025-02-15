document.querySelectorAll('.nav-btn').forEach(button => {
    button.addEventListener('click', () => {
      // Видаляємо активний стан у всіх кнопок і секцій
      document.querySelectorAll('.nav-btn').forEach(btn => btn.classList.remove('active'));
      document.querySelectorAll('section').forEach(section => {
        section.classList.remove('active');
        section.style.display = 'none';
      });

      // Додаємо активний стан обраній кнопці та відповідній секції
      button.classList.add('active');
      const sectionName = button.dataset.section;
      const targetSection = document.querySelector(`.${sectionName}-section`);
      if (targetSection) {
        targetSection.classList.add('active');
        targetSection.style.display = 'block';
      }
    });
  });

  // Обробник зміни аватарки
  document.addEventListener('DOMContentLoaded', () => {
    const avatarContainer = document.getElementById('avatarContainer');
    const currentAvatar = document.getElementById('currentAvatar');
    const avatars = [
      "https://marketplace.canva.com/EAFqhoRVTgA/1/0/1600w/canva-grey-and-blue-cute-cartoon-anime-manga-illustrated-boy-profile-photo-avatar-u9aFvuQMzUk.jpg",
      "https://static.vecteezy.com/system/resources/thumbnails/009/749/751/small_2x/avatar-man-icon-cartoon-male-profile-mascot-illustration-head-face-business-user-logo-free-vector.jpg",
      "https://img.freepik.com/free-vector/smiling-young-man-illustration_1308-174401.jpg?semt=ais_hybrid",
      "https://i.etsystatic.com/39063034/r/il/cb88ab/4893801479/il_570xN.4893801479_e4tq.jpg",
      "https://w7.pngwing.com/pngs/439/837/png-transparent-avatar-face-girl-female-woman-profile-happy-avatar-icon.png",
      "https://i.fbcd.co/products/resized/resized-750-500/s211206-kids-avat001-mainpreview-68e535dc97667c8fffa14c6da9e6f5787447ab7513f0fee9a9a39b9856312c9c.jpg"
    ];
    
    let currentIndex = avatars.findIndex(url => url === currentAvatar.src);
    if (currentIndex === -1) currentIndex = 0;

    avatarContainer.addEventListener('click', (e) => {
      e.preventDefault();
      currentIndex = (currentIndex + 1) % avatars.length;
      currentAvatar.src = avatars[currentIndex];
    });
  });