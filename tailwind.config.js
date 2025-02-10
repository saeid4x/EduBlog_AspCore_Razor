/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml' ,

    ],
  theme: {
    extend: {
          colors: {
            customColor1: '#fde0dd',
            customColor2: '#ece1f0',

          },

          fontFamily: {
              iransans_regular: ['iranSans_regular', 'sans-serif'],
              iransans_black: ['iranSans_black', 'sans-serif'],
              iransans_bold: ['iranSans_bold', 'sans-serif'],
              iransans_medium: ['iranSans_medium', 'sans-serif'],
              
          }
    },
  },
    plugins: [require('daisyui'),],

     safelist: [
    'bg-red-400',
    'bg-blue-400',
    'bg-green-400',
    'bg-amber-400',
    'bg-purple-400'
  ]
}

