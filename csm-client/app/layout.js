import './globals.css';
import Header from '@comp/Header/Header';
import Footer from '@comp/Footer/Footer';

export const metadata = {
  title: 'CS market',
  description: 'Marketplace for csgo skins',
}

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Header />
        {children}
        <Footer />
        </body>
    </html>
  )
}
