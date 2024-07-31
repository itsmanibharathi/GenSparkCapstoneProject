/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,js}', './public/index.html'],
  theme: {
    extend: {
      colors: {
        primary: '#1E293B',  // Dark blue for a professional look
        secondary: '#3B82F6', // Blue for accents and highlights
        tertiary: '#64748B',  // Grayish blue for neutral elements
        button: '#3B82F6',    // Consistent with secondary color
        'button-hover': '#2563EB', // Slightly darker blue for button hover state
        accent: '#FBBF24', // Gold for luxury touches and highlights
        background: '#F1F5F9', // Light gray for backgrounds
      },
      boxShadow: {
        'md': '0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)',
        'lg': '0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
        'xl': '0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)',
      },
    },
  },
  plugins: [],
}
