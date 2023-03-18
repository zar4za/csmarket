import { signOpenIdClaims } from "@/lib/api/local";
import { NextResponse } from "next/server";

export async function GET(request) {
    const { origin, search }= new URL(request.url);
    const token = await signOpenIdClaims(search);
    const response =  NextResponse.redirect(origin);
    response.cookies.set({
        name: "Bearer",
        value: token,
        expires: new Date(new Date().getTime() + 600 * 1000)
    });
    return response;
}