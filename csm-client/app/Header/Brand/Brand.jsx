import styles from './Brand.module.css';
import Image from 'next/image';

export default function Brand() {
    return <div className={styles.brand}>
        <Image 
            src='/static/images/logo-ipsum.svg'
            width={40}
            height={40}
            className={styles.logo}
        />
        <span className={styles.brandname}>
            LogoIpsum
        </span>
    </div>
}