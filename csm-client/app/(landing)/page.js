import SignIn from '@comp/SignIn/SignIn';
import Container from '@comp/Container/Container';
import { offer, head, motto } from './landing.module.css';

export default function Home() {
  return <Container>
    <div className={offer}>
      <h1 className={head}>НОРМАЛЬНАЯ ТЕМА - ТЕМНАЯ ТЕМА!</h1>
      <h3 className={motto}>Торгуй скинами как по кайфу - ваще ништяковая тема!</h3>
      <SignIn />
    </div>
  </Container>
}
