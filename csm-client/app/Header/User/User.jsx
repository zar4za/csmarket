'use client';
import Image from 'next/image';
import styles from './User.module.css';

const formatConfig = {
    style: 'currency',
    currency: 'RUB',
    minimumFractionDigits: 2,
    currencyDisplay: 'symbol',
};

const formatter = new Intl.NumberFormat('ru-RU', formatConfig);

export default function User() {
    const balance = 1000000;

    return <div className={styles.user}>
        <button 
            className={styles.balance}
            onClick={() => alert(formatter.format(balance))}
        >
            <Image 
                className={styles.icon}
                src='/static/icons/plus.svg'
                width={24}
                height={24}
            />
            {formatter.format(balance)}
        </button>
        <Image className={styles.avatar}
            src='/static/images/avatar_medium.jpg'
            width={40}
            height={40}
        />
    </div>
}