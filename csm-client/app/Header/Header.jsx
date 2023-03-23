import styles from './Header.module.css';
import Brand from './Brand/Brand';
import Link from 'next/link';
import UserContainer from './UserContainer/UserContainer';

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