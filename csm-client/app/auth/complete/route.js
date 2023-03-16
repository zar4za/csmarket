import { signOpenIdClaims } from "@/lib/api/local";

export async function GET(request) {
    const { search }= new URL(request.url);
    const token = signOpenIdClaims(search);
    
    return new Response(init={
        headers: {
            'Set-Cookie': `bearer=${token}`
        }
    });
}