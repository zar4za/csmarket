import Link from 'next/link';
import styles from './Header.module.css';
import Brand from '@comp/Brand/Brand';
import UserContainer from '@comp/UserContainer/UserContainer';

export default function Header() {
    return <header className={styles.header}>
        <nav className={styles.container}>
            <Link href='/'>
                <Brand />
            </Link>
            <UserContainer />
        </nav>
    </header>
}