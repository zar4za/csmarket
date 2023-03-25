'use client';
import Link from 'next/link';
import styles from './SignIn.module.css';

export default function SignIn() {
    return <Link href={'/auth/request'} className={styles.button}>
        Войти
    </Link>
};