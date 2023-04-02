'use client';
// import useSWRImmutable from "swr/immutable";
// const fetchWithJwt = ({url, token}) => fetch(url, {
//         headers: {
//             Authorization: `Bearer ${token}`
//         }
//     }).then(res => res.json());

// export const useInventory = (token) => useSWRImmutable({ url: '/api/user/inventory', token }, fetchWithJwt);

export const getInventory = () => getWithCreds('/api/user/inventory');
export const postOnSale = (items) => postWithCreds('/api/market/list', items);

async function postWithCreds(url, data) {
    console.log(data);
    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('Bearer')}`
        }
    })

    if (!response.ok) throw new Error("Failder to post");
}

async function getWithCreds(url) {
    const response = await fetch(url, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem('Bearer')}`
        }
    });

    if (response.status == 401) {
        const error = new Error("Unauthorized, proceed to signin");
        error.redirect = '/';
        throw error;
    }

    return response.json();
}