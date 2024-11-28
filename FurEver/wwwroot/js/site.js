window.addEventListener("DOMContentLoaded", () => {
    const progressBars = document.getElementsByClassName('progress-bar-rating');
    Array.from(progressBars).forEach(progressBar => {
        const value = progressBar.value;
        const min = progressBar.min || 0;
        const max = progressBar.max || 100;
        const percentage = ((value - min) / (max - min)) * 100;
        const progressContainer = progressBar.closest('.progress-bar');
        if (progressContainer) {
            progressContainer.style.background = `
                radial-gradient(closest-side, white 79%, transparent 80% 100%),
                conic-gradient(#1d5d9b ${percentage}%, #75c2f6 0)
            `;
        }
    });

    document.querySelector('.read-more').addEventListener('click', function () {
        const description = document.querySelector('.long-description');
        description.classList.toggle('expanded');
        if (description.classList.contains('expanded')) {
            this.textContent = 'Read Less';
        } else {
            this.textContent = 'Read More';
        }
    });
});
