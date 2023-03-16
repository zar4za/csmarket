export async function getOpenIdRequest() {
    const data = await fetch(
        process.env.LOCAL_HOST + "/api/auth/request",
        {
            cache: 'reload',
            next: {
                revalidate: 60
            }
        }
    ).then(r => r.json());

    return data.url;
}

export async function signOpenIdClaims(openIdQuery) {
    const data = await fetch(
        process.env.LOCAL_HOST + "/api/auth/complete" + openIdQuery,
        {
            cache: "no-cache"
        }
    ).then(r => r.json());

    return data.token;
}