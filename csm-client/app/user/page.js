'use client';
import Image from 'next/image';
import Footer from '@/components/Footer/Footer';
import Header from '@/components/Header/Header';
import { useInventory } from '@/lib/api/client';
import ItemCard from '@/components/ItemCard/ItemCard';
import { grid } from './page.module.css';

export default function UserPage() {
    const { data, error, isLoading } = useInventory(localStorage.getItem('Bearer'));

    return <>
    <Header />
    <main className={grid}>
    {
        isLoading ? 
        'Загрузка' : 
        data.inventory.map(asset => 
            <ItemCard
                href="#"
                market_hash_name={asset.market_hash_name}
                icon_url={asset.icon_url}
                quality={asset.rarity}
            />
        )
    }
    </main>
    <Footer />
    </>
}