import Image from 'next/image';
import Footer from '@comp/Footer/Footer';
import Header from '@comp/Header/Header';
import ItemCard from '@comp/ItemCard/ItemCard';
import SkeletonCard from '@comp/ItemCard/SkeletonCard';
import { grid } from './page.module.css';
// import useSWRImmutable from 'swr/immutable';
import { getInventory } from '@/lib/api/client';
// import { useRouter }from 'next/router';
import Inventory from '@/components/Inventory/Inventory';

export default function UserPage() {
    // const { data, error, isLoading } = useSWRImmutable('inventory', getInventory);
    // const amount = Math.floor(window.innerWidth / 240 * window.innerHeight / 180);

    return <>
        <Inventory />


    {/* <main className={grid}>
    {
        isLoading ? 
        <Skeleton length={amount}/> :  
        data.inventory.map(asset => 
            <ItemCard
                href="#"
                assetId={asset.assetid}
                market_hash_name={asset.market_hash_name}
                icon_url={asset.icon_url}
                quality={asset.rarity}
            />
        )
    }
    </main> */}
    </>
}

// function Skeleton({ length }) {
//     return <>
//         {Array(length).fill(null).map((item, index) => <SkeletonCard key={index}/>)}
//     </>
// }