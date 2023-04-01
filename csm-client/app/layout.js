import './globals.css';
import Header from '@comp/Header/Header';
import Footer from '@comp/Footer/Footer';
import { main } from './Layout.module.css';
import Container from '@comp/Container/Container';

export const metadata = {
  title: 'CS market',
  description: 'Marketplace for csgo skins',
}

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
        <Header />
          <main className={main}>
            <Container>{children}</Container>
          </main>
        <Footer />
        </body>
    </html>
  )
}
