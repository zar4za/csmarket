import SellCard from '../market/card/SellCard';
import { block } from './SellBlock.module.css';

export default function SellBlock({ items }) {
    return <div className={block}>
        <h3>Предметы на продажу</h3>
        <div>
            {items.map(item => <SellCard asset={item}/>)}
        </div>
    </div>
}