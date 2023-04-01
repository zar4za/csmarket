'use client';
import Image from 'next/image';
import Link from 'next/link';
import { card, card__price, card__link, card__wear, card__desc, card__image, card__name } from './ItemCard.module.css';
import itemCardQuality from './ItemCardQuality.module.css';


const regex = /\|\s(.*)\s\((?:([A-Z])(?:([a-z]*)\)|[a-z]*(?:-|\s)([A-Z])))/;


export default function ItemCard({href, assetId, market_hash_name, icon_url, price, quality, float}) {
    const finish = market_hash_name.match(regex);
    let name = market_hash_name;
    let wear = '';
    if (finish != undefined && finish.length != 0) {
        name = finish[1];
        wear = finish[2] + (finish[3] == undefined ? finish[4] : finish[3].toUpperCase());
    }

    return <div className={getCardClass(quality)} id={assetId}>
        <Image
                className={card__image}
                src={icon_url}
                loader={steamImageLoader}
                fill
            />
        <div className={card__desc}>
            <Link href={href} className={card__link}>
                <div className={card__wear}>{wear} {float?.toFixed(4) ?? <></>}</div>
                <div className={card__name}>{name}</div>
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