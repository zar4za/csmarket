/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
    appDir: true,
  },
  rewrites: async () => {
    return [
      {
        source: '/api/:path*',
        destination: process.env.NEXT_PUBLIC_HOST + '/api/:path*'
      }
    ]
  },
  images: {
    domains: [
      "community.akamai.steamstatic.com"
    ]
  }
}

module.exports = nextConfig
