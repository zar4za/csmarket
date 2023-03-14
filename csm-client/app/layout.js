import './globals.css'

export const metadata = {
  title: 'CS market',
  description: 'Marketplace for csgo skins',
}

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  )
}
