import Footer from '@comp/Footer/Footer';
import Container from '@comp/Container/Container';
import { main } from './app.module.css';

export default function PagesLayout({ children }) {
    return <>
        <main className={main}>
            <Container>
                {children}
            </Container>
        </main>
        <Footer />
    </>
}