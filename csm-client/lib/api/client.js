'use client';
// import useSWRImmutable from "swr/immutable";
// const fetchWithJwt = ({url, token}) => fetch(url, {
//         headers: {
//             Authorization: `Bearer ${token}`
//         }
//     }).then(res => res.json());

// export const useInventory = (token) => useSWRImmutable({ url: '/api/user/inventory', token }, fetchWithJwt);

export const getInventory = () => fetchWithCreds('/api/user/inventory');

async function fetchWithCreds(url) {
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