import { createContext } from 'react';

export const SellItemsContext = createContext([]);

export function sellItemsReducer(items, action) {
    switch (action.type) {
        case 'ADD': {
            return [...items, action.asset];
        }
        case 'SET': {
            return items.map(i => {
                if (i.assetId === action.assetId) {
                    i.price = action.price
                    return i;
                } else {
                  return i;
                }
              });
        }
        case 'REMOVE': {
            return items.filter(a => a.assetId !== action.asset.assetId);  
        }
        default: {
            throw Error('Unknown action: ' + action.type);
        }
    }
}