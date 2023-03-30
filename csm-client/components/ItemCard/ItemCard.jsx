'use client';
import Image from 'next/image';
import Link from 'next/link';
import { card, card__price, card__link, card__wear, card__desc, card__image } from './ItemCard.module.css';
import itemCardQuality from './ItemCardQuality.module.css';


const heightAspect = 3/4;
const width = 170;
const height = width * heightAspect;
const regex = /\|\s(.*)\s\((.*)\)/;


export default function ItemCard({href, market_hash_name, icon_url, price, quality, float}) {
    const finish = market_hash_name.match(regex);
    let name = market_hash_name;
    let wear = '';
    if (finish != undefined && finish.length != 0) {
        name = finish[1];
        wear = finish[2].split(' ')[0]?.substring(0, 1) + finish[2]?.split(' ')[1]?.substring(0, 1);
    }

    return <div className={getCardClass(quality)}>
        <Image
                className={card__image}
                src={icon_url}
                loader={steamImageLoader}
                fill
            />
        <div className={card__desc}>
            <Link href={href} className={card__link}>
                <div className={card__wear}>{wear} {float?.toFixed(4) ?? <></>}</div>
                <div>{name}</div>
                {
                    price == null ? <></> : <div className={card__price}>{price} ₽</div>
                }
            </Link>
        </div>
    </div>
}

function steamImageLoader({src}) {
    return `https://community.akamai.steamstatic.com/economy/image/${src}`;
}

function getCardClass(quality) {
    if (quality == 'common') return card;
    return `${card} ${itemCardQuality[quality]}`;
}