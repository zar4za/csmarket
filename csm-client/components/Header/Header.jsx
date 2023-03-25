import styles from './Header.module.css';
import Brand from '@/components/Brand/Brand';
import Link from 'next/link';
import UserContainer from '@/components/UserContainer/UserContainer';

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