'use client';
import Image from 'next/image';
import Footer from '@/components/Footer/Footer';
import Header from '@/components/Header/Header';
import { useInventory } from '@/lib/api/client';

export default function UserPage() {
    const { data, error, isLoading } = useInventory(localStorage.getItem('Bearer'));

    return <>
    <Header />
    {console.log(data?.inventory)}
    {isLoading ? "Загрузка" : data.inventory.map(item => <AssetCard asset={item}/>)}
    <Footer />
    </>
}

function AssetCard({asset}) {
    return <div>
        <Image
            loader={loader}
            src={asset.icon_url}
            height={100}
            width={100}
        />
        {asset.market_hash_name}
    </div>
}

function loader({src}) {
    return `https://community.akamai.steamstatic.com/economy/image/${src}`
}