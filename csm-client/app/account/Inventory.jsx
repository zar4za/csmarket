'use client';
import { useInventory } from "@/lib/api/client";
import Image from "next/image";

const itemStaticUrl = "https://community.akamai.steamstatic.com/economy/image/";

export default function Inventory() {
    const { data, error, isLoading } = useInventory(localStorage.getItem("Bearer"));
    console.log(data);

    if (isLoading) {
        return <div>Loading...</div>
    }
    else {
        const items = data.map((item) => <div>
            <div>{item.market_hash_name}</div>
            <Image
                src={itemStaticUrl + item.icon_url_large}
                alt={item.market_hash_name}
                height={100}
                width={100} 
            />
        </div>)

        return <div>
            {items}
        </div>
    }
}