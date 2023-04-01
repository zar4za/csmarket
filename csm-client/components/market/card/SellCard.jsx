'use client';
import { background, name } from './SellCard.module.css';
import Image from 'next/image';

export default function SellCard({ asset }) {
    const {
        market_hash_name, 
        icon_url, 
        quality, 
        float, 
        onRemoveItem
    } = asset;

    return <div className={background}>
        <Image
            loader={steamImageLoader}
            src={icon_url}
            width={120}
            height={90}
        />
        <div>
            <input></input>
            <button></button>
        </div>
        <div className={name}>{market_hash_name}</div>
    </div>
}

function steamImageLoader({src}) {
    return `https://community.akamai.steamstatic.com/economy/image/${src}`;
}