/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      gridRow: {
        'span-7': 'span 7 / span 7',
        'span-10': 'span 10 / span 10',
        'span-13': 'span 13 / span 13',
        'span-16': 'span 16 / span 16',
      },
      colors: {
        primary: {
          "50": "#ECF8F8",
          "100": "#DAF1F1",
          "200": "#B9E5E5",
          "300": "#93D7D7",
          "400": "#6EC9C9",
          "500": "#4CBCBC",
          "600": "#399898",
          "700": "#2B7373",
          "800": "#1D4E4E",
          "900": "#0E2525"
        },
        secondary: {
          "50": "#FEFCF6",
          "100": "#FDF8EC",
          "200": "#FCF2D9",
          "300": "#FAEBC6",
          "400": "#F8E2AF",
          "500": "#F7DC9D",
          "600": "#F1C151",
          "700": "#E2A412",
          "800": "#976D0C",
          "900": "#4B3706"
        }
      },
    },
  },
  daisyui: {
    themes: [
      {
        mytheme: {
          "primary": "#4CBCBC",     
          "secondary": "#F7DC9D",
          "accent": "#EA5234",
          "neutral": "#333C4D",
          "base-100": "#FFFFFF",
          "info": "#3ABFF8",
          "success": "#36D399",
          "warning": "#FBBD23",
          "error": "#F87272",
        },
      },
    ],
  },
  plugins: [require("daisyui")],
}
