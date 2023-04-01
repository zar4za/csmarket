import './globals.css';
import Header from '@comp/Header/Header';
import Footer from '@comp/Footer/Footer';
import { main } from './Layout.module.css';

export const metadata = {
  title: 'CS market',
  description: 'Marketplace for csgo skins',
}

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Header />
          <main className={main}>{children}</main>
        <Footer />
        </body>
    </html>
  )
}
