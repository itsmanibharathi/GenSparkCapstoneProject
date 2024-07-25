import logo from '../../public/assets/Image/logo.svg';

const headerTemplate = (token) => {
    return (
        `<header id="navbar" class=" fixed w-full bg-transparent transition-colors duration-300">
            <nav class="flex justify-between items-center w-[92%] h-[8%] mx-auto">
                <div>
                    <a href="/">
                        <img class="w-16 cursor-pointer" src="${logo}" alt="Logo">
                    </a>
                </div>
                <div class="bg-tertiary  z-10 text-lg nav-links duration-500 md:static absolute md:min-h-fit min-h-[60vh] left-0 top-[-100%] md:w-auto w-full flex items-center px-5">
                    <ul class="m-auto flex md:flex-row flex-col md:items-center md:gap-[4vw] gap-8">
                        <li><a href="/postproperty" class="nav">Post Property</a></li>
                    </ul>
                </div>
                <div class="flex items-center gap-6 my-2">
                    ${!token.excess ? `
                        <div class="relative">
                            <img src="https://i.pravatar.cc/250?u=mail@ashallendesign.co.uk" class="w-10 h-10 rounded-full cursor-pointer" onclick="toggleProfileMenu()" alt="Profile">
                            <div id="profile-menu" class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg py-2 hidden">
                                <a href="/profile" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Profile</a>
                                <a href="/orders" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Orders</a>
                                <a href="/logout" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Logout</a>
                            </div>
                        </div>
                    ` : `
                        <a href="/login" class="bg-button text-white text-lg px-5 py-1 rounded-xl">LogIn</a>
                    `}
                    <button onclick="onToggleMenu(this)" class="md:hidden">
                        <i name="menu" class="fa-solid fa-bars"></i>
                    </button>
                </div>
            </nav>
        </header>
        <style>
    \
        </style>
        <script>
            function onToggleMenu(e) {
                const navLinks = document.querySelector('.nav-links');
                if (navLinks.classList.contains('top-[-100%]')) {
                    navLinks.classList.remove('top-[-100%]');
                    navLinks.classList.add('top-[6%]');
                } else {
                    navLinks.classList.remove('top-[6%]');
                    navLinks.classList.add('top-[-100%]');
                }
            }

            function toggleProfileMenu() {
                const profileMenu = document.getElementById('profile-menu');
                profileMenu.classList.toggle('hidden');
            }

            window.addEventListener('click', function(e) {
                const profileMenu = document.getElementById('profile-menu');
                if (!e.target.closest('.relative')) {
                    profileMenu.classList.add('hidden');
                }
            });

            window.addEventListener('scroll', function() {
                const navbar = document.getElementById('navbar');
                if (window.scrollY > 50) {
                    navbar.classList.add('bg-tertiary');
                    navbar.classList.remove('bg-transparent');
                } else {
                    navbar.classList.remove('bg-tertiary');
                    navbar.classList.add('bg-transparent');
                }
            });
        </script>`
    );
}

export default headerTemplate;
