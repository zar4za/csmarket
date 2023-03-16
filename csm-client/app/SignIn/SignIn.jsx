'use client';
import Link from "next/link";

export default function SignIn() {
    return (
        <Link href={'/auth/request'}>Sign in</Link>
    )
};