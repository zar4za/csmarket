import Link from 'next/link';
import styles from './Header.module.css';
import Brand from '@comp/Brand/Brand';
import UserContainer from '@comp/UserContainer/UserContainer';
import Container from '@comp/Container/Container';

export default function Header() {
    return <header className={styles.header}>
        <Container>
            <nav className={styles.container}>
                <Link href='/'>
                    <Brand />
                </Link>
            </nav>
            <UserContainer />
        </Container>
    </header>
}