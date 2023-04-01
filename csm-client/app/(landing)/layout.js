import Image from 'next/image';
import Footer from '@comp/Footer/Footer';
import { background, main, container } from './landing.module.css';

export default function HomeLayout({ children }) {
  return <>
    <Image 
        className={background} 
        alt='background'
        src='/static/images/art.png'
        fill
      />
    <main className={main}>
      {children}
    </main>
    <Footer transparent/>
  </>
}
