import useSWRImmutable from "swr/immutable";

const fetchWithJwt = ({url, token}) => fetch(url, {
        headers: {
            Authorization: `Bearer ${token}`
        }
    }).then(res => res.json());

export const useInventory = (token) => useSWRImmutable({ url: '/api/user', token }, fetchWithJwt);

