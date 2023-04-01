'use client'
import { getInventory } from '@/lib/api/client';
import useSWRImmutable from 'swr/immutable';
import SkeletonCard from '@comp/market/card/SkeletonCard';
import ItemCard from '@comp/market/card/ItemCard';
import { grid } from './Inventory.module.css';
import { useRouter } from 'next/navigation';

export default function Inventory({handleAdd, handleRemove}) {
    const router = useRouter();
    const length = Math.floor((document.documentElement.clientWidth - 40) / 200) * Math.floor((document.documentElement.clientHeight - 40) / 200);
    const {
        isLoading,
        data,
        error
    } = useSWRImmutable('inventory', getInventory);



    const items = isLoading ? 
        Array(length).fill(null).map(() => <SkeletonCard />) :
        (error ? router.push(error.redirect) : data.inventory.map(asset => 
            <ItemCard
                href="#"
                assetId={asset.assetid}
                market_hash_name={asset.market_hash_name}
                icon_url={asset.icon_url}
                quality={asset.rarity}
                onAddItem={handleAdd}
                onRemoveItem={handleRemove}
            />
        ));

    return <div className={grid}>
        {items}
    </div>
}