import { getOpenIdRequest } from "@/lib/api/local";

export async function GET() {
    const url = await getOpenIdRequest();

    return Response.redirect(url);
}