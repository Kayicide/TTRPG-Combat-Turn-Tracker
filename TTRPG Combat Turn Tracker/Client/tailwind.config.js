/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{razor,html,cshtml,cs,razer.cs}"],
    safelist: [
        {
            pattern: /bg-(red|green|blue|orange|purple|sky|gray|black|white)-(...)/,
            pattern: /w-(..)/,
            pattern: /h-(..)/,
        }
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
