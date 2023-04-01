'use client';
import { useReducer } from 'react';
import Inventory from '@comp/Inventory/Inventory';
import SellBlock from '@comp/SellBlock/SellBlock';
import { SellItemsContext, sellItemsReducer } from '@comp/market/context';

export default function SellPage() {
    const [items, dispatch] = useReducer(sellItemsReducer, []);
    const handleAdd = (asset) => {
        console.log(items);
        dispatch({
            type: 'ADD',
            asset: asset
        });
    }
    
    const handleRemove = (asset) => {
        dispatch({
            type: 'REMOVE',
            asset: asset
        });
    }


    return <SellItemsContext.Provider>
        <SellBlock items={items}/>
        <Inventory handleAdd={handleAdd} handleRemove={handleRemove}/>
    </SellItemsContext.Provider>
}