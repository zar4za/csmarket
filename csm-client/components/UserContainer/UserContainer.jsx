'use client';
import SignIn from '@comp/SignIn/SignIn';
import User from '@comp/User/User';

export default function UserContainer() {
    if (!document.cookie.includes("Bearer"))
        return <SignIn />;
    const token = getCookie("Bearer");
    if (localStorage.getItem("Bearer") != token)
        localStorage.setItem("Bearer", token);
    return <User />;
}

function getCookie(name) {
    const cookieString = decodeURIComponent(document.cookie);
    const startIndex = cookieString.indexOf(name) + name.length + "=".length;
    const endIndex = cookieString.indexOf(";", startIndex);
    return cookieString.substring(startIndex, endIndex != -1 ? endIndex : cookieString.length)
}