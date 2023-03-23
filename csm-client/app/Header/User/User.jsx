'use client';
import Image from "next/image";

export default function User() {
    return <div>
        <Image 
            src="/static/images/template.jpg"
            width={100}
            height={100}
        />
        <span>Template name</span>
        <br />
        <span>{localStorage.getItem("Bearer").split('.')[2]}</span>
        <br />
        <span>{document.cookie.split('.')[2]}</span>
    </div>
}