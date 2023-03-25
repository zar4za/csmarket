import styles from './Brand.module.css';
import Image from 'next/image';

export default function Brand() {
    return <div className={styles.brand}>
        <Image
            src='/static/images/logo.svg'
            width={76}
            height={40}
            className={styles.logo}
        />
    </div>
}