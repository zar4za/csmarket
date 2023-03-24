import Header from './Header/Header';
import styles from './page.module.css';
import Image from 'next/image';
import SignIn from './Header/SignIn/SignIn';

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
    <Header />
    <main className={styles.hello}>
      <h1 className={styles.theme}>НОРМАЛЬНАЯ ТЕМА - ТЕМНАЯ ТЕМА!</h1>
      <h3 className={styles.trade}>Торгуй скинами как по кайфу - ваще ништяковая тема ежжи! </h3>
      <SignIn />
    </main>
  </>
  )
}
