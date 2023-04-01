import Image from 'next/image';
import styles from './page.module.css';
import Header from '@comp/Header/Header';
import SignIn from '@comp/SignIn/SignIn';
import Footer from '@comp/Footer/Footer';

export default function Home() {
  return (
  <>
    <div className={styles.background}>
      <Image
        className={styles.image} 
        alt='background'
        src='/static/images/art.png'
        fill
      />
    </div>
    <main className={styles.hello}>
      <h1 className={styles.theme}>НОРМАЛЬНАЯ ТЕМА - ТЕМНАЯ ТЕМА!</h1>
      <h3 className={styles.trade}>Торгуй скинами как по кайфу - ваще ништяковая тема ежжи! </h3>
      <SignIn />
    </main>
  </>
  )
}
