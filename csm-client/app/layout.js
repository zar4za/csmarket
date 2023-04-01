import './globals.css';
import Header from '@comp/Header/Header';

export const metadata = {
  title: 'pxly',
  description: 'Marketplace for csgo skins',
}

export default function RootLayout({ children }) {
  return <html lang="en">
    <body>
      <Header />
      {children}
    </body>
  </html>
}
