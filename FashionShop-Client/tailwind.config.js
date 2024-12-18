const mtConfig = require("@material-tailwind/react").mtConfig;
/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./src/**/*.{js,jsx,ts,tsx}",
        "./node_modules/@material-tailwind/react/**/*.{js,ts,jsx,tsx}"
    ],
    theme: {
        extend: {},

    },
    plugins: [mtConfig],

}
