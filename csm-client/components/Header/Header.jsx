import Link from 'next/link';
import styles from './Header.module.css';
import Brand from '@comp/Brand/Brand';
import UserContainer from '@comp/UserContainer/UserContainer';
import Container from '@comp/Container/Container';

export default function Header() {
    return <header className={styles.header}>
        <Container>
            <Link href='/'>
                    <Brand />
                </Link>
            <nav className={styles.container}>
                <Link href={'/market'}>Купить</Link>
                <Link href={'/market/sell'}>Продать</Link>
            </nav>
            <UserContainer />
        </Container>
    </header>
}