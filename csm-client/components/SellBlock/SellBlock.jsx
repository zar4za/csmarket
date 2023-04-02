import { postOnSale } from '@/lib/api/client';
import SellCard from '../market/card/SellCard';
import { block, cards, confirm, submit } from './SellBlock.module.css';

export default function SellBlock({ items }) {
    return <div className={block}>
        <h3>Предметы на продажу</h3>
        <div className={cards}>
            {items.map(item => <SellCard asset={item}/>)}
        </div>
        <div className={confirm}>
            <button className={submit} onClick={() => {
                postOnSale(items.map(item => {
                    return {
                        assetId: item.assetId,
                        price: item.price
                    }
                }));
            }}>Продать</button>
        </div>
    </div>
}
