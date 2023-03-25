import styles from './Footer.module.css';
import classNames from 'classnames';

export default function Footer({ transparent }) {
    return <footer className={classNames(styles.footer, { [styles.footer_transparent]: transparent })}>
        <div className={styles.container}>
            <span className={styles.footer__copyright}>Copyright © LogoIpsum</span>
            <ul className={styles.footer__info}>
                <li className={classNames(styles.footer__link, { [styles.footer__link_transparent]: transparent })}>Контакты</li>
                <li className={classNames(styles.footer__link, { [styles.footer__link_transparent]: transparent })}>Помощь</li>
                <li className={classNames(styles.footer__link, { [styles.footer__link_transparent]: transparent })}>Пользовательское соглашение</li>
            </ul>
        </div>
    </footer>
}