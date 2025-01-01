const expired = new Date(Date.now() + 1000 * 60 * 30);
export const accessToken =  {
    secure: false,
    httpOnly: true,
    maxAge:expired
}