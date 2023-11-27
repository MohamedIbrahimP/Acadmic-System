// JavaScript code to handle mode switching
var toggleModeButton = document.getElementById('toggleModeButton');
var body = document.body;

toggleModeButton.addEventListener('click', function () {
    // Toggle between 'dark-mode' and 'light-mode' classes on the body
    body.classList.toggle('light-mode');
    body.classList.toggle('dark-mode');

    // Save the user's preference in localStorage
    var isDarkMode = body.classList.contains('dark-mode');
    localStorage.setItem('darkMode', isDarkMode);
});

// Check user's preference in localStorage and set the initial mode
var savedDarkMode = localStorage.getItem('darkMode');
if (savedDarkMode === 'true') {
    body.classList.add('dark-mode');
} else {
    body.classList.add('light-mode'); // Assume light mode if preference not found
}
//////////////////////////////////////////////////////////////////////////////////////