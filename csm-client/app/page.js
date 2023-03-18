import SignIn from "./SignIn/SignIn";
import UserContainer from "./UserContainer/UserContainer";
import Link from "next/link";

export default function Home() {
  return (
    <main>
      <Link href={"/account"}>Аккаунт</Link>
      <UserContainer />
    </main>
  )
}
