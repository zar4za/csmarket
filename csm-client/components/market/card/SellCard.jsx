'use client';
import { background, item, name, sell, price, close, remove } from './SellCard.module.css';
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
        <div className={name}>{market_hash_name}</div>
        <button className={remove} onClick={() => onRemoveItem(asset)}>
            <Image className={close}
                src='/static/icons/close.svg'
                width={24}
                height={24}
            />
        </button>
        <Image
            loader={steamImageLoader}
            src={icon_url}
            width={80}
            height={60}
        />
        <div className={sell}>
            <input type='number' step='0.01' className={price}></input>
            <input type='number' step='0.01' className={price}></input>
        </div>
    </div>
}

function steamImageLoader({src}) {
    return `https://community.akamai.steamstatic.com/economy/image/${src}`;
}