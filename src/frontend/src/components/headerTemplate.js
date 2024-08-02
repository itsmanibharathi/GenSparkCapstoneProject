import logo from '../../public/assets/Image/logo.svg';

const headerTemplate = (token) => {
    return (
        `<header id="navbar" class="w-full bg-tertiary">
            <nav class="flex justify-between items-center w-[92%] h-[8%] mx-auto">
                <div>
                    <a href="/">
                        <p class="text-2xl text-primary font-bold">360area.tech</p>
                    </a>
                </div>
                <div class="bg-tertiary  z-40 text-lg nav-links duration-500 md:static absolute md:min-h-fit min-h-[60vh] left-0 top-[-100%] md:w-auto w-full flex items-center px-5">
                    <ul class="m-auto flex md:flex-row flex-col md:items-center md:gap-[4vw] gap-8">
                        <li><a href="/property/post" class="nav">Post Property</a></li>
                        <li><a href="/subscription/plan" class="nav">Subscription Plan</a></li>
                    </ul>
                </div>
                <div class="flex items-center gap-6 my-2">
                    ${token.exists() ? `
                        <div class="relative">
                            <img src="https://i.pravatar.cc/250?u=mail@ashallendesign.co.uk" class="w-10 h-10 rounded-full cursor-pointer" onclick="toggleProfileMenu()" alt="Profile">
                            <div id="profile-menu" class="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg py-2 hidden">
                                <a href="/profile" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Profile</a>
                                <a href="/orders" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Orders</a>
                                <a href="/logout" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">Logout</a>
                            </div>
                        </div>
                    ` : `
                        <a href="/auth" class="bg-button text-white text-lg px-5 py-1 rounded-xl">SignIn</a>
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
                    navLinks.classList.add('top-[7.7%]');
                } else {
                    navLinks.classList.remove('top-[7.7%]');
                    navLinks.classList.add('top-[-100%]');
                }
            }

            function toggleProfileMenu() {
                const profileMenu = document.getElementById('profile-menu');
                profileMenu.classList.toggle('hidden');
            }
        </script>`
    );
}

export default headerTemplate;
